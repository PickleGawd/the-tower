using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class PlayerScript : MonoBehaviour
{

	public Transform m_camera;

	private int currentLevel = 0;
	public Transform current;//Cube Control
	public Transform currentHeight;

	public Vector3 cameraPos;

	private Transform[] holderLevels = new Transform[5];//Make it able to change number of levels
	public GameObject holder;

	public GameObject gameOver;
	public TextMeshProUGUI timerText;//UI
	public float timeLeft;

	int redBlockIndexer = 0;
	private Transform[] redTiles = new Transform[20];//May not need

	AudioSource audioSource;
	public AudioClip gameOverClip;//Audio
	bool audioOneTime = false;

	public Material currentMat;
	public Material redBlockMat;

	private void Start()
	{
		int levelIndexer = 0;
		audioSource = GetComponent<AudioSource>();
		cameraPos = m_camera.position;

		foreach (Transform child in holder.transform) {
			holderLevels[levelIndexer++] = child;
			Debug.Log(child);
			foreach (Transform _child in child) {
				foreach(Transform __child in _child)
				{
					if(__child.transform.tag == "Red Block")
					{
						__child.gameObject.GetComponent<MeshRenderer>().material = redBlockMat;
						redTiles[redBlockIndexer] = __child;
						redBlockIndexer++;
					}
				}
			}
		}
	}

	private void Update()
	{
		if (timeLeft <= 0)
			StartCoroutine(Death());
		else
			Timer();
	}

	private void OnCollisionEnter(Collision col)
	{
		if(col.transform.tag == "Victory")
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}

		if(col.transform.tag == "Red Block")
		{
			StartCoroutine(Death());
		}

		current = col.transform;
		currentHeight = col.transform.parent.parent;

		Debug.Log(currentLevel);

		col.gameObject.GetComponent<MeshRenderer>().material = currentMat;

		if(currentHeight != holderLevels[currentLevel])
			ChangeCurrentHeight();
		Debug.Log(currentHeight == holderLevels[currentLevel]);
			
		
	}

	void ChangeCurrentHeight()
	{
		foreach(Transform child in holderLevels)
		{
			if(child != null && child.position.y >= holderLevels[currentLevel].position.y)
			{
				Destroy(child.gameObject);
			}

		}
		
		currentLevel++;
		currentHeight = holderLevels[currentLevel];

		m_camera.position = new Vector3(m_camera.position.x, holderLevels[currentLevel].position.y - 10, m_camera.position.z);

		cameraPos = m_camera.position;

		Debug.Log(m_camera.position.y + " " + (currentHeight.position.y - 10));
	}

	public void Move(float x)
	{
		if (gameObject.transform.position.x > 35 && x > 0)
			return;

		if (gameObject.transform.position.x < -35 && x < 0)
			return;

		transform.Translate(new Vector3(x, 0, 0) * 20);
	}

	public void MoveWithCube()
	{
		Vector3 pos = new Vector3(current.position.x, current.position.y + 5, current.position.z);
		transform.position = pos;
	}

	IEnumerator Death()
	{
		gameOver.SetActive(true);
		timeLeft = 0;

		if (!audioOneTime)
		{
			audioSource.PlayOneShot(gameOverClip);
			audioOneTime = true;
		}
		
		yield return new WaitForSeconds(gameOverClip.length);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	void Timer()
	{
		timerText.text = "Time Left: " + timeLeft;

		if (timeLeft <= 0f)
		{
			timeLeft = 0f;
			StartCoroutine(Death());
		}else
			timeLeft -= Time.deltaTime;
		timeLeft = Mathf.Round(timeLeft * 100f) / 100f;
	}
}

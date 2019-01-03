using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Script : MonoBehaviour
{

	public bool topView = false;

	public Transform menu;
	public Transform m_camera;

	Transform current;

	public GameObject player;

	Vector3 camerPos;

	public void StartGame()
	{
		if (!PlayerPrefs.HasKey("Level"))
			PlayerPrefs.SetInt("Level", 1);
		SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
	}

	public void LoadLevel(int level)
	{
		SceneManager.LoadScene(level);
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void Pause(bool pause)
	{
		menu.gameObject.SetActive(pause);
		Time.timeScale = pause ? 0f : 1f;
	}

	public void TopView()
	{
		topView = true;

		current = player.GetComponent<PlayerScript>().current;

		m_camera.Translate(Vector3.up * 50);
		m_camera.transform.eulerAngles = new Vector3(10, 0, 0); ;
	}

	public void NormalView()
	{
		topView = false;

		m_camera.SetPositionAndRotation(player.GetComponent<PlayerScript>().cameraPos, new Quaternion(0, 0, 0, 0));
	}
}

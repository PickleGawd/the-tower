using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

    static MusicPlayer instance = null;

	string level;

	private void Awake()
	{
		level = SceneManager.GetActiveScene().name;

		if (level == "Win")
		{
			Destroy(gameObject);
		}

		if (instance != null)
		{
			Debug.Log("Object Self-Destructing");
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepMusicAlive : MonoBehaviour 
{

	// keeps the BackGround music game object allive 
	// thru out all scenes 
	// Must start in first scene
	void Awake ()
	{
		// store the music game objects in an array
		GameObject[] musicObj = GameObject.FindGameObjectsWithTag("Music");

		// no more than one music object in scene 
		// destroy if more than one
		if(musicObj.Length > 1)
		{
			Destroy(this.gameObject);
		}
		else
			// else if just one, keep it alive
			//thru out all scenes
			DontDestroyOnLoad(this.gameObject);

	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;// new sceneManger requirement


public class LevelManager : MonoBehaviour {
	/*Stinky Boots LLC 2017*/

	[Tooltip("How Many Seconds till autoload next scene in build settings")]
	public float autoLoadNextLevelAfter;  

	private bool effectDelay;
	private string button0Name;

	void Start()
	{
		if (autoLoadNextLevelAfter <= 0) 
		{
			Debug.Log ("Auto Load Next Scene is Disabled use positive numbers\n to represent how many seconds to wait (LevelManager)");
		}
		else
		{
			// Invoke - the AutoLoadScene() function, 
			// AFTER autoLoadNextLevelAfter public variable
			Invoke ("AutoLoadScene", autoLoadNextLevelAfter);  
		}

		effectDelay = true;
	}


	public void LoadLevel(string name)
	{
		Debug.Log ("New Level load: " + name);

		SceneManager.LoadScene (name);
	}

	public void QuitRequest()
	{
		Debug.Log ("Quit requested");
		Application.Quit ();

	}
	
	public void AutoLoadScene() 
	{
		// get active scene in build index then add 1
		// auto load next scene in build index
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}

	// use this method to wait for a sound to play on a button
	public void LoadSceneDelayed(string name0)
	{
		button0Name = name0;

		if (effectDelay == true)
		{
			StartCoroutine(WaitForMusic0());
		}
		else
		{
			SceneManager.LoadScene (name0);
			Debug.Log ("New Level load: " + name0);
		}

	}

	/*******************D E L A Y S ********************/

	IEnumerator WaitForMusic0()
	{ 
		effectDelay = false;
		// .5 sec delay to wait for buttun audio source
		yield return new WaitForSeconds (0.5f);
		LoadSceneDelayed (button0Name); 

	}
	/*******************D E L A Y S ********************/

}//end

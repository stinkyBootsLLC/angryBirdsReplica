using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// original script by Brackeys.com
public class EnemyController : MonoBehaviour 
{

	public GameObject deathEffect;
	public float health = 4f;
	public static int EnemiesAlive = 0;

	private AudioSource explodeSound;

	void Start ()
	{
		// creat object or instatiate
		explodeSound = GetComponent<AudioSource>();
		EnemiesAlive++;
		// this variable will increase by 1 for each 
		// enemy in the scene
		// each enemy in the scene has this script attached to it 
		// so it will be one on start 
	}



	void OnCollisionEnter2D (Collision2D colInfo)
	{	
		// The relative linear velocity of the two colliding objects
		// if the magnitude is > than health then die
		if (colInfo.relativeVelocity.magnitude > health)
		{
			Debug.Log("dead enemy");

			// play explosion sound
			explodeSound.Play();
			// need a delay here before dying
			// start the delay
			StartCoroutine(SoundDelay());

		}
	}

	void Die ()
	{
		
		// a call to the particle system 
		Instantiate(deathEffect, transform.position, Quaternion.identity);
		// decrease the amount by one
		EnemiesAlive--;
		// if all enemies in scene are dead
		if (EnemiesAlive <= 0)
		{
			Debug.Log("LEVEL WON!");
			// the next level call will be here 
			// restart the scene is temp (only one scene)
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			// reset the enemy count
			EnemiesAlive = 0;
		}

		Destroy(gameObject);
	}

	IEnumerator SoundDelay ()
	{
		// wait .5 seconds then goto function Die()

		yield return new WaitForSeconds(.2f);

		// Function call
		Die();


	}





}

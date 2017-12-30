using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// original script by Brackeys.com
public class BallController : MonoBehaviour 
{
	// declare public variables
	public Rigidbody2D rigidBody;
	public Rigidbody2D sling;
	public float releaseTime = .15f;
	// how far the ball can be pulled back
	public float maxDragDistance = 1.5f;
	public GameObject nextBall;
	// declare private variables
	private bool mouseIsPressed = false;

	void Update ()
	{
		if (mouseIsPressed)
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			// will limit how far back the ball can be pulled on the sling shot
			if (Vector3.Distance(mousePos, sling.position) > maxDragDistance)
			{
				rigidBody.position = sling.position + (mousePos - sling.position).normalized * maxDragDistance;
			}
			else
			{
				rigidBody.position = mousePos;
			}	
		}
	}

	void OnMouseDown ()
	{
		// Hold the ball
		// will toggle boolean
		mouseIsPressed = true;

		// toggles the body type setting 
		// on the rigidbody2D component
		rigidBody.isKinematic = true;
	}

	void OnMouseUp ()
	{
		// let go of the ball
		// will toggle boolean
		mouseIsPressed = false;

		// toggles the body type setting 
		// on the rigidbody2D component
		rigidBody.isKinematic = false;

		// start the delay
		StartCoroutine(ReleaseDelay());
	}

	void OnCollisionEnter2D (Collision2D collider)
	{
		// needs to pass an argument
		// future make explosion and destroy 
		Debug.Log("COLLISION");

	}

	// interface
	// A delay routine
	IEnumerator ReleaseDelay ()
	{
		// releaseTime is Public and adjustable
		yield return new WaitForSeconds(releaseTime);


		GetComponent<SpringJoint2D>().enabled = false;

		// cant touch ball after release
		this.enabled = false;

		// wait for next ball
		yield return new WaitForSeconds(2f);


		// gets rid off reference error
		// if the next ball is available
		if (nextBall != null)
		{
			// activate the next ball
			nextBall.SetActive(true);
		} 
		else
		{
			// no more balls 
			// reset the enemies
			EnemyController.EnemiesAlive = 0;

			// this is restarting the current scene 
			// because there is only one in this project
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

			// game is over here
			// resseting the scene is temp

		}
	
	}

}//end class

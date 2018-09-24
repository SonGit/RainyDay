using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AI : MonoBehaviour {

	public float hitTime = 3;
	public float hitTimeCount;

	// Use this for initialization
	public RGMovementController movement;

	public TextMeshPro debugText;

	Collider collider;

	public enum RGState
	{
		WALK,
		HIT,
		START
	}

	public RGState currentState;

	void Start () {

		collider = this.GetComponent<Collider> ();

		movement.GoToRandDirection ();

		currentState = RGState.START;

	}
	
	// Update is called once per frame
	void Update () {
		
		if (currentState == RGState.HIT) {
			hitTimeCount += Time.deltaTime;

			if (hitTimeCount > hitTime) {
				
				hitTimeCount = 0;

				WalkBack ();

			}
		}

		if (currentState == RGState.START) {
			
			debugText.text = "Falling!";

			if (transform.position.y > 0) {
				transform.position += new Vector3 (0, -3 * Time.deltaTime, 0);
				collider.enabled = false;
			} else {
				Walk ();
				collider.enabled = true;
			}
		}

	}


	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "AI") {
			debugText.text = "Hit!";
			OnHit ();
		}
	}

	void OnHit()
	{
		if (currentState != RGState.HIT)
			currentState = RGState.HIT;
		else {
			return;
		}
		
		movement.Stop ();

		ResetTimer ();
	}

	void ResetTimer()
	{
		hitTimeCount = 0;
	}

	void WalkBack()
	{
		debugText.text = "Walk Back!";
		movement.Reverse ();
		movement.Run ();
		currentState = RGState.WALK;
	}

	void Walk()
	{
		debugText.text = "Walk!";
		currentState = RGState.WALK;
		movement.Run ();
	}
		
}

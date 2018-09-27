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

	bool delayedCollider;

	float delayedColliderTimeCount;

	public Arrow[] arrows;

	void Start () {

		arrows = this.GetComponentsInChildren<Arrow> ();

		collider = this.GetComponent<Collider> ();

		currentState = RGState.START;

		TurnOffAllArrow ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (currentState == RGState.HIT) {
			hitTimeCount += Time.deltaTime;

			if (hitTimeCount > hitTime) {
				
				hitTimeCount = 0;

				delayedCollider = true;

				WalkBack ();

				RaycastForward ();
			}
		}

		if (delayedCollider) {

			delayedColliderTimeCount += Time.deltaTime;

			if (delayedColliderTimeCount > .5f) {

				delayedColliderTimeCount = 0;

				delayedCollider = false;

			}

		}

		if (currentState == RGState.START) {
			
			debugText.text = "Falling!";

			if (transform.position.y > 0) {
				transform.position += new Vector3 (0, -3 * Time.deltaTime, 0);
			} else {
				Walk ();
			}
		}
			
		RaycastForward ();
	}

	public bool raycasting = true;

	bool RaycastForward()
	{
		Vector3 raycastDir = Vector3.zero;

		switch (movement.direction) {

		case Direction.UP:
			raycastDir = transform.forward;
			break;
		case Direction.DOWN:
			raycastDir = -transform.forward;
			break;
		case Direction.LEFT:
			raycastDir = -transform.right;
			break;
		case Direction.RIGHT:
			raycastDir = transform.right;
			break;

		}
	
		Debug.DrawRay (transform.position + new Vector3 (0, 1, 0), raycastDir * 9999, Color.red);

		RaycastHit hit;

		// Does the ray intersect any objects excluding the player layer
		if (Physics.Raycast(transform.position + new Vector3(0,1,0), raycastDir, out hit, Mathf.Infinity))
		{
//			Debug.Log(transform.name + " Did Hit " + hit.transform.name + " hit.distance " + hit.distance);
			float distanceToHit = Vector3.Distance (transform.position,hit.transform.position);

			if (hit.distance < 0.17f) {
				OnHit ();
			}

			return true;
		}

		return false;
	}

	bool specialCase;
	float specialCaseTimeCount;
	void OnCollisionStay(Collision collision)
	{
		//if (collision.gameObject.tag == "AI") {

			//hitTime += Random.Range (0,100)/100;
			//print (collision.gameObject.name);

			//AI hitAI = collision.gameObject.GetComponent<AI> ();

			//if (hitAI != null) {
			//	OnHit ();
			//}
		
		//}
	}

	void OnHit()
	{

		if (currentState != RGState.HIT)
			currentState = RGState.HIT;
		else 
		{
			return;
		}

		movement.Stop ();

		debugText.text = "Hit!";

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

	public void TurnOffAllArrow()
	{
		foreach (Arrow arrow in arrows) {

			arrow.gameObject.SetActive (false);

		}
	}

	public void TurnOnArrow(Direction direction)
	{
		foreach (Arrow arrow in arrows) {

			arrow.gameObject.SetActive (arrow.direction == direction);

		}
	}

		
}

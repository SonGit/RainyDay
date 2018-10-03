using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GirlType
{
	RED,
	BLUE,
	YELLOW,
}
public class AI : MonoBehaviour {

	public float hitTime = 3;
	public float hitTimeCount;

	// Use this for initialization
	public RGMovementController movement;

	public TextMeshPro debugText;


	public enum RGState
	{
		WALK,
		WAIT,
		HIT,
		START
	}

	public RGState currentState;

	float waitTimeCount;

	public Arrow[] arrows;

	int layer_mask;

	void Start () {

		arrows = this.GetComponentsInChildren<Arrow> ();

		currentState = RGState.START;

		TurnOffAllArrow ();

		layer_mask = LayerMask.GetMask("Fence","AI");
	}
	
	// Update is called once per frame
	void Update () {
		
		if (currentState == RGState.HIT) {
			hitTimeCount += Time.deltaTime;

			if (hitTimeCount > hitTime) {
				
				hitTimeCount = 0;

				WalkBack ();

				RaycastForward ();
			}
		}

		if (currentState == RGState.WAIT) {
			waitTimeCount += Time.deltaTime;

			if (waitTimeCount > .5f) {

				StopWait ();
				waitTimeCount = 0;
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
		if (Physics.Raycast(transform.position + new Vector3(0,1,0), raycastDir, out hit, Mathf.Infinity,layer_mask))
		{
//			Debug.Log(transform.name + " Did Hit " + hit.transform.name + " hit.distance " + hit.distance);

			float distanceToHit = Vector3.Distance (transform.position,hit.transform.position);

			if (hit.distance < 0.17f) {

				// if the AI is not follow any path, play normal hit
				if (!movement.pathfinding) {
					OnHit ();
				}
				// else, just wait
				else {
					Wait ();
				}

				//Check if hit fence
				Fence fence = hit.transform.GetComponent<Fence>();
				// If hit fence, push it down
				if (fence != null) {
					fence.PopDown ();
					fence.isPopDown = true;
				}
			}

			return true;
		}

		return false;
	}

	void Wait()
	{
		currentState = RGState.WAIT;
		movement.Stop ();
		debugText.text = "Wait!";
		ResetTimer ();
	}

	void StopWait()
	{
		currentState = RGState.WALK;
		movement.Run ();
		debugText.text = "Walk!";
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
		waitTimeCount = 0;
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
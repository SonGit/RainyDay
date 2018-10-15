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
	
	[SerializeField]
	private float hitTime = 3;
	[SerializeField]
	private float hitTimeCount = 0;

	[SerializeField]
	private float dizzyTime = 3;
	[SerializeField]
	private float dizzyTimeCount = 0;

	[SerializeField]
	private float fallTime = 3;
	[SerializeField]
	private float fallTimeCount = 0;

	[SerializeField]
	private float dizzyAnimTime = 3;
	[SerializeField]
	private float dizzyAnimTimeCount = 0;
	public bool isFall;
	public Transform roller;
	public Animator Anim;
	// Use this for initialization
	public RGMovementController movement;

	public TextMeshPro debugText;

	public enum RGState
	{
		WALK,
		WAIT,
		HIT,
		DIZZY_ANIM,
		DIZZY,
		START,
		FALLING,
		FELL,
		GPS
	}

	public RGState currentState;

	float waitTimeCount;

	public Arrow[] arrows;

	int layer_mask;

	Collider collider;

	bool isDead;

	void Awake () {

		arrows = this.GetComponentsInChildren<Arrow> ();

		currentState = RGState.START;

		Anim.SetTrigger ("Idle");

		TurnOffAllArrow ();

		layer_mask = LayerMask.GetMask("Fence","AI","Wall");

		collider = this.GetComponent<Collider> ();

		collider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (isDead)
			return;

		if(currentState == RGState.FALLING){
			
			fallTimeCount += Time.deltaTime;

			if (fallTimeCount > fallTime) {
				Fell ();
				fallTimeCount = 0;
			}
		}
		
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
				transform.position += new Vector3 (0, -.5f * Time.deltaTime, 0);
			} else {
				collider.enabled = true;
				Walk ();

			}
		}

		if (currentState == RGState.DIZZY_ANIM) {

			dizzyAnimTimeCount += Time.deltaTime;

			// if roller is not active, reset its position 
			if (!roller.gameObject.activeInHierarchy) {
				roller.gameObject.SetActive (true);
				roller.localPosition = new Vector3 (0,-1,0);
			}

			// move roller to position
			if (roller.localPosition != Vector3.zero) {
				roller.localPosition = Vector3.MoveTowards (roller.localPosition, Vector3.zero, 10 * Time.deltaTime);
			} 

			// keep spinning it
			roller.localEulerAngles += new Vector3 (0,400,0) * Time.deltaTime;
			movement.mesh.localEulerAngles += new Vector3 (0,600,0) * Time.deltaTime;

			// Once anim dizzy is completed, get to dizzy state
			if (dizzyAnimTimeCount > dizzyAnimTime) {
				dizzyAnimTimeCount = 0;
				movement.Run ();
				movement.Reverse ();
			
				currentState = RGState.DIZZY;
			}

		}

		// allow dizzy to manually rotate mesh
		if (currentState == RGState.DIZZY_ANIM) {
			movement.rotateToTarget = false;
			roller.gameObject.SetActive (true);
		} else {
			movement.rotateToTarget = true;
			roller.gameObject.SetActive (false);
		}
			

		if (currentState == RGState.DIZZY) {
			dizzyAnimTimeCount += Time.deltaTime;

			if (dizzyAnimTimeCount > dizzyTime) {
				dizzyAnimTimeCount = 0;
				Walk ();
			}
		}
			
		RaycastForward ();
	}

	void Fell()
	{
		currentState = RGState.FELL;
		movement.enabled = false;
		isDead = true;
	}

	public void Dizzy()
	{
		if (currentState == RGState.DIZZY || currentState == RGState.START || currentState == RGState.HIT) {
			return;
		}

		if (currentState != RGState.DIZZY_ANIM ) {
			currentState = RGState.DIZZY_ANIM;
			Anim.ResetTrigger ("Walk");
			Anim.SetTrigger ("Dizzy");
		}
		else 
		{
			return;
		}

		debugText.text = "Dizzy ANIM!";
		movement.Stop ();
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
		//	Debug.Log(transform.name + " Did Hit " + hit.transform.name + " hit.distance " + hit.distance);

//			float distanceToHit = Vector3.Distance (transform.position,hit.transform.position);

			if (hit.distance < 0.17f) {

				//Check if hit AI
				if (hit.transform.gameObject.layer == LayerMask.NameToLayer ("AI")) {
					
					// if the AI is not follow any path, play normal hit
					if (currentState != RGState.GPS) {
						OnHit ();
					}
					// else, just wait
					else {
						Wait ();
					}
				}

				//Check if hit fence
				Fence fence = hit.transform.GetComponent<Fence> ();
				// If hit fence, push it down
				if (fence != null) {
					fence.PopDown ();
					fence.isPopDown = true;
				}

				//Check if out-of-bounds
				if (hit.transform.gameObject.layer == LayerMask.NameToLayer ("Wall")) {
					Falling ();
				} 
			} 

			return true;
		}

		if (currentState == RGState.FALLING) {
			Walk ();
		}
		return false;
	}

	public void GPS(Vector3 position)
	{
		currentState = RGState.GPS;
		movement.Run ();
		movement.FollowPath (position);
		ResetTimer ();
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

		if (currentState != RGState.HIT) {
			Anim.ResetTrigger ("Walk");
			Anim.SetTrigger ("Stun");
			currentState = RGState.HIT;
		}
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
		dizzyTimeCount = 0;
		fallTimeCount = 0;
	}

	void WalkBack()
	{
		debugText.text = "Walk Back!";
		Anim.ResetTrigger ("Stun");
		Anim.SetTrigger ("Walk");
		movement.Reverse ();
		movement.Run ();
		currentState = RGState.WALK;
	}

	void Walk()
	{
		Anim.ResetTrigger ("Idle");
		Anim.SetTrigger ("Walk");
		debugText.text = "Walk!";
		currentState = RGState.WALK;
		movement.Run ();
		ResetTimer ();
	}

	void Falling()
	{
		if (currentState == RGState.FALLING || currentState == RGState.FELL) {
			return;
		} else {
			currentState = RGState.FALLING;
		}
		print ("FALLING");

		movement.Stop();
		Anim.ResetTrigger ("Walk");
		Anim.ResetTrigger ("Dizzy");
		Anim.SetTrigger ("StepFall");
	}

	void StopFalling()
	{
		currentState = RGState.WALK;
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
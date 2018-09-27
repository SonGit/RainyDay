using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class AI : MonoBehaviour {

	public float hitTime = 3;
	public float hitTimeCount;
	public int rand;
	// Use this for initialization
	public RGMovementController movement;

	public TextMeshPro debugText;
	public bool isAuto;


	Collider collider;
	Color meshColor;

	public enum RGState
	{
		WALK,
		HIT,
		START,
		AUTO
	}

	public enum MeshColor
	{
		Red,
		Blue,
		Green
	}

	public RGState currentState;
	void OnMouseDown() {

		//Fetch the Renderer from the GameObject
		Selector.instance.Select(transform);

	}
	void Start () {

		collider = this.GetComponent<Collider> ();

		movement.GoToRandDirection ();

		currentState = RGState.START;
		ChangeColor ();


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

			if (transform.position.y > 0.3f) {
				transform.position += new Vector3 (0, -1 * Time.deltaTime, 0);
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
	void ChangeColor()
	{
		rand = Random.Range (0, 3);
		SkinnedMeshRenderer rend = GetComponentInChildren<SkinnedMeshRenderer> ();

		if (rand == 0) {
			meshColor = new Color32 (109, 18, 0, 255);
		}
		if (rand == 1) {
			meshColor = new Color32 (0, 117, 181, 255);
		}
		if (rand == 2) {
			meshColor = new Color32 (9, 135, 0, 255);
		}
		rend.materials[0].color = meshColor;
	}

}

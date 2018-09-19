using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {


	public float rotSpeed = 150f;

	public float moveSpeed = .5f;

	public bool isMoving;

	public GameObject lastTile;
	public GameObject currentTile;

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = this.GetComponentInChildren<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(isMoving)
		transform.position += transform.forward * moveSpeed * Time.deltaTime;

		if (Input.GetKeyDown (KeyCode.D)) {
			TurnRight ();
		}
	}

	public void TurnRight()
	{
		StartCoroutine (TurnRight_async());
	}

	IEnumerator TurnRight_async()
	{
		isMoving = false;
		Vector3 currentTile = new Vector3((int)transform.position.x,(int)transform.position.y,(int)transform.position.z);

		Vector3 nextTilePos = currentTile + new Vector3(1,0.5f,0);

		while (Vector3.Distance (transform.position, nextTilePos) > 0.01f) {
			transform.position = Vector3.MoveTowards (transform.position,nextTilePos,.5f*Time.deltaTime);

			transform.LookAt (nextTilePos);
			yield return new WaitForEndOfFrame ();
		}

		transform.position = nextTilePos;

		Quaternion targetRot = Quaternion.Euler( new Vector3 (0,90,0));

		while (transform.rotation != targetRot) {

			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
			yield return new WaitForEndOfFrame ();
		}
		isMoving = true;
		yield return new WaitForEndOfFrame ();
	}

	IEnumerator MoveToRightTile()
	{
		//isMoving = false;
		Vector3 currentTile = new Vector3((int)transform.position.x,(int)transform.position.y,(int)transform.position.z);

		Vector3 nextTilePos = currentTile + new Vector3(1,0.5f,0);


		while (Vector3.Distance (transform.position, nextTilePos) > 0.01f) {
			print (Vector3.Distance (transform.position, nextTilePos));
			transform.position = Vector3.MoveTowards (transform.position,nextTilePos,2*Time.deltaTime);
			yield return new WaitForEndOfFrame ();
		}
		print (nextTilePos);
		transform.position = nextTilePos;
	}

	public void Bump()
	{
		StartCoroutine (Bump_async());
	}

	IEnumerator Bump_async()
	{
		isMoving = false;
		animator.SetTrigger ("BumpTrigger");
		yield return new WaitForSeconds (1);

		//transform.position = lastTile.transform.position;



		Quaternion targetRot = Quaternion.Euler(transform.eulerAngles + new Vector3 (0,-180,0));

		while (transform.rotation != targetRot) {

			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
			print (transform.rotation + "         " + targetRot);
			yield return new WaitForEndOfFrame ();
		}
		print ("FINISH");UnityEditor.TransformUtils.GetInspectorRotation(transform); 
		isMoving = true;
	}

	void OnCollisionEnter(Collision collision)
	{
		if (lastTile == null) {
			lastTile = collision.gameObject;
		}

		lastTile = currentTile;
		currentTile = collision.gameObject;

	}
}

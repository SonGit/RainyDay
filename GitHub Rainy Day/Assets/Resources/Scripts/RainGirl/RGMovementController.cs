using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGMovementController : MonoBehaviour {

	public enum RGDirection
	{
		UP,
		DOWN,
		LEFT,
		RIGHT
	}

	[SerializeField]
	private Transform mesh;

	// Public movement vars
	public float rotSpeed;
	public float speed;
	public RGDirection direction;

	// Private movement vars
	private bool changedDir;
	private Vector3 targetTile;
	private Vector3 targetEulerAngle;
	private float tileNo;
	[SerializeField]
	private Vector3 currentTile;

	// Use this for initialization
	void Start () {
		targetTile = new Vector3 (Mathf.Round(transform.position.x),0,Mathf.Round(transform.position.z));
		targetEulerAngle = new Vector3 (0,0,0);
		tileNo = 1;
	}
		
	// Update is called once per frame
	void Update () {
		
		currentTile = new Vector3 (Mathf.Round(transform.position.x),0,Mathf.Round(transform.position.z));

		if (Input.GetKeyDown (KeyCode.W)) {
			targetTile = currentTile + new Vector3 (0,0,tileNo);
			changedDir = true;

			direction = RGDirection.UP;
		}

		if (Input.GetKeyDown (KeyCode.S)) {
			targetTile = currentTile + new Vector3 (0,0,-tileNo);
			changedDir = true;
		
			direction = RGDirection.DOWN;
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			targetTile = currentTile + new Vector3 (-tileNo,0,0);
			changedDir = true;

			direction = RGDirection.LEFT;
		}

		if (Input.GetKeyDown (KeyCode.D)) {
			targetTile = currentTile + new Vector3 (tileNo,0,0);
			changedDir = true;
		
			direction = RGDirection.RIGHT;
		}

		if (changedDir) {
			float distanceToTargetTile = Vector3.Distance (transform.position, targetTile);

			if (distanceToTargetTile <= 0) {
				changedDir = false;
			} else {
				transform.position = Vector3.MoveTowards (transform.position, targetTile, speed * Time.deltaTime);
			}

		} else {
			if (direction == RGDirection.UP) {
				transform.position += new Vector3(0,0,1) * Time.deltaTime * speed;
			}
			if (direction == RGDirection.DOWN) {
				transform.position += new Vector3(0,0,-1) * Time.deltaTime * speed;
			}
			if (direction == RGDirection.LEFT) {
				transform.position += new Vector3(-1,0,0) * Time.deltaTime * speed;
			}
			if (direction == RGDirection.RIGHT) {
				transform.position += new Vector3(1,0,0) * Time.deltaTime * speed;
			}
		}
			
		if (direction == RGDirection.UP) {
			targetEulerAngle = new Vector3 (0,0,0);
		}
		if (direction == RGDirection.DOWN) {
			targetEulerAngle = new Vector3 (0,180,0);
		}
		if (direction == RGDirection.LEFT) {
			targetEulerAngle = new Vector3 (0,-90,0);
		}
		if (direction == RGDirection.RIGHT) {
			targetEulerAngle = new Vector3 (0,90,0);
		}

		if (mesh != null)
			mesh.localRotation = Quaternion.Slerp (mesh.localRotation, Quaternion.Euler (targetEulerAngle), Time.deltaTime * rotSpeed);
		else {
			Debug.Log ("No Mesh Founded!");
		}

	}
		
}

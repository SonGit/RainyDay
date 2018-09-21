using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGMovementController : MonoBehaviour {

	public Transform mesh;

	public Vector3 currentTile;

	public float tileNo;

	public float speed;

	public float rotSpeed;

	public bool isMoving;

	public Vector3 targetTile;

	bool changedDir;

	// Use this for initialization
	void Start () {

		isMoving = true;
		targetTile = new Vector3 (Mathf.Round(transform.position.x),0,Mathf.Round(transform.position.z));
	}
	int dirType = 0;
	// Update is called once per frame
	void Update () {
		
		currentTile = new Vector3 (Mathf.Round(transform.position.x),0,Mathf.Round(transform.position.z));

		if (Input.GetKeyDown (KeyCode.W)) {
			targetTile = currentTile + new Vector3 (0,0,tileNo);
			changedDir = true;

			dirType = 0;
		}

		if (Input.GetKeyDown (KeyCode.S)) {
			targetTile = currentTile + new Vector3 (0,0,-tileNo);
			changedDir = true;
		
			dirType = 1;
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			targetTile = currentTile + new Vector3 (-tileNo,0,0);
			changedDir = true;

			dirType = 2;
		}

		if (Input.GetKeyDown (KeyCode.D)) {
			targetTile = currentTile + new Vector3 (tileNo,0,0);
			changedDir = true;
		
			dirType = 3;
		}

		if (changedDir) {
			float distanceToTargetTile = Vector3.Distance (transform.position, targetTile);

			if (distanceToTargetTile <= 0) {
				changedDir = false;
			} else {
				transform.position = Vector3.MoveTowards (transform.position, targetTile, speed * Time.deltaTime);
			}
		} else {
			if (dirType == 0) {
				transform.position += new Vector3(0,0,1) * Time.deltaTime * speed;
			}
			if (dirType == 1) {
				transform.position += new Vector3(0,0,-1) * Time.deltaTime * speed;
			}
			if (dirType == 2) {
				transform.position += new Vector3(-1,0,0) * Time.deltaTime * speed;
			}
			if (dirType == 3) {
				transform.position += new Vector3(1,0,0) * Time.deltaTime * speed;
			}
		}
			
		if (dirType == 0) {
			mesh.localEulerAngles = new Vector3 (0,0,0);
		}
		if (dirType == 1) {
			mesh.localEulerAngles = new Vector3 (0,180,0);
		}
		if (dirType == 2) {
			mesh.localEulerAngles = new Vector3 (0,-90,0);
		}
		if (dirType == 3) {
			mesh.localEulerAngles = new Vector3 (0,90,0);
		}

	}

	IEnumerator LookUp(int dir)
	{
		Quaternion targetRot = Quaternion.Euler( new Vector3 (0,0,0));

		switch (dir)
		{
		case 0:
			targetRot = Quaternion.Euler( new Vector3 (0,0,0));
			break;
		case 1:
			targetRot = Quaternion.Euler( new Vector3 (0,-180,0));
			break;
		case 2:
			targetRot = Quaternion.Euler( new Vector3 (0,-90,0));
			break;
		case 3:
			targetRot = Quaternion.Euler( new Vector3 (0,90,0));
			break;
		default:
			targetRot = Quaternion.Euler( new Vector3 (0,0,0));
			break;
		}

		while (mesh.rotation != targetRot) {

			mesh.rotation = Quaternion.RotateTowards(mesh.rotation, targetRot, rotSpeed * Time.deltaTime);
			yield return new WaitForEndOfFrame ();
		}

		//isMoving = true;
	}

	public float MyRound(float value) {
		if (value % 0.5f == 0)
			return Mathf.Ceil(value);
		else
			return Mathf.Floor(value);
	}
}

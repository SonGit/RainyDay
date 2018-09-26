using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public enum Direction
{
	UP,
	DOWN,
	LEFT,
	RIGHT
}

public class RGMovementController : MonoBehaviour {
	public Animator Anim_hit;

	[SerializeField]
	public Transform mesh;

	// Public movement vars
	public float rotSpeed;
	public float speed;
	public Direction direction;

	// Private movement vars
	public bool changedDir;
	private Vector3 targetTile;
	private Vector3 targetEulerAngle;
	private float tileNo;
	private int startDir;

	[SerializeField]
	private Vector3 currentTile;

	public void GoToRandDirection()
	{
		GoToDirection ((Direction)Random.Range(0,4));
	}

	public void GoToDirection(Direction dir)
	{
		switch (dir) {

		case Direction.UP:
			targetTile = currentTile + new Vector3 (0, 0, tileNo);
			changedDir = true;
			direction = Direction.UP;
			break;
		case Direction.DOWN:
			targetTile = currentTile + new Vector3 (0, 0, -tileNo);
			changedDir = true;
			direction = Direction.DOWN;
			break;
		case Direction.LEFT:
			targetTile = currentTile + new Vector3 (-tileNo, 0, 0);
			changedDir = true;
			direction = Direction.LEFT;
			break;
		case Direction.RIGHT:
			targetTile = currentTile + new Vector3 (tileNo, 0, 0);
			changedDir = true;
			direction = Direction.RIGHT;
			break;
		}

	}

	void Move () {
		
		currentTile = new Vector3 (Mathf.Round (transform.position.x), 0, Mathf.Round (transform.position.z));

		if (changedDir) {
			float distanceToTargetTile = Vector3.Distance (transform.position, targetTile);

			if (distanceToTargetTile <= 0) {
				changedDir = false;
			} else {
				transform.position = Vector3.MoveTowards (transform.position, targetTile, speed * Time.deltaTime);
			}

		} else {
			if (direction == Direction.UP) {
				transform.position += new Vector3 (0, 0, 1) * Time.deltaTime * speed;
			}
			if (direction == Direction.DOWN) {
				transform.position += new Vector3 (0, 0, -1) * Time.deltaTime * speed;
			}
			if (direction == Direction.LEFT) {
				transform.position += new Vector3 (-1, 0, 0) * Time.deltaTime * speed;
			}
			if (direction == Direction.RIGHT) {
				transform.position += new Vector3 (1, 0, 0) * Time.deltaTime * speed;
			}
		}

		if (direction == Direction.UP) {
			targetEulerAngle = new Vector3 (0, 0, 0);
		}
		if (direction == Direction.DOWN) {
			targetEulerAngle = new Vector3 (0, 180, 0);
		}
		if (direction == Direction.LEFT) {
			targetEulerAngle = new Vector3 (0, -90, 0);
		}
		if (direction == Direction.RIGHT) {
			targetEulerAngle = new Vector3 (0, 90, 0);
		}

		if (mesh.localRotation != Quaternion.Euler (targetEulerAngle)) {
			isRotated = false;
		} else {
			isRotated = true;
		}

		if (mesh != null)
			mesh.localRotation = Quaternion.Slerp (mesh.localRotation, Quaternion.Euler (targetEulerAngle), Time.deltaTime * rotSpeed);
		else {
			Debug.Log ("No Mesh Founded!");
		}
	}

	public bool isRotated;

	void Start () {
		targetTile = new Vector3 (Mathf.Round(transform.position.x),0,Mathf.Round(transform.position.z));
		targetEulerAngle = new Vector3 (0,0,0);
		tileNo = 1;
		currentTile = new Vector3 (Mathf.Round (transform.position.x), 0, Mathf.Round (transform.position.z));
	}
	// Update is called once per frame
	void Update () {

		if (isRun) {

			if (Input.GetKeyDown (KeyCode.W)) {
				GoToDirection (Direction.UP);
			}

			if (Input.GetKeyDown (KeyCode.S)) {
				GoToDirection (Direction.DOWN);
			}

			if (Input.GetKeyDown (KeyCode.A)) {
				GoToDirection (Direction.LEFT);
			}

			if (Input.GetKeyDown (KeyCode.D)) {
				GoToDirection (Direction.RIGHT);
			}

			Move ();
		}
	
	}
		
	private bool isRun ;

	public void Run()
	{
		isRun = true;
	}

	public void Stop()
	{
		isRun = false;
	}

	public void Reverse()
	{
		switch (direction) {

		case Direction.DOWN:
			targetTile = currentTile + new Vector3 (0, 0, tileNo);
			changedDir = true;
			direction = Direction.UP;
			break;
		case Direction.UP:
			targetTile = currentTile + new Vector3 (0, 0, -tileNo);
			changedDir = true;
			direction = Direction.DOWN;
			break;
		case Direction.RIGHT:
			targetTile = currentTile + new Vector3 (-tileNo, 0, 0);
			changedDir = true;
			direction = Direction.LEFT;
			break;
		case Direction.LEFT:
			targetTile = currentTile + new Vector3 (tileNo, 0, 0);
			changedDir = true;
			direction = Direction.RIGHT;
			break;
		}
	}
}
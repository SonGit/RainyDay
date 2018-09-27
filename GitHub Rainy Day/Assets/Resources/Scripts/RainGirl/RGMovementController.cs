using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
public class RGMovementController : MonoBehaviour {
	
	public Animator Anim_hit;
	public enum RGDirection
	{
		UP,
		DOWN,
		LEFT,
		RIGHT
	}

	[SerializeField]
	public Transform mesh;

	// Public movement vars
	public float rotSpeed;
	public float speed;
	public RGDirection direction;

	// Private movement vars
	public bool changedDir;
	private Vector3 targetTile;
	private Vector3 targetEulerAngle;
	private float tileNo;
	private int startDir;

	[SerializeField]
	private Vector3 currentTile;

	// Use this for initialization
	void OnEnable() {

	}

	public void GoToRandDirection()
	{
		GoToDirection ((RGDirection)Random.Range(0,4));
	}

	public void GoToDirection(RGDirection dir)
	{
		switch (dir) {

		case RGDirection.UP:
			targetTile = currentTile + new Vector3 (0, 0, tileNo);
			changedDir = true;
			direction = RGDirection.UP;
			break;
		case RGDirection.DOWN:
			targetTile = currentTile + new Vector3 (0, 0, -tileNo);
			changedDir = true;
			direction = RGDirection.DOWN;
			break;
		case RGDirection.LEFT:
			targetTile = currentTile + new Vector3 (-tileNo, 0, 0);
			changedDir = true;
			direction = RGDirection.LEFT;
			break;
		case RGDirection.RIGHT:
			targetTile = currentTile + new Vector3 (tileNo, 0, 0);
			changedDir = true;
			direction = RGDirection.RIGHT;
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
			if (direction == RGDirection.UP) {
				transform.position += new Vector3 (0, 0, 1) * Time.deltaTime * speed;
			}
			if (direction == RGDirection.DOWN) {
				transform.position += new Vector3 (0, 0, -1) * Time.deltaTime * speed;
			}
			if (direction == RGDirection.LEFT) {
				transform.position += new Vector3 (-1, 0, 0) * Time.deltaTime * speed;
			}
			if (direction == RGDirection.RIGHT) {
				transform.position += new Vector3 (1, 0, 0) * Time.deltaTime * speed;
			}
		}

		if (direction == RGDirection.UP) {
			targetEulerAngle = new Vector3 (0, 0, 0);
		}
		if (direction == RGDirection.DOWN) {
			targetEulerAngle = new Vector3 (0, 180, 0);
		}
		if (direction == RGDirection.LEFT) {
			targetEulerAngle = new Vector3 (0, -90, 0);
		}
		if (direction == RGDirection.RIGHT) {
			targetEulerAngle = new Vector3 (0, 90, 0);
		}

		if (mesh != null)
			mesh.localRotation = Quaternion.Slerp (mesh.localRotation, Quaternion.Euler (targetEulerAngle), Time.deltaTime * rotSpeed);
		else {
			Debug.Log ("No Mesh Founded!");
		}
	}
	void Start () {
		targetTile = new Vector3 (Mathf.Round(transform.position.x),0,Mathf.Round(transform.position.z));
		targetEulerAngle = new Vector3 (0,0,0);
		tileNo = 1;
	}
	// Update is called once per frame
	void Update () {

		if (isRun) {

			if (Input.GetKeyDown (KeyCode.W)) {
				GoToDirection (RGDirection.UP);
			}

			if (Input.GetKeyDown (KeyCode.S)) {
				GoToDirection (RGDirection.DOWN);

			}

			if (Input.GetKeyDown (KeyCode.A)) {
				GoToDirection (RGDirection.LEFT);
			}

			if (Input.GetKeyDown (KeyCode.D)) {
				GoToDirection (RGDirection.RIGHT);
			}

			Move ();
		}
		if(transform.position.x<-0.5f || transform.position.x>5.5f|| transform.position.z>5.5f|| transform.position.z<-0.5f)
		{
			Destroy(gameObject);
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

		case RGDirection.DOWN:
			targetTile = currentTile + new Vector3 (0, 0, tileNo);
			changedDir = true;
			direction = RGDirection.UP;
			break;
		case RGDirection.UP:
			targetTile = currentTile + new Vector3 (0, 0, -tileNo);
			changedDir = true;
			direction = RGDirection.DOWN;
			break;
		case RGDirection.RIGHT:
			targetTile = currentTile + new Vector3 (-tileNo, 0, 0);
			changedDir = true;
			direction = RGDirection.LEFT;
			break;
		case RGDirection.LEFT:
			targetTile = currentTile + new Vector3 (tileNo, 0, 0);
			changedDir = true;
			direction = RGDirection.RIGHT;
			break;
		}
	}
}
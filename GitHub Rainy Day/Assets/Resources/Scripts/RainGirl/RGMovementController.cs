﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using Pathfinding;
using System.Linq;

public enum Direction
{
	UP,
	DOWN,
	LEFT,
	RIGHT,
	NONE
}

public class RGMovementController : MonoBehaviour {

	[SerializeField]
	public Transform mesh;

	// Public movement vars
	public float rotSpeed;
	public float speed;
	public Direction direction;

	// Private movement vars
	public bool changedDir;
	public Vector3 targetTile;
	public Vector3 targetEulerAngle;
	private float tileNo;

	// Pathfinding stuffs
	private Seeker seeker;
	Vector3 currentWaypoint;
	int currentWaypointNo;
	Path path;

	//public List<Vector3> remainingPath;

	[SerializeField]
	private Vector3 currentTile;

	// Set this to false to manually rotate mesh
	public bool rotateToTarget = true;

	private AI ai;

	public void GoToRandDirection()
	{
		GoToDirection ((Direction)Random.Range(0,4));
	}

	public void GoToDirection(Direction dir)
	{
		switch (dir) {

		case Direction.UP:
			targetTile = currentTile + new Vector3 (0, 0, tileNo);
			break;
		case Direction.DOWN:
			targetTile = currentTile + new Vector3 (0, 0, -tileNo);
			break;
		case Direction.LEFT:
			targetTile = currentTile + new Vector3 (-tileNo, 0, 0);
			break;
		case Direction.RIGHT:
			targetTile = currentTile + new Vector3 (tileNo, 0, 0);
			break;
		}

		direction = dir;

		GoTo (targetTile);
	}
	public void GoReverse(Direction R)
	{
		switch (R) {

		case Direction.DOWN:
			targetTile = currentTile + new Vector3 (0, 0, tileNo);
			break;
		case Direction.UP:
			targetTile = currentTile + new Vector3 (0, 0, -tileNo);
			break;
		case Direction.RIGHT:
			targetTile = currentTile + new Vector3 (-tileNo, 0, 0);
			break;
		case Direction.LEFT:
			targetTile = currentTile + new Vector3 (tileNo, 0, 0);
			break;
		}

		direction = R;

		GoTo (targetTile);
	}
		
	void Start () {
		
		targetTile = new Vector3 (Mathf.Round(transform.position.x),0,Mathf.Round(transform.position.z));
		targetEulerAngle = new Vector3 (0,0,0);
		currentTile = targetTile;
		tileNo = 10;

		GoToDirection (direction);
		Run ();
	}

	void Awake()
	{
		seeker = this.GetComponent<Seeker> ();
		ai = this.GetComponent<AI> ();
	}

	// Update is called once per frame
	void Update () {

		if (ai.currentState == AI.RGState.START) {
			return;
		}
		
			currentTile = new Vector3 (Mathf.Round (transform.position.x), 0, Mathf.Round (transform.position.z));

			if (Vector3.Distance (transform.position, currentWaypoint) > 0.01f) {
					
				transform.position = Vector3.MoveTowards (transform.position, currentWaypoint, Time.deltaTime * speed);

				Vector3 dir = (currentWaypoint - transform.position).normalized;
				dir = new Vector3 (Mathf.Round (dir.x), 0, Mathf.Round (dir.z));

				if (dir.z > 0 ) {
					direction = Direction.UP;
				}

				if (dir.z < 0 ) {
					direction = Direction.DOWN;
				}

				if (dir.x < 0 ) {
					direction = Direction.LEFT;
				}

				if (dir.x > 0 ) {
					direction = Direction.RIGHT;
				}

			if (rotateToTarget) {
				
				Vector3 waypoint = new Vector3 (currentWaypoint.x, 0, currentWaypoint.z);
				Vector3 currentPos = new Vector3 (transform.position.x, 0, transform.position.z);

				if ((waypoint  - currentPos) != Vector3.zero) {
					
					var rotation = Quaternion.LookRotation(waypoint - currentPos);
					mesh.localRotation = Quaternion.Lerp(mesh.localRotation, Quaternion.LookRotation((currentWaypoint - transform.position)), Time.deltaTime * rotSpeed);

				}
				//if (rotation.x >= 0)
				//mesh.localRotation = Quaternion.Slerp(mesh.localRotation, rotation, Time.deltaTime * rotSpeed);

			}

				} else {


//				if (ai.currentState == AI.RGState.DIZZY && currentWaypointNo == 1) {
//						GoReverse (direction);
//				}

				currentWaypointNo++;

				if (currentWaypointNo >= path.vectorPath.Count) 
				{
					if(ai.currentState == AI.RGState.GPS)
						OnArrivedHome ();
				} 
				else 
				{
					currentWaypoint = path.vectorPath[currentWaypointNo];
				}
				
//				remainingPath.Clear ();
//
//				for (int i = currentWaypointNo - 1; i < path.vectorPath.Count ; i++) {
//					remainingPath.Add (path.vectorPath[i]);
				}
						
			}

//		if (remainingPath.Count > 1) {
//			remainingPath [0] = transform.position;
//		
//		}
//	}

	void OnArrivedHome()
	{
		if (ai.gpsFX != null) {
			ai.gpsFX.transform.SetParent(null);
			Destroy (ai.gpsFX);
		}
		Destroy (gameObject,0.5f);
	}
	public void GPS()
	{
		speed = 2;
	}
	public void Run()
	{
		speed = 1;
	}

	public void Stop()
	{
		speed = 0;
	}

	public void Reverse()
	{
		switch (direction) {

		case Direction.DOWN:
			targetTile = currentTile + new Vector3 (0, 0, tileNo);
			direction = Direction.UP;
			break;
		case Direction.UP:
			targetTile = currentTile + new Vector3 (0, 0, -tileNo);
			direction = Direction.DOWN;
			break;
		case Direction.RIGHT:
			targetTile = currentTile + new Vector3 (-tileNo, 0, 0);
			direction = Direction.RIGHT;
			break;
		case Direction.LEFT:
			targetTile = currentTile + new Vector3 (tileNo, 0, 0);
			direction = Direction.LEFT;
			break;
		}

		GoTo (targetTile);
	}
		
	public void GoTo(Vector3 target)
	{
		Run ();
		// Graph without pathfinding
		seeker.graphMask = 2;
		seeker.StartPath(transform.position, target, OnPathComplete);
	}

	public void FollowPath(Vector3 target)
	{
		//Run ();
		// Graph with pathfinding
		seeker.graphMask = 1;
		seeker.StartPath(transform.position, target, OnPathComplete);
	}


	private void OnPathComplete (Path p) {
//		Debug.Log("Yay, we got a path back. Did it have an error? " + p.error);

		if (!p.error) {
			path = p;
			currentWaypoint = transform.position;
			currentWaypointNo = 0;

		//	remainingPath = new List<Vector3>(p.vectorPath);

		//	GameObject lineGameObject = (GameObject)Instantiate (gpsLinePrefab, Vector3.zero, gpsLinePrefab.transform.rotation);
		//	gpsDrawLine = lineGameObject.GetComponent<GRDrawGPSLine> ();

		//	if(gpsDrawLine != null)
			//	gpsDrawLine.DrawPath (remainingPath,transform);

		} else {
			Debug.Log ("No Path!");
		}


	}


}
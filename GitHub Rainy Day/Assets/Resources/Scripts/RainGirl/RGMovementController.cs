using System.Collections;
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


	// Pathfinding stuffs
	private Seeker seeker;
	Vector3 currentWaypoint;
	int currentWaypointNo;
	Path path;
	public bool pathfinding;
	public List<Vector3> remainingPath;

	[SerializeField]
	private Vector3 currentTile;

	public GRDrawGPSLine gpsDrawLine;

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


		if (mesh != null) {
			mesh.localRotation = Quaternion.Slerp (mesh.localRotation, Quaternion.Euler (targetEulerAngle), Time.deltaTime * rotSpeed);
		}

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
		seeker = this.GetComponent<Seeker> ();
//		FollowPath ();
	}
	// Update is called once per frame
	void Update () {
		
		currentTile = new Vector3 (Mathf.Round (transform.position.x), 0, Mathf.Round (transform.position.z));



			if (!pathfinding) {
				Move ();
			} else {
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

					var rotation = Quaternion.LookRotation(currentWaypoint - transform.position);
					mesh.localRotation = Quaternion.Slerp(mesh.localRotation, rotation, Time.deltaTime * rotSpeed);

				} else {

					currentWaypointNo++;

					if (currentWaypointNo >= path.vectorPath.Count) 
					{
						pathfinding = false;
						Destroy (gameObject);
					} 
					else 
					{
						currentWaypoint = path.vectorPath[currentWaypointNo];
					}
			
					remainingPath.Clear ();

					for (int i = currentWaypointNo - 1; i < path.vectorPath.Count ; i++) {
						remainingPath.Add (path.vectorPath[i]);
					}
						
				}

				remainingPath [0] = transform.position;
				if(gpsDrawLine != null)
				gpsDrawLine.DrawPath (remainingPath);
			}
		

	
	}
		
	private bool isRun ;

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
		

	public void FollowPath(Vector3 target)
	{
		seeker.StartPath(transform.position, target, OnPathComplete);
	}

	private void OnPathComplete (Path p) {
		Debug.Log("Yay, we got a path back. Did it have an error? " + p.error);

		if (!p.error) {
			path = p;
			currentWaypoint = transform.position;
			currentWaypointNo = 0;
			pathfinding = true;

			remainingPath = new List<Vector3>(p.vectorPath);

			if(gpsDrawLine != null)
			gpsDrawLine.DrawPath (remainingPath);

		} else {
			Debug.Log ("No Path!");
		}


	}
}
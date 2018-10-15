using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGPlayerInput : MonoBehaviour {

	public AI chosenAI;

	public Vector3 dir;

	public Direction chosenDirection;

	public GameObject cube;



	// Use this for initialization
	void Start () {
		dir = Vector3.zero;
		layer_mask = LayerMask.GetMask("Plane","AI");
	}

	private Vector3 fromV3;
	private Vector3 toV3;

	private Vector2 fromV2;
	private Vector2 toV2;

	[SerializeField]
	private float minDistanceToChoose = 1;

	bool mouseDown;

	int layer_mask ;

	bool chosen;

	// Update is called once per frame
	void Update () {

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		// Save the info
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, Mathf.Infinity, layer_mask)) {

			// Normalize the hit point - no vertical movement
			hit.point = new Vector3 (hit.point.x,0,hit.point.z);

			if (Input.GetMouseButtonDown (0)) {
				// Debug
				cube.transform.position = hit.point;

				// If raycast hit an AI, choose it
				// otherwise, scan the area around ray hit point to get nearest AI

				chosenAI = hit.transform.GetComponent<AI> ();

				if (chosenAI != null) {

					//if (chosenAI.movement.pathfinding)
						//return;

					if (!CheckValidState (chosenAI.currentState))
						return;
					
					// set default direction
					chosenAI.TurnOnArrow (chosenAI.movement.direction);
					chosenDirection = chosenAI.movement.direction;
					chosen = true;

				} else {
					
					// Get all AI objects
					GameObject[] gos = GameObject.FindGameObjectsWithTag ("AI");

					if (gos.Length != 0) {
						// Get the closest AI to ray hit point
						Transform closest = GetClosestEnemy (hit.point,gos);

						float distance = Vector3.Distance (hit.point,closest.position);
						// If the AI is within distance, select it
						if (distance < .8f) {

//							print ("You choose  "+closest.name +" distance "+ distance);

							chosenAI = closest.GetComponent<AI> ();

							if (chosenAI != null) {
								//
								//if (chosenAI.movement.pathfinding)
									//return;

								if (!CheckValidState (chosenAI.currentState))
									return;
							
								// set default direction
								chosenAI.TurnOnArrow(chosenAI.movement.direction);
								chosenDirection = chosenAI.movement.direction;
								chosen = true;

							}
						}
					}
				}
			}

			if (chosen) {

				float dist = Vector3.Distance (hit.point,chosenAI.transform.position);

				// Find the direction to move in
				dir = hit.point - chosenAI.transform.position;

				// Make it so that its only in x and y axis
				dir.y = 0; // No vertical movement

				Debug.DrawLine (chosenAI.transform.position,chosenAI.transform.position + chosenAI.transform.forward * 2,Color.green);
				Debug.DrawLine (chosenAI.transform.position,chosenAI.transform.position + dir * 2,Color.green);

				if (dist > minDistanceToChoose) {
					fromV3 = (chosenAI.transform.position + chosenAI.transform.forward * 2) - chosenAI.transform.position;
					toV3 = (chosenAI.transform.position + dir * 2) - chosenAI.transform.position;

					fromV2 = new Vector2 (fromV3.x,fromV3.z);
					toV2 = new Vector2 (toV3.x,toV3.z);

					float angle = (Mathf.Abs( 360 - Angle360(fromV2,toV2)));

					if (  (angle > 315 && angle <= 360) || (angle > 0 && angle < 45)) {
						//print ("UP");
						chosenDirection = Direction.UP;
					}

					if( (angle > 45) && (angle < 135))
					{
						//print ("RIGHT");
						chosenDirection = (Direction.RIGHT);
					}

					if( (angle > 135) && (angle < 225))
					{
						//print ("DOWN");
						chosenDirection = (Direction.DOWN);
					}

					if( (angle > 225) && (angle < 315))
					{
						//print ("LEFT");
						chosenDirection = (Direction.LEFT);
					}

					chosenAI.TurnOnArrow(chosenDirection);
				}

			}


		}

		if (Input.GetMouseButtonUp (0)) {

			if (chosenAI != null) {
				// If the AI is playing hit animation, ignore
				if (chosenAI.currentState == AI.RGState.HIT) {
					return;
				}

				chosenAI.movement.GoToDirection (chosenDirection);
				chosenAI.TurnOffAllArrow ();
				chosenAI = null;
				chosen = false;
			}

		}
				
	}

	float SignedAngleBetween(Vector3 a, Vector3 b)
	{
		// Angle between -1 and +1
		float fAngle = Vector3.Cross(a.normalized,b.normalized).y;

		// Convert to -180 to +180 degrees
		fAngle *= 180.0f;

		return( fAngle );
	}

	public static float SignedAngle( Vector3 from, Vector3 to, Vector3 normal )
	{
		// angle in [0,180]
		float angle = Vector3.Angle( from, to );
		float sign = Mathf.Sign( Vector3.Dot( normal, Vector3.Cross( from, to ) ) );

		float result =  angle * sign;

		if (result < 0) {

			result = result + Mathf.Abs (180 - result);

		}
		return result;
	}

	public static float Angle360(Vector2 p1, Vector2 p2, Vector2 o = default(Vector2))
	{
		Vector2 v1, v2;
		if (o == default(Vector2))
		{
			v1 = p1.normalized;
			v2 = p2.normalized;
		}
		else
		{
			v1 = (p1 - o).normalized;
			v2 = (p2 - o).normalized;
		}

		float angle = Vector2.Angle(v1, v2);
		return Mathf.Sign(Vector3.Cross(v1, v2).z) < 0 ? (360 - angle) % 360 : angle;
	}

	Transform GetClosestEnemy (Vector3 epicenter, GameObject[] enemies)
	{
		Transform bestTarget = null;
		float closestDistanceSqr = Mathf.Infinity;
		Vector3 currentPosition = epicenter;
		foreach(GameObject potentialTarget in enemies)
		{
			Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
			float dSqrToTarget = directionToTarget.sqrMagnitude;
			if(dSqrToTarget < closestDistanceSqr)
			{
				closestDistanceSqr = dSqrToTarget;
				bestTarget = potentialTarget.transform;
			}
		}

		return bestTarget;
	}


	bool CheckValidState(AI.RGState inputState)
	{
		
		if (inputState == AI.RGState.DIZZY ||
		   inputState == AI.RGState.DIZZY_ANIM ||
		   inputState == AI.RGState.HIT ||
		   inputState == AI.RGState.START ||
		   inputState == AI.RGState.WAIT ||
		   inputState == AI.RGState.FELL) {
			return false;
		}
		return true;
	}
}

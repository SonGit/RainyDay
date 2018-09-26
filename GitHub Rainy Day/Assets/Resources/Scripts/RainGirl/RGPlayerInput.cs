using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGPlayerInput : MonoBehaviour {

	public AI ai;

	public Vector3 dir;

	public Arrow[] arrows;

	public Direction chosenDirection;

	// Use this for initialization
	void Start () {
		dir = Vector3.zero;
		arrows = this.GetComponentsInChildren<Arrow> ();
		layer_mask = LayerMask.GetMask("Plane");
		TurnOffAllArrow ();
	}

	private Vector3 fromV3;
	private Vector3 toV3;

	private Vector2 fromV2;
	private Vector2 toV2;

	bool mouseDown;
	bool chosen;
	int layer_mask ;

	// Update is called once per frame
	void Update () {

		if (!chosen)
			return;

		if (Input.GetMouseButtonDown (0)) {
	
			mouseDown = true;
		}

		if (Input.GetMouseButtonUp (0)) {
			print ("mouse up");
			mouseDown = false;

		}

		if (!mouseDown) {
			print (chosenDirection);
			TurnOffAllArrow ();
			chosen = false;
			ai.movement.GoToDirection (chosenDirection);
			return;
		}

		// Cast a ray from screen point
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		// Save the info
		RaycastHit hit;

	

		// You successfully hi
		if (Physics.Raycast (ray, out hit,Mathf.Infinity,layer_mask)) {

			// Find the direction to move in
			dir = hit.point - transform.position;

			// Make it so that its only in x and y axis
			dir.y = 0; // No vertical movement

		}

		Debug.DrawLine (transform.position,transform.position + transform.forward * 2,Color.green);
		Debug.DrawLine (transform.position,transform.position + dir * 2,Color.green);

		//Vector3 mousePos = transform.position + dir * 9999;

		fromV3 = (transform.position + transform.forward * 2) - transform.position;
		toV3 = (transform.position + dir * 2) - transform.position;

		fromV2 = new Vector2 (fromV3.x,fromV3.z);
		toV2 = new Vector2 (toV3.x,toV3.z);
	
		float angle = (Mathf.Abs( 360 - Angle360(fromV2,toV2)));
		//float angle = ( Angle360(A2,B2));
		//print (angle);

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

		TurnOnArrow(chosenDirection);
	}

	void OnMouseDown()
	{
		TurnOnArrow(ai.movement.direction);
		chosenDirection = ai.movement.direction;
		chosen = true;
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

	void TurnOnArrow(Direction direction)
	{
		foreach (Arrow arrow in arrows) {

			arrow.gameObject.SetActive (arrow.direction == direction);
		
		}
	}

	void TurnOffAllArrow()
	{
		foreach (Arrow arrow in arrows) {

			arrow.gameObject.SetActive (false);

		}
	}

}

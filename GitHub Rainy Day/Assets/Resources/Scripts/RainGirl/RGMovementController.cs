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
	private Transform mesh;

	// Public movement vars
	public float rotSpeed;
	public float speed;
	public RGDirection direction;
	public bool isHit;
	// Private movement vars
	private bool changedDir;
	private Vector3 targetTile;
	private Vector3 targetEulerAngle;
	private float tileNo;
	private int startDir;
	private SphereCollider head;
	[SerializeField]
	private Vector3 currentTile;

	// Use this for initialization
	void OnEnable() {
		changedDir = false;
		startDir = Random.Range (0, 4);
		if (startDir == 0) {
			direction = RGDirection.UP;
			targetEulerAngle = new Vector3 (0,0,0);
			mesh.localRotation = Quaternion.Slerp (mesh.localRotation, Quaternion.Euler (targetEulerAngle), Time.deltaTime * rotSpeed);
		}
		if (startDir == 1) {
			direction = RGDirection.DOWN;
			targetEulerAngle = new Vector3 (0,180,0);
			mesh.localRotation = Quaternion.Slerp (mesh.localRotation, Quaternion.Euler (targetEulerAngle), Time.deltaTime * rotSpeed);
		}
		if (startDir == 2) {
			direction = RGDirection.LEFT;
			targetEulerAngle = new Vector3 (0,-90,0);
			mesh.localRotation = Quaternion.Slerp (mesh.localRotation, Quaternion.Euler (targetEulerAngle), Time.deltaTime * rotSpeed);
		}
		if (startDir == 3) {
			direction = RGDirection.RIGHT;
			targetEulerAngle = new Vector3 (0,90,0);
			mesh.localRotation = Quaternion.Slerp (mesh.localRotation, Quaternion.Euler (targetEulerAngle), Time.deltaTime * rotSpeed);
		}
	}
	void OnCollisionEnter(Collision col) {
		if (col.collider.gameObject.layer == 9) {

			isHit = true;

			if (direction == RGDirection.DOWN) {
				direction = RGDirection.UP;
				targetTile = currentTile + new Vector3 (0, 0, tileNo);
			}else if (direction == RGDirection.UP) {
				direction = RGDirection.DOWN;
				targetTile = currentTile + new Vector3 (0, 0, -tileNo);
			}
			if (direction == RGDirection.LEFT) {
				direction = RGDirection.RIGHT;
				targetTile = currentTile + new Vector3 (tileNo, 0, 0);
			}else if (direction == RGDirection.RIGHT) {
				direction = RGDirection.LEFT;
				targetTile = currentTile + new Vector3 (-tileNo, 0, 0);
			}
		} else {
			isHit = false;
		}
	}
	private IEnumerator SetAnim()
	{
		head.enabled = false;
		Anim_hit.SetBool ("isHit", true);
		yield return new WaitForSeconds( 2.0f );
		isHit = false;
		head.enabled = true;
		Anim_hit.SetBool ("isHit", false);
	}
	void Move () {
		currentTile = new Vector3 (Mathf.Round (transform.position.x), 0, Mathf.Round (transform.position.z));

				if(transform.position.x<-0.5f || transform.position.x>5.5f|| transform.position.z>5.5f|| transform.position.z<-0.5f)
				{
					//LeanPoolTestOF.instance.DespawnPrefab (LeanPoolTestOF.instance.spawnedRainGirlPrefabs,0);
			Destroy(gameObject);
				}

		if (Input.GetKeyDown (KeyCode.W)) {
			targetTile = currentTile + new Vector3 (0, 0, tileNo);
			changedDir = true;
			direction = RGDirection.UP;
		}

		if (Input.GetKeyDown (KeyCode.S)) {
			targetTile = currentTile + new Vector3 (0, 0, -tileNo);
			changedDir = true;
			direction = RGDirection.DOWN;
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			targetTile = currentTile + new Vector3 (-tileNo, 0, 0);
			changedDir = true;
			direction = RGDirection.LEFT;
		}

		if (Input.GetKeyDown (KeyCode.D)) {
			targetTile = currentTile + new Vector3 (tileNo, 0, 0);
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
		Anim_hit = GetComponentInChildren<Animator> ();
		head = GetComponent<SphereCollider> ();
		isHit = false;
		targetTile = new Vector3 (Mathf.Round(transform.position.x),0,Mathf.Round(transform.position.z));
		targetEulerAngle = new Vector3 (0,0,0);
		tileNo = 1;
	}
	// Update is called once per frame
	void Update () {
		if (isHit) {
			isHit = false;
			StartCoroutine(SetAnim() );
		} else {
			Move ();
		}
	}
}
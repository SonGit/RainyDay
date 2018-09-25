using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

	// Use this for initialization
	public Transform trans;
	public Vector3 nextPos;

	void Fall() {
		nextPos.y = Mathf.MoveTowards (nextPos.y, 0.15f, Time.deltaTime*2);
		transform.position = nextPos;
	}
	void Hit() {
	
	}
	void Start () {
		nextPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (nextPos.y > 0.15f) {
			Fall ();
		}
	}
}

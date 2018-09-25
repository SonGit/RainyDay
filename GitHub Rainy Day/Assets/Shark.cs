using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour {
	Vector3 nextPos;
	// Use this for initialization
	void Start () {
		print (transform.localRotation.y);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.eulerAngles.y == 90) {
			nextPos = transform.position + Vector3.right*0.05f;
			transform.position = nextPos;
			print ("left");
		} else{
			nextPos = transform.position - Vector3.right*0.05f;
			transform.position = nextPos;
		}
	}
}
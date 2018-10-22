using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {

	// Use this for initialization
	public AutoDoor door;
	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "AI") {
			door.isRay = true;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.transform.tag == "AI") {
			door.isRay = false;
		}
	}
}

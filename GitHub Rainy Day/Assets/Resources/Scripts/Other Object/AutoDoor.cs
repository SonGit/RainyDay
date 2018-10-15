using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour {
	public enum DoorID{
		RED,
		BLUE,
		YELLOW
	}
	[SerializeField]
	private DoorID id;
	[SerializeField]
	private float speed;
	[SerializeField]
	private bool isOpen;
	// Use this for initialization
	void openDoor() {
		if (!isOpen) {
			switch (id) {
			case DoorID.BLUE:
				if (transform.eulerAngles.y > 1) {
					transform.Rotate (0, -Time.deltaTime * speed, 0);
				} 
				else isOpen = true;
				break;
			case DoorID.RED:
				
				if (transform.eulerAngles.y < 359) {
					transform.Rotate (0, Time.deltaTime * speed, 0);
				}
				else isOpen = true;
				break;
			case DoorID.YELLOW:
				
				if (transform.eulerAngles.y > 179) {
					transform.Rotate (0, -Time.deltaTime * speed, 0);
				}
				else isOpen = true;
				break;
			}
		}
	}
	void closeDoor() {
		if (isOpen) {
			switch (id) {
			case DoorID.BLUE:
				if (transform.eulerAngles.y < 89) {
					transform.Rotate (0, Time.deltaTime * speed, 0);
				}
				else isOpen = false;
				break;
			case DoorID.RED:
				if (transform.eulerAngles.y > 271) {
					transform.Rotate (0, -Time.deltaTime * speed, 0);
				}
				else isOpen = false;
				break;
			case DoorID.YELLOW:
				if (transform.eulerAngles.y < 271) {
					transform.Rotate (0, Time.deltaTime * speed, 0);
				}
				else isOpen = false;
				break;
			}
		}
	}
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (isOpen)
			closeDoor ();
		else
			openDoor ();
	}
}

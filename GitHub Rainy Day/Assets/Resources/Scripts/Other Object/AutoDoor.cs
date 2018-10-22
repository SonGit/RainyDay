using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour {
	public static AutoDoor instance;
	void Awake()
	{
		if (instance == null)
			instance = this;
	}
	public enum DoorID{
		RED,
		BLUE,
		YELLOW
	}
	[SerializeField]
	private DoorID id;
	[SerializeField]
	private float speed;

	public bool isRay;
	// Use this for initialization
	public void openDoor() {
			switch (id) {
			case DoorID.BLUE:
			if (transform.localEulerAngles.y > 270) {
					transform.Rotate (0, -Time.deltaTime * speed, 0);
				} 
				break;
			case DoorID.RED:
				if (transform.eulerAngles.y < 358) {
					transform.Rotate (0, Time.deltaTime * speed, 0);
				}
				break;
			case DoorID.YELLOW:
				if (transform.eulerAngles.y > 189) {
					transform.Rotate (0, -Time.deltaTime * speed, 0);
				}
				break;
			}
	}
	public void closeDoor() {
			switch (id) {
			case DoorID.BLUE:
			if (transform.localEulerAngles.y < 358) {
					transform.Rotate (0, Time.deltaTime * speed, 0);
				}
				break;
			case DoorID.RED:
				if (transform.eulerAngles.y > 269) {
					transform.Rotate (0, -Time.deltaTime * speed, 0);
				}
				break;
			case DoorID.YELLOW:
				if (transform.eulerAngles.y < 271) {
					transform.Rotate (0, Time.deltaTime * speed, 0);
				}
				break;
		}
	}
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (isRay)
			openDoor ();
		else
			closeDoor ();
	}
}

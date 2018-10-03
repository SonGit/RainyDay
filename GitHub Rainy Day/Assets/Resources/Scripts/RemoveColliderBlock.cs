using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveColliderBlock : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	AI ai ;
	void OnTriggerEnter(Collider other)
	{
	 ai = other.GetComponent<AI> ();
		if (ai != null) {
			if(ai.movement.pathfinding)
				ai.GetComponent<Collider> ().enabled = false;
		}
	}
}

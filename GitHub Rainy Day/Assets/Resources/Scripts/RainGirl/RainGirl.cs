using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainGirl : MonoBehaviour {

	public GirlType type;

	AI ai;

	// Use this for initialization
	void Start () {
		ai = this.GetComponent<AI> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void FollowPathHome(Vector3 target)
	{

		ai.movement.FollowPath (target);
	}
}

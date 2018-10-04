﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainGirl : MonoBehaviour {

	public GirlType type;

	public AI ai;

	// Use this for initialization
	void Start () {
		ai = this.GetComponent<AI> ();
		RandomGirlType ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void FollowPathHome(Vector3 target)
	{

		ai.movement.FollowPath (target);
	}

	void RandomGirlType()
	{
		int rand = Random.Range (0,3);
		switch (rand) {

		case 0:
			SetGirlType (GirlType.BLUE);
			break;
		case 1:
			SetGirlType (GirlType.YELLOW);
			break;
		case 2:
			SetGirlType (GirlType.RED);
			break;
		}
	}

	void SetGirlType(GirlType newType)
	{
		Renderer rend = this.GetComponentInChildren<Renderer>();

		switch (newType) {

		case GirlType.RED:
			rend.material.color = Color.red;
			break;
		case GirlType.YELLOW:
			rend.material.color = Color.yellow;
			break;
		case GirlType.BLUE:
			rend.material.color = Color.blue;
			break;
		}

		type = newType;
	}
}

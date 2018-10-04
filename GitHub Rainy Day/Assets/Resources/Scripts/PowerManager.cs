using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour {
//	private AI ai;

	// Use this for initialization
	void Start () {
//		ai = GetComponent<AI> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GPSPower()
	{
		GameObject[] girls = GetAllGirl ();
		GameObject[] homes = GetAllHome ();

		foreach (GameObject girl in girls) {

			RainGirl rainGirl = girl.GetComponent<RainGirl> ();

			if (rainGirl != null) {

				foreach (GameObject home in homes) {

					Home homeScript = home.GetComponent<Home> ();

					if (homeScript != null) {

						if (rainGirl.type == homeScript.hometype && 
							rainGirl.ai.currentState != AI.RGState.START && 
							rainGirl.ai.currentState != AI.RGState.WAIT && 
							!rainGirl.ai.movement.pathfinding	)

							{
							rainGirl.FollowPathHome (homeScript.exitPoint.position);
							}
						}

					}

				}

			}


	}

	GameObject[] GetAllGirl()
	{
		return GameObject.FindGameObjectsWithTag ("AI");
	}

	GameObject[] GetAllHome()
	{
		return GameObject.FindGameObjectsWithTag ("Home");
	}
}

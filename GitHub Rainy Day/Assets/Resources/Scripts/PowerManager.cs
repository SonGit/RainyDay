using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Power{
	GPS,
	LIFE,
	REVERSE,
	FENCE
}
public class PowerManager : MonoBehaviour {
//	private AI ai;
	public FenceManager fen;
	private float countFence;
	public float FenceCD;
	public static PowerManager instance;
	// Use this for initialization
	void Awake()
	{
		if (instance == null)
			instance = this;
	}
	void Start () {
//		ai = GetComponent<AI> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void FenceUp() {
		fen.PopAllUp ();
		countFence = FenceCD;
	}
	public void LifePower() {
		print("Life Up");
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
							rainGirl.ai.currentState != AI.RGState.GPS	)
							{
							
							rainGirl.FollowPathHome (homeScript.exitPoint.position);
							}
						}
					}
				}
			}
	}
	public void ReversePower(){
		GameObject[] girls = GetAllGirl ();
		foreach (GameObject girl in girls) {
			RainGirl rainGirl = girl.GetComponent<RainGirl> ();

			if (rainGirl != null) {
				rainGirl.ai.Dizzy ();
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

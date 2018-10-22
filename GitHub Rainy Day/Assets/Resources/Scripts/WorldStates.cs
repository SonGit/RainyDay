using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStates : MonoBehaviour {

	public static WorldStates instance;
	public bool isStartle;

	void Awake()
	{
			instance = this;
	}
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutBoardDestroy : MonoBehaviour {
	AI ai;
	Home home;
	RainGirl girl;
	// Use this for initialization
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "AI") {
			girl = col.GetComponent<RainGirl> ();
			ai = col.GetComponent<AI> ();
			if (ai.currentState != AI.RGState.GPS && home.hometype!= girl.type) {
				Destroy (col.gameObject);
				LifeManager.instance.Lives--;
			}
			else{
				Destroy(col.gameObject);
			}
		}
	}
	void Start () {
		home = GetComponentInParent<Home> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

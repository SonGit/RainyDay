using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.MoveTo(gameObject,iTween.Hash("path",iTweenPath.GetPath("Path1"),"time",10f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

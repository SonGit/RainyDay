using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour {
	
	public Color color;

	// Use this for initialization
	void Start () {
		Renderer rend = this.GetComponent<Renderer>();
		color = rend.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

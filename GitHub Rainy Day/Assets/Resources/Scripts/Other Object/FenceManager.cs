using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceManager : MonoBehaviour {
	public Fence[] Fences;

	// Use this for initialization
	public void PopAllUp(){
		for (int i = 0; i < Fences.Length; i++) {
			Fences [i].PopUp();
		}
	}
	public void PopAllDown(){
		for (int i = 0; i < Fences.Length; i++) {
			Fences [i].PopDown();
		}
	}
	void Start () {
		Fences = GetComponentsInChildren<Fence>();
		PopAllDown ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

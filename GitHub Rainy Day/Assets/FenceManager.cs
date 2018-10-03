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
	IEnumerator Start () {
		Fences = GetComponentsInChildren<Fence>();
		while (false) {
			PopAllDown ();
			yield return new WaitForSeconds (2);
			PopAllUp ();
			yield return new WaitForSeconds (2);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

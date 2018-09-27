using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class Spawner : MonoBehaviour {
	
	public Color _currentColor;
	public GameObject AI;

	// Use this for initialization
	IEnumerator Start () {

		while (true) {

			Vector3 pos = new Vector3 (Random.Range(0,5),5,Random.Range(0,5));
//			Vector3 pos = new Vector3(0,3,0);
		//	LeanPoolTestOF.instance.GetRainGirlPrefab (pos);
			Instantiate(AI,pos,Quaternion.identity);
			//SetColor ();

			yield return new WaitForSeconds (Random.Range(1,2));
		}

	}
//	public void SetColor()
//	{
//		int color = Random.Range (0,3);
//		Renderer rend = this.GetComponentInChildren<Renderer>();
//
//		if (color == 0) {
//			rend.material.color = Color.red;
//		}
//		if (color == 1) {
//			rend.material.color = Color.green;
//		}
//		if (color == 2) {
//			rend.material.color = Color.blue;
//		}
//
//		_currentColor = rend.material.color;
//	}
	// Update is called once per frame
	void Update () {
		
	}
}

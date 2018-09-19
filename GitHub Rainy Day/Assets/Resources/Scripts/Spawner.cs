using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject _prefab;

	// Use this for initialization
	IEnumerator Start () {

		while (true) {

			GameObject go = Instantiate (_prefab);

			go.transform.position = new Vector3 (Random.Range(0,8),0,Random.Range(-7,0));

			BaseAI ai = go.GetComponent<BaseAI> ();
			ai.SetColor ();

			yield return new WaitForSeconds (Random.Range(5,15));
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

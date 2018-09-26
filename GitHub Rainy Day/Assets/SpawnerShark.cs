using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerShark : MonoBehaviour {
	public int dir;
	public GameObject shark;
	private Vector3 nextPos;
	// Use this for initialization

	IEnumerator Start () {

		while (true) {

			dir = Random.Range (0, 6);
			print (dir);
			if (dir <= 2) {
				shark.transform.position = new Vector3(-2,1, Random.Range(0,6));
				shark.transform.eulerAngles = new Vector3 (0, 90, 0);
				Instantiate (shark);
			} else {
				shark.transform.position = new Vector3(7,1, Random.Range(0,6));
				shark.transform.eulerAngles = new Vector3 (0, -90, 0);
				Instantiate (shark);
			}
			print (shark.transform.eulerAngles);
			yield return new WaitForSeconds (Random.Range(3,4));
		}

	}
	// Update is called once per frame
	void Update () {
		
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class SpawnerAI : MonoBehaviour {
	public Color _currentColor;
	public GameObject AI;
	public float spawnerTime;
	// Use this for initialization
	IEnumerator Start () {

		while (true) {
			Vector3 pos = new Vector3 (Random.Range(0,5),1.5f,Random.Range(0,5));
			GameObject ai = (GameObject)Instantiate(AI,pos,Quaternion.identity);
			AI a = ai.GetComponent<AI> ();
			a.movement.GoToRandDirection ();
			yield return new WaitForSeconds (spawnerTime);
		}

	}
	public void SetColor()
	{
		int color = Random.Range (0,3);
		Renderer rend = this.GetComponentInChildren<Renderer>();

		if (color == 0) {
			rend.material.color = Color.red;
		}
		if (color == 1) {
			rend.material.color = Color.green;
		}
		if (color == 2) {
			rend.material.color = Color.blue;
		}

		_currentColor = rend.material.color;
	}
	// Update is called once per frame
	void Update () {
		
	}
}

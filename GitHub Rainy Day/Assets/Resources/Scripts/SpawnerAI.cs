using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class SpawnerAI : MonoBehaviour {
	public float spawnerTime;
	public GameObject playerBoy;
	public GameObject playerGirl;
	private Vector3 pos;
	public RGMovementController movement;
	// Use this for initialization
	IEnumerator Start () {
		while (true) {
			yield return new WaitForSeconds (1f);
			pos = new Vector3 (Random.Range(0,5),1.5f,Random.Range(0,5));
			int rand = Random.Range (0, 2);
			if (rand == 0) {
				GameObject _player = (GameObject)Instantiate (playerBoy, pos, Quaternion.identity);
				movement = _player.GetComponent<RGMovementController> ();
				movement.GoToRandDirection ();
			} else {
				GameObject _player = (GameObject)Instantiate (playerGirl, pos, Quaternion.identity);
				movement = _player.GetComponent<RGMovementController> ();
				movement.GoToRandDirection ();
			}

			yield return new WaitForSeconds (spawnerTime);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

//	void GetPlayerBoy (Vector3 pos){
//		if (playerBoy = null) {
//			return;
//		}
//		playerBoy = ObjectPool.instance.GetPlayerBoy ();
//		playerBoy.Init ();
//		playerBoy.transform.position = pos;
//		movement = playerBoy.GetComponent<RGMovementController> ();
//		movement.GoToRandDirection ();
//	}
//
//	void GetPlayerGirl (Vector3 pos){
//		if (playerGirl = null) {
//			return;
//		}
//		playerGirl = ObjectPool.instance.GetPlayerGirl ();
//		playerGirl.Init ();
//		playerGirl.transform.position = pos;
//		movement = playerGirl.GetComponent<RGMovementController> ();
//		movement.GoToRandDirection ();
//	}
}

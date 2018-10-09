using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftSpawner : MonoBehaviour {
	public GameObject Gift;
	public float hGift;
	private Power _pow;
	private Vector3 pos;
	// Use this for initialization
	void RandomPower(){
		int rand = Random.Range (0, 4);
		switch (rand) {
		case 1:
			_pow = Power.FENCE;
			break;
		case 2:
			_pow = Power.GPS;
			break;
		case 3:
			_pow = Power.LIFE;
			break;
		case 4:
			_pow = Power.REVERSE;
			break;
		}
	}
	Vector3 RandomPos() {
		return pos = new Vector3 (Random.Range(0,5),hGift,Random.Range(0,5));
	}

	IEnumerator Start () {
		while (true) {
			pos = RandomPos ();
			RandomPower ();
			GameObject _gift = (GameObject)Instantiate(Gift,pos,Quaternion.identity);
			Gift gift =  _gift.GetComponent<Gift> ();
			gift._pow = _pow;
			yield return new WaitForSeconds (3);
			Destroy (_gift);
			yield return new WaitForSeconds (Random.Range(5,6));
		}

	}
	
	// Update is called once per frame
	void Update () {

	}

}

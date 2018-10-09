using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Gift : MonoBehaviour {
	public float speedFalling;
	public float speedBeating;
	public Power _pow;
	public TextMeshPro debugText;
	// Use this for initialization
	void Falling(){
		transform.position += new Vector3 (0, -speedFalling * Time.deltaTime, 0);		
	}
	void OnTheGround(){
		
	}
	void OnCollisionEnter (Collision col) {
		if (col.collider.gameObject.tag == "AI") {
			switch (_pow) {
			case Power.FENCE:
				PowerManager.instance.FenceUp ();
				Destroy (this);
				break;
			case Power.LIFE:
				PowerManager.instance.LifePower();
				Destroy (this);
				break;
			case Power.GPS:
				PowerManager.instance.GPSPower ();
				Destroy (this);
				break;
			case Power.REVERSE:
				PowerManager.instance.ReversePower ();
				Destroy (this);
				break;
			}
		}
	}
	void Start () {
		iTween.ScaleTo(gameObject, 
			iTween.Hash( 
				"scale", new Vector3(1.2f,1.2f,1.2f),  
				"time", speedBeating, 
				"easeType",iTween.EaseType.linear,
				"loopType", iTween.LoopType.pingPong));
//		debugText.text = (string)_pow;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y > 0) {
			Falling ();
		} else {
			OnTheGround ();
		}
	}
}


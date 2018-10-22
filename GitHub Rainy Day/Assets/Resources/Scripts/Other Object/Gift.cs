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
	void OnTriggerEnter (Collider col) {
		if (col.gameObject.layer == 9) {
			switch (_pow) {
			case Power.FENCE:
				PowerManager.instance.FenceUp ();
				GetGiftFX ();
				Destroy (gameObject);
				break;
			case Power.LIFE:
				PowerManager.instance.LifePower ();
				GetGiftFX ();
				GetGiftFX ();
				Destroy (gameObject);
				break;
			case Power.GPS:
				PowerManager.instance.GPSPower ();
				GetGiftFX ();
				Destroy (gameObject);
				break;
			case Power.REVERSE:
				PowerManager.instance.ReversePower ();
				GetGiftFX ();
				Destroy (gameObject);
				break;
			}
		}
	}
	GiftFX giftFX;
	void GetGiftFX (){
		giftFX = ObjectPool.instance.GetGiftFX ();
		giftFX.transform.position = this.transform.position+new Vector3(0,0.5f,0);
	}

	HeartFX heartFX;
	void GetHeartFX(){
		heartFX = ObjectPool.instance.GetHeartFX ();
		heartFX.transform.position = this.transform.position+new Vector3(0,0.5f,0);
	}

	void Start () {
		iTween.ScaleTo(gameObject, 
			iTween.Hash(
				"scale", new Vector3(1.2f,1.2f,1.2f),  
				"time", speedBeating, 
				"easeType",iTween.EaseType.linear,
				"loopType", iTween.LoopType.pingPong));
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y > 0) {
			Falling ();
		} else {
			OnTheGround ();
		}
				debugText.text = _pow.ToString();
	}
}
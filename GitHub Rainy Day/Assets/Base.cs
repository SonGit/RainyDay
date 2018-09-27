using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {

	// Use this for initialization
	public int color_int;
	private MeshRenderer baseColor;
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.GetComponent<AI>().rand == color_int) {
			print ("hit");
			Score.instance.score++;
		}

	}
	void Start () {
		Color color0 = new Color32(109, 18, 0, 255);
		Color color1 = new Color32(0, 117, 181, 255);
		Color color2 = new Color32(9, 135, 0, 255);

		baseColor = GetComponent<MeshRenderer> ();
		if ( baseColor.material.color == color0) {
			color_int = 0;
		}
		if (baseColor.material.color == color1) {
			color_int = 1;
		}
		if (baseColor.material.color == color2) {
			color_int = 2;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

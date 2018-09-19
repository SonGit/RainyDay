using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAI : MonoBehaviour {

	public Color _currentColor;

	//Material material;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {

		//Fetch the Renderer from the GameObject


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
	}

	void OnCollisionEnter(Collision collision)
	{
		print (collision.gameObject.name);

		if (collision.gameObject.tag == "Home") {
			Home home = collision.gameObject.GetComponent<Home> ();

			if (home != null) {
				if (_currentColor == home.color) {
					Destroy (gameObject);
				} else {
					Destroy (gameObject);
				}
			}

		}
	}
}

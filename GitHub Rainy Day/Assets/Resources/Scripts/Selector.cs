using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {

	public static Selector instance;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			transform.position = target.position + new Vector3 (0, 2, 0);

			if (gridMove != null) {
				if (Input.GetKeyDown (KeyCode.D)) {
					gridMove.dir = new Vector2 (1,0);
				}
				if (Input.GetKeyDown (KeyCode.A)) {
					gridMove.dir = new Vector2 (-1,0);
				}
				if (Input.GetKeyDown (KeyCode.W)) {
					gridMove.dir = new Vector2 (0,1);
				}
				if (Input.GetKeyDown (KeyCode.S)) {
					gridMove.dir = new Vector2 (0,-1);
				}
			}

		} else {
			transform.position = Vector3.one * 999;
			gridMove = null;
		}

	}
	Transform target;
	public GridMove gridMove;
	public void Select(Transform _target)
	{
		target = _target;
		gridMove = _target.GetComponent<GridMove> ();
	}
}

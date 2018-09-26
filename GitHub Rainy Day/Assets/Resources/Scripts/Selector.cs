using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Selector : MonoBehaviour {

	public static Selector instance;
	public TextMeshPro DebugT;
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


			if (controler != null) {
				if (SwipeManager.IsSwipingUp()) {
					controler.GoToDirection (RGMovementController.RGDirection.UP);
					DebugT.text = "select";
				}
				if (SwipeManager.IsSwipingDown()) {
					controler.GoToDirection (RGMovementController.RGDirection.DOWN);
					DebugT.text = "select";
				}
				if (SwipeManager.IsSwipingLeft()) {
					controler.GoToDirection (RGMovementController.RGDirection.LEFT);
					DebugT.text = "select";
				}
				if (SwipeManager.IsSwipingRight()) {
					controler.GoToDirection (RGMovementController.RGDirection.RIGHT);
				DebugT.text = "select";
				}
			}

		} else {
			transform.position = Vector3.one * 999;
			controler = null;
		}

	}
	Transform target;
	public RGMovementController controler;
	public void Select(Transform _target)
	{
		target = _target;
		controler = _target.GetComponent<RGMovementController> ();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour {

	[SerializeField]
	private float popTime = 1;

	// Use this for initialization
	void Start () {

	}


	// Update is called once per frame
	void Update () {

	}

	public void PopUp()
	{
		transform.eulerAngles = new Vector3 (-180,0,0);

		iTween.RotateTo(this.gameObject,iTween.Hash(
			"x", this.gameObject.transform.eulerAngles.x+180,
			"time", popTime,
			"easetype", iTween.EaseType.spring
		));
	}

	public void PopDown()
	{
		transform.eulerAngles = new Vector3 (0,0,0);
		iTween.RotateTo(this.gameObject,iTween.Hash(
			"x", this.gameObject.transform.eulerAngles.x+180,
			"time", popTime,
			"easetype", iTween.EaseType.spring
		));

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour {

	[SerializeField]
	private float popTime = 1;
	public bool isPopDown;
	public enum Direction
	{
		UP,
		DOWN,
		LEFT,
		RIGHT
	}
	public Direction direction;

	private Vector3 orgRot;
	// Use this for initialization
	void Start () {
		orgRot = transform.localEulerAngles;
	}

	void ResetRot()
	{
		transform.localEulerAngles = orgRot;
	}

	// Update is called once per frame
	void Update () {

	}

	public void PopUp()
	{
		StopAllCoroutines ();
		if (isPopDown) {


			switch (direction) {
			case Direction.UP:
				transform.localEulerAngles = new Vector3 (90, 0, 0);
				iTween.RotateTo (this.gameObject, iTween.Hash (
					"x", -90,
					"time", popTime,
					"easetype", iTween.EaseType.spring,
					"oncomplete","OnCompletePopUp",
					"oncompletetarget",gameObject,
				    "islocal",true));

				break;
			case Direction.DOWN:
				transform.localEulerAngles = new Vector3 (90, 0, 0);
				iTween.RotateTo (this.gameObject, iTween.Hash (
					"x", 270,
					"time", popTime,
					"easetype", iTween.EaseType.spring,
					"oncomplete","OnCompletePopUp",
					"oncompletetarget",gameObject,
					"islocal",true));

				break;
			case Direction.LEFT:
				transform.localEulerAngles = new Vector3 (-180, 90, 90);
				iTween.RotateTo (this.gameObject, iTween.Hash (
					"y", transform.localEulerAngles.y - 180,
					"time", popTime,
					"easetype", iTween.EaseType.spring,
					"oncomplete","OnCompletePopUp",
					"oncompletetarget",gameObject,
					"islocal",true));
				break;
			case Direction.RIGHT:
				transform.localEulerAngles = new Vector3 (180, 90, 90);
				iTween.RotateTo (this.gameObject, iTween.Hash (
					"y", transform.localEulerAngles.y + 180,
					"time", popTime,
					"easetype", iTween.EaseType.spring,
					"oncomplete","OnCompletePopUp",
					"oncompletetarget",gameObject,
					"islocal",true));

				break;
			}
		}
	}

	void OnCompletePopUp()
	{
		isPopDown = false;
	}

	void OnCompletePopDown()
	{
		isPopDown = true;
	}

	public void PopDown()
	{
		StopAllCoroutines ();
		if (!isPopDown) {
			
			ResetRot ();

			switch (direction) {
			case Direction.UP:
				iTween.RotateTo (this.gameObject, iTween.Hash (
					"x", this.gameObject.transform.eulerAngles.x + 180,
					"time", popTime,
					"easetype", iTween.EaseType.spring,
					"oncomplete","OnCompletePopDown",
					"oncompletetarget",gameObject));

				break;
			case Direction.DOWN:
				iTween.RotateTo (this.gameObject, iTween.Hash (
					"x", this.gameObject.transform.eulerAngles.x - 180,
					"time", popTime,
					"easetype", iTween.EaseType.spring,
					"oncomplete","OnCompletePopDown",
					"oncompletetarget",gameObject));

				break;
			case Direction.LEFT:
				iTween.RotateTo (this.gameObject, iTween.Hash (
					"x", this.gameObject.transform.eulerAngles.x - 180,
					"time", popTime,
					"easetype", iTween.EaseType.spring,
					"oncomplete","OnCompletePopDown",
					"oncompletetarget",gameObject));

				break;
			case Direction.RIGHT:
				iTween.RotateTo (this.gameObject, iTween.Hash (
					"x", this.gameObject.transform.eulerAngles.x + 180,
					"time", popTime,
					"easetype", iTween.EaseType.spring,
					"oncomplete","OnCompletePopDown",
					"oncompletetarget",gameObject));

				break;
			}
		}
	}
}


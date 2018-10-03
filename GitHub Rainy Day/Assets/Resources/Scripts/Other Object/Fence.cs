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
	// Use this for initialization
	void Start () {

	}


	// Update is called once per frame
	void Update () {

	}

	public void PopUp()
	{
		switch (direction) {
		case Direction.UP:
//			transform.eulerAngles = new Vector3 (0,180,0);
//			if (isPopDown = true) {
				iTween.RotateTo (this.gameObject, iTween.Hash (
					"x", this.gameObject.transform.eulerAngles.x + 180,
					"time", popTime,
					"easetype", iTween.EaseType.spring));
				isPopDown = false;
//			}
			break;
		case Direction.DOWN:
//			transform.eulerAngles = new Vector3 (0,180,0);
//			if (isPopDown = true) {
				iTween.RotateTo (this.gameObject, iTween.Hash (
					"x", this.gameObject.transform.eulerAngles.x - 180,
					"time", popTime,
					"easetype", iTween.EaseType.spring));
				isPopDown = false;
//			}
			break;
		case Direction.LEFT:
//			if (isPopDown = true) {
				transform.eulerAngles = new Vector3 (-180, -90, 0);
				iTween.RotateTo (this.gameObject, iTween.Hash (
					"x", this.gameObject.transform.eulerAngles.x - 180,
					"time", popTime,
					"easetype", iTween.EaseType.spring));
				isPopDown = false;
//			}
			break;
		case Direction.RIGHT:
//			if (isPopDown = true) {
				transform.eulerAngles = new Vector3 (180, -90, 0);
				iTween.RotateTo (this.gameObject, iTween.Hash (
					"x", this.gameObject.transform.eulerAngles.x + 180,
					"time", popTime,
					"easetype", iTween.EaseType.spring));
				isPopDown = false;
//			}
			break;
		}
	}

	public void PopDown()
	{
			switch (direction) {
			case Direction.UP:
//			if (isPopDown = false) {
				transform.eulerAngles = new Vector3 (0, 0, 0);
				iTween.RotateTo (this.gameObject, iTween.Hash (
					"x", this.gameObject.transform.eulerAngles.x + 180,
					"time", popTime,
					"easetype", iTween.EaseType.spring));
				isPopDown = true;
//			}
				break;
			case Direction.DOWN:
//			if (isPopDown = false) {
				transform.eulerAngles = new Vector3 (0, 0, 0);
				iTween.RotateTo (this.gameObject, iTween.Hash (
					"x", this.gameObject.transform.eulerAngles.x - 180,
					"time", popTime,
					"easetype", iTween.EaseType.spring));
				isPopDown = true;
//			}
				break;
			case Direction.LEFT:
//			if (isPopDown = false) {
				transform.eulerAngles = new Vector3 (0, 90, 0);
				iTween.RotateTo (this.gameObject, iTween.Hash (
					"x", this.gameObject.transform.eulerAngles.x - 180,
					"time", popTime,
					"easetype", iTween.EaseType.spring));
				isPopDown = true;
//			}
				break;
			case Direction.RIGHT:
//			if (isPopDown = false) {
				transform.eulerAngles = new Vector3 (0, 90, 0);
				iTween.RotateTo (this.gameObject, iTween.Hash (
					"x", this.gameObject.transform.eulerAngles.x + 180,
					"time", popTime,
					"easetype", iTween.EaseType.spring));
				isPopDown = true;
//			}
				break;
			}
		}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour {
	Vector3 nextPos;
	public float speed;
	// Use this for initialization
	void Start () {

		Pathing();
	}
	
	// Update is called once per frame
	void Update () {
//		if (transform.eulerAngles.y == 90) {
//			nextPos = transform.position + Vector3.right*0.05f;
//			transform.position = nextPos;
//			print ("left");
//		} else{
//			nextPos = transform.position - Vector3.right*0.05f;
//			transform.position = nextPos;
//		}
	}

	//Force enemy to follow a pre-defined path 
	protected virtual void Pathing()
	{
		int rand;
		rand = Random.Range (1,10);
		string pathName = "path" + rand;

		//	Vector3[] pathNodes = iTweenPath.GetPath (pathName);
		//	pathNodes [pathNodes.Length - 1] = stateController.playerReference.transform.position;
		//iTweenPath path = iTweenPath.paths[pathName];
		//path.nodes [path.nodes.Count - 1] = new Vector3(1,1,1);

		iTween.MoveTo(gameObject, 
			iTween.Hash("path", iTweenPath.GetPath(pathName), 
				"orienttopath", true, 
				"looktime", 0.001f, 
				"lookahead", 0.001f, 
				"speed", speed, 
				"easetype", iTween.EaseType.linear, 
				"oncomplete", "OnCompletePath"));
	}

	protected virtual void OnCompletePath()
	{
		Pathing();
	}
}
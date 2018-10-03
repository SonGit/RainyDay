using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour {
	Vector3 nextPos;
	public float speed;
	public int id;
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
		
//		rand = Random.Range (1,4);
//		rand = 2;
		string pathName = "path" + id;

		//	Vector3[] pathNodes = iTweenPath.GetPath (pathName);
		//	pathNodes [pathNodes.Length - 1] = stateController.playerReference.transform.position;
		//iTweenPath path = iTweenPath.paths[pathName];
		//path.nodes [path.nodes.Count - 1] = new Vector3(1,1,1);

		iTween.MoveTo(gameObject, 
			iTween.Hash("path", iTweenPath.GetPath(pathName), 
				"orienttopath", true, 
				"looktime", 0.01f, 
				"lookahead", 0.01f, 
				"speed", speed, 
				"easetype", iTween.EaseType.linear, 
				"oncomplete", "OnCompletePath"));
	}

	protected virtual void OnCompletePath()
	{
		Pathing();
	}
}
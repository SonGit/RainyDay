using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duck : MonoBehaviour {
	public float speed;
	public int rand;
	// Use this for initialization
	void Start () {
		Pathing();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	protected virtual void Pathing()
	{
		
//		rand = Random.Range (1,4);
		//		rand = 2;
		string pathName = "Duck" + rand;
//		string pathName = "Duck";

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


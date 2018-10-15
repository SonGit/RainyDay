using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duck : MonoBehaviour {
	public float speed;
	public enum duckID{
		PATROL,
		RIGHT,
		LEFT
	}
	public duckID id;
	// Use this for initialization
	void Start () {
		Pathing();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	protected virtual void Pathing()
	{
		string pathName = "Duck_" + id.ToString();;
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


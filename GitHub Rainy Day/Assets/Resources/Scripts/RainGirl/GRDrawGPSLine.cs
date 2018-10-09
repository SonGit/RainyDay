using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using geniikw.DataRenderer2D;

public class GRDrawGPSLine : MonoBehaviour {
	[SerializeField]
	WorldLine worldLine;

	// Use this for initialization
	void Start () {
		worldLine = this.GetComponent<WorldLine> ();
		worldLine.MakeNewMesh ();
	}

	public void DrawPath(List<Vector3> nodes,Transform org)
	{
		worldLine = this.GetComponent<WorldLine> ();

		this.org = org;
	
		worldLine.line.Clear ();

		foreach (Vector3 node in nodes) {
			worldLine.line.Push (node,Vector3.zero,Vector3.zero,.05f);
		}
	}

	void Update()
	{
		if (first) {
			worldLine.line.EditPoint (0,new Vector3((float)System.Math.Round(org.position.x,1),(float)System.Math.Round(org.position.y,1),(float)System.Math.Round(org.position.z,1)),0.05f);
		}
	}

	bool first;
	Transform org;

	public void PopANode(List<Vector3> nodes)
	{
		if(first)
		worldLine.line.PopFirst ();	
		first = true;
	}
}

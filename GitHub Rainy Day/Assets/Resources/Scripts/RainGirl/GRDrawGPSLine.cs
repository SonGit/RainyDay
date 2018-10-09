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

	public void DrawPath(List<Vector3> nodes)
	{
		worldLine = this.GetComponent<WorldLine> ();
	
		worldLine.line.Clear ();

		foreach (Vector3 node in nodes) {
			worldLine.line.Push (node + new Vector3(0,.025f,0),Vector3.zero,Vector3.zero,.05f);
		}
	}

	void Update()
	{

	}

	public void PopANode(List<Vector3> nodes)
	{
		for (int i = 0; i < nodes.Count; i++) {
			// Fix for the spline's bug
			if (i == 1) {
				worldLine.line.EditPoint( i, new Point (nodes[i],new Vector3(0,-.01f,0),new Vector3(0,0,0),0.05f));
			} else {
				worldLine.line.EditPoint( i, new Point (nodes[i],Vector3.zero,Vector3.zero,0.05f));
			}
		}
			
	}


}

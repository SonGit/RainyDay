using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using geniikw.DataRenderer2D;

public class GRDrawGPSLine : MonoBehaviour {

	WorldLine worldLine;

	Transform origin;

	bool drawing;

	// Use this for initialization
	void Start () {
		worldLine = this.GetComponent<WorldLine> ();
	}

	public void DrawPath(List<Vector3> nodes)
	{
		worldLine = this.GetComponent<WorldLine> ();
		this.origin = origin;
		int i = 0;

		worldLine.line.Clear ();
		worldLine.MakeNewMesh ();

		foreach (Vector3 node in nodes) {

			worldLine.line.Push ();
			worldLine.line.EditPoint (i,node + new Vector3(0,.3f,0),.05f);
			i++;
		}


		drawing = true;
	}

	void Update()
	{
		if (drawing) {
			//worldLine.line.EditPoint (0,origin.position,.1f);
		}
	}

}

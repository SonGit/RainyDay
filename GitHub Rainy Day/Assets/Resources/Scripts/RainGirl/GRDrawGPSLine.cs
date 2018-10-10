using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using geniikw.DataRenderer2D;

public class GRDrawGPSLine : MonoBehaviour {
	[SerializeField]
	LineRenderer lineRenderer;
	[SerializeField]
	LineRenderer lineRendererToAI;

	// Use this for initialization
	void Start () {
		
	}

	public void DrawPath(Vector3[] nodes, Vector3 AIpos)
	{
		lineRenderer.SetPositions(nodes);

		for (int i = 1; i < nodes.Length; i++) {
			lineRenderer.SetPosition (i,nodes[i]);
		}

		lineRendererToAI.SetPosition (0,lineRenderer.GetPosition(0));
		lineRendererToAI.SetPosition (1,AIpos);
	}

	void Update()
	{
		
	}
		

}

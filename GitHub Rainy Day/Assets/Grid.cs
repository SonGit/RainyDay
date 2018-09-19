using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Grid : MonoBehaviour {

	public GameObject gridPrefab;

	public int gridCol;
	public int gridRow;

	// Use this for initialization
	void Start () {
		int count = 0;
		for(int i = 0 ; i < gridCol ; i++)
		{
			for(int y = 0 ; y < gridRow ; y++)
			{
				GameObject go = Instantiate (gridPrefab);
				go.transform.SetParent (transform);

				go.transform.localPosition = new Vector3 (y * 1, i * 1,0);

				count++;
				TextMeshPro textMesh = go.GetComponentInChildren<TextMeshPro> ();
				textMesh.text = count + "";
			}

		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

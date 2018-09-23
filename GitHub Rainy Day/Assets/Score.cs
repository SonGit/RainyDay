using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour {
	public static Score instance;
	private TextMeshPro scoreUI;
	public int score;
	void Awake(){
		instance = this;
	}
	// Use this for initialization
	void Start () {
		scoreUI = GetComponent<TextMeshPro> ();
	}
	
	// Update is called once per frame
	void Update () {
		scoreUI.text = "Score: " + score;
	}
}

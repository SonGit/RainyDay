using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeManager : MonoBehaviour {
	public static LifeManager instance;
	public int Lives;
	private TextMeshProUGUI lifemanager;
	// Use this for initialization

	void Awake()
	{
		if (instance == null)
			instance = this;
	}

	void Start () {
		lifemanager = GetComponent<TextMeshProUGUI> ();
	}
	
	// Update is called once per frame
	void Update () {
		lifemanager.text = "LIFE: " + Lives;
	}


}

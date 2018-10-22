using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFX : Cacheable {
	ParticleSystem particle;

	// Use this for initialization
	void Awake () {
		particle = this.GetComponent<ParticleSystem> ();
	}
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	}

	public override void OnLive ()
	{
		gameObject.SetActive (true);
		StartCoroutine (Play());
	}

	public override void OnDestroy ()
	{
		gameObject.SetActive (false);
	}
	IEnumerator Play()
	{
		yield return new WaitForSeconds(particle.main.duration);
		this.Destroy ();
	}
}

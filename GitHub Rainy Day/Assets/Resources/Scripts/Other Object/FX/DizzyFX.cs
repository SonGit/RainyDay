using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DizzyFX : Cacheable {

	public override void OnLive ()
	{
		if (gameObject != null) {
			gameObject.SetActive (true);
		}
	}

	public override void OnDestroy ()
	{
		if (gameObject != null) {
			gameObject.SetActive (false);
		}
	}
}
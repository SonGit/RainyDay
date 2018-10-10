using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cacheable : MonoBehaviour {

	private bool living;
	public bool _living
	{
		get 
		{
			return living;
		}

		set 
		{
			living = value;
		}
	}

	void Start()
	{
	//	Destroy ();// default state
	}

	public void Destroy()
	{
		OnDestroy ();
		_living = false;
		//gameObject.hideFlags = HideFlags.HideInHierarchy;
	}

	public void Live()
	{
		OnLive ();
		_living = true;
		//gameObject.hideFlags = HideFlags.HideInHierarchy;
	}

	public abstract void OnDestroy ();
	public abstract void OnLive ();
}

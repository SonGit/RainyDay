using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ATTACH TO ObjectFactory gameObject
public class ObjectFactory: MonoBehaviour {

	public static ObjectFactory instance;

	void Awake()
	{
		instance = this;
	}

	void Start () {
	}

	public enum PrefabType
	{
		None,
		AudioSource,
		EffectHit,
		RainGirl
	}

	public Dictionary<PrefabType,string> PrefabPaths = new Dictionary<PrefabType, string> {
		
		{ PrefabType.None, "" },
		{ PrefabType.AudioSource, "Prefabs/AudioSource" },
		{ PrefabType.EffectHit, "Prefabs/EffectHit" },
		{ PrefabType.RainGirl, "Prefabs/RainGirl" }
    };

	public GameObject LinkPrefab (PrefabType type)
	{
		string path;
		if (PrefabPaths.TryGetValue (type, out path)) {
			return (Resources.Load (path, typeof(GameObject)) as GameObject);
		}
		print ("NULL");
		return null;
	}

}

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

	public enum PrefabType
	{
		None,

		// Effect
		HitNumber,
		TextHitRandom,
		DeathSkull,
		StunEffect,
		SmokeEffect,
		Explosion,

		// Audio
		AudioSource,

		// GameObject
		Rock,

		// Enemy
		DinoFat,
		Dino_LongLeg,
		Dino_Triceratop,
		Dino_Fly,

		// WeaponIcon
		BowIcon,
		SpearIcon
	}

	public Dictionary<PrefabType,string> PrefabPaths = new Dictionary<PrefabType, string> {
		
		{ PrefabType.None, "" },

		// Effect
		{ PrefabType.HitNumber, "Prefabs/HitNumber" },
		{ PrefabType.TextHitRandom, "Prefabs/TextHitRandom" },
		{ PrefabType.DeathSkull, "Prefabs/DeathSkull" },
		{ PrefabType.StunEffect, "Prefabs/StunEffect" },
		{ PrefabType.SmokeEffect, "Prefabs/SmokeEffect" },
		{ PrefabType.Explosion, "Prefabs/Explosion" },

		// Audio
		{ PrefabType.AudioSource, "Prefabs/AudioSource" },

		// GameObject
		{ PrefabType.Rock, "Prefabs/Rock" },

		// Enemy
		{ PrefabType.DinoFat, "Prefabs/DinoFat" },
		{ PrefabType.Dino_LongLeg, "Prefabs/Dino_LongLeg" },
		{ PrefabType.Dino_Triceratop, "Prefabs/DinoTriceratop" },
		{ PrefabType.Dino_Fly, "Prefabs/Dino_Fly" },

		// WeaponIcon
		{ PrefabType.BowIcon, "Prefabs/bowIcon" },
		{ PrefabType.SpearIcon, "Prefabs/spearIcon" },
	};

	// Make GameObject from Resources
	public GameObject MakeObject(PrefabType type)
	{
		string path;
		if (PrefabPaths.TryGetValue (type, out path)) {
			return (Instantiate (Resources.Load (path, typeof(GameObject))) as GameObject);
		}
		print ("NULL");
		return null;
	}

	// Load gameObject from resources
	public GameObject LoadObject(PrefabType type)
	{
		string path;
		if (PrefabPaths.TryGetValue (type, out path)) {
			return (Resources.Load (path, typeof(GameObject))) as GameObject;
		}
		print ("NULL");
		return null;
	}

}

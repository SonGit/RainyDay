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
		WaterSplash,
		HitFX,
		GiftFX,
		LifeFX,
		HeartFX,
		GpsFX,
		StunFX,
		DizzyFX,

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
		{ PrefabType.WaterSplash, "Prefabs/FX/Water_Splash" },
		{ PrefabType.HitFX, "Prefabs/FX/Hit" },
		{ PrefabType.GiftFX, "Prefabs/FX/GiftExplosive" },
		{ PrefabType.HeartFX, "Prefabs/FX/Heart" },
		{ PrefabType.GpsFX, "Prefabs/FX/gpsFX" },
		{ PrefabType.StunFX, "Prefabs/FX/StunFX" },
		{ PrefabType.DizzyFX, "Prefabs/FX/DizzyFX" },
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

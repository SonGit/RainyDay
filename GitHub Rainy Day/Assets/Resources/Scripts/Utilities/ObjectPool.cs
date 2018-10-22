using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ATTACH TO ObjectPool gameObject

public class ObjectPool : MonoBehaviour {

	public static ObjectPool instance;

	// Effect
	GenericObject<WaterFX> waterFX;
	GenericObject<HitFX> hitFX;
	GenericObject<GiftFX> giftFX;
	GenericObject<HeartFX> heartFX;
	GenericObject<GpsFX> gpsFX;
	GenericObject<StunFX> stunFX;
	GenericObject<DizzyFX> dizzyFX;
	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		
		// Effect
		waterFX = new GenericObject<WaterFX>(ObjectFactory.PrefabType.WaterSplash,5);
		hitFX = new GenericObject<HitFX>(ObjectFactory.PrefabType.HitFX,5);
		giftFX = new GenericObject<GiftFX>(ObjectFactory.PrefabType.GiftFX,1);
		heartFX = new GenericObject<HeartFX>(ObjectFactory.PrefabType.HeartFX,1);
		gpsFX = new GenericObject<GpsFX>(ObjectFactory.PrefabType.GpsFX,10);
		stunFX = new GenericObject<StunFX>(ObjectFactory.PrefabType.StunFX,10);
		dizzyFX = new GenericObject<DizzyFX>(ObjectFactory.PrefabType.DizzyFX,10);
	}

	#region Effect
	public WaterFX GetWaterFX()
	{
		return waterFX.GetObj ();
	}

	public HitFX GetHitFX()
	{
		return hitFX.GetObj ();
	}

	public GiftFX GetGiftFX()
	{
		return giftFX.GetObj ();
	}
	public HeartFX GetHeartFX()
	{
		return heartFX.GetObj ();
	}
	public GpsFX GetGpsFX()
	{
		return gpsFX.GetObj ();
	}
	public StunFX GetStunFX()
	{
		return stunFX.GetObj ();
	}
	public DizzyFX GetDizzyFX()
	{
		return dizzyFX.GetObj ();
	}

	#endregion



}

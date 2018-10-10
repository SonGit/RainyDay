using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ATTACH TO ObjectPool gameObject

public class ObjectPool : MonoBehaviour {

	public static ObjectPool instance;

	// Effect
	GenericObject<HitNumber> hitNumbers;
	GenericObject<TextHitRandom> textHitRandoms;
	GenericObject<DeathSkull> deathSkulls;
	GenericObject<SmokeEffect> smokeEffect;
	GenericObject<ExplosionBoom> explosionBoom;
	GenericObject<StunEffect> stunEffect;

	// Enemy
	GenericObject<Dino_Fat> dinos;
	GenericObject<Dino_LongLeg> dino_LongLeg;
	GenericObject<Dino_Triceratop> dinoTriceratop;
	GenericObject<Dino_Fly> dino_Fly;

	// Audio
	GenericObject<Audio> audioS;

	// GameObject
	GenericObject<ThrowObject> rockThrow;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		
		// Effect
		hitNumbers = new GenericObject<HitNumber>(ObjectFactory.PrefabType.HitNumber,10);
		textHitRandoms = new GenericObject<TextHitRandom>(ObjectFactory.PrefabType.TextHitRandom,10);
		deathSkulls = new GenericObject<DeathSkull>(ObjectFactory.PrefabType.DeathSkull,10);
		stunEffect = new GenericObject<StunEffect>(ObjectFactory.PrefabType.StunEffect,1);
		smokeEffect = new GenericObject<SmokeEffect> (ObjectFactory.PrefabType.SmokeEffect, 10);
		explosionBoom = new GenericObject<ExplosionBoom> (ObjectFactory.PrefabType.Explosion, 10);

		// Audio
		audioS = new GenericObject<Audio>(ObjectFactory.PrefabType.AudioSource,10);

		// GameObject
		rockThrow = new GenericObject<ThrowObject> (ObjectFactory.PrefabType.Rock, 25);

		// Enemy
		dinos = new GenericObject<Dino_Fat>(ObjectFactory.PrefabType.DinoFat,25);
		dino_LongLeg = new GenericObject<Dino_LongLeg>(ObjectFactory.PrefabType.Dino_LongLeg,25);
		dinoTriceratop = new GenericObject<Dino_Triceratop>(ObjectFactory.PrefabType.Dino_Triceratop,25);
		dino_Fly = new GenericObject<Dino_Fly>(ObjectFactory.PrefabType.Dino_Fly,25);
	}

	#region Effect
	public HitNumber GetHitNumber()
	{
		return hitNumbers.GetObj ();
	}

	public TextHitRandom GetTextHitRandom()
	{
		return textHitRandoms.GetObj ();
	}	

	public DeathSkull GetDeathSkulls()
	{
		return deathSkulls.GetObj ();
	}
		
	public StunEffect GetStunEffect()
	{
		return stunEffect.GetObj ();
	}
		
	public SmokeEffect GetSmokeEffect()
	{
		return smokeEffect.GetObj ();
	}

	public ExplosionBoom GetExplosionBoom()
	{
		return explosionBoom.GetObj ();
	}
	#endregion

	#region GameObject 
	public ThrowObject GetRockThrow()
	{
		return rockThrow.GetObj ();
	}
	#endregion

	#region Audio
	public Audio GetAudioSoure()
	{
		return audioS.GetObj ();
	}
	#endregion

	#region Enemy
	public Enemy GetEnemy(Enemy enemy)
	{
		if (enemy is Dino_Fat) {
			return dinos.GetObj ();
		}

		else if (enemy is Dino_LongLeg) {
			return dino_LongLeg.GetObj ();
		}

		else if (enemy is Dino_Triceratop) {
			return dinoTriceratop.GetObj ();
		}

		else if (enemy is Dino_Fly) {
			return dino_Fly.GetObj ();
		}

		return null;
	}
	#endregion
}

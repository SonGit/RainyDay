﻿using UnityEngine;
using System.Collections.Generic;

namespace Lean.Pool
{
	// This script shows you how you can easily spawn and despawn a prefab
	public class LeanPoolTestOF : MonoBehaviour
	{
		public static LeanPoolTestOF instance;
		void Awake()
		{
			instance = this;
		}
		[Tooltip("The prefab that will be used in the test")]
		private GameObject pawnPrefab;
		private GameObject EffectHit;
		// This stores all spawned prefabs, so they can be despawned later
		[HideInInspector] public Stack<GameObject> spawnedPawnPrefabs = new Stack<GameObject>();
//		[HideInInspector] public Stack<GameObject> spawnedSmokeEffect = new Stack<GameObject>();


		void Start () 
		{
			//pawnPrefab = ObjectFactory.instance.LinkPrefab (ObjectFactory.PrefabType.AudioSource);
			//EffectHit = ObjectFactory.instance.LinkPrefab (ObjectFactory.PrefabType.EffectHit);
			pawnPrefab = ObjectFactory.instance.LinkPrefab (ObjectFactory.PrefabType.PawnPrefab);
		}

		public GameObject SpawnPrefab(GameObject prefab, Stack<GameObject> stack, Vector3 position)
		{
			GameObject clone = LeanPool.Spawn(prefab, position, Quaternion.identity, null);

			// Add the clone to the clones stack if it doesn't exist
			// If this prefab can be recycled then it could already exist
			if (stack.Contains(clone) == false)
			{
				stack.Push(clone);
			}

			return clone;
		}

		public void DespawnPrefab(Stack<GameObject> stack, float DespawnDelay)
		{
			if (stack.Count > 0)
			{
				// Get the last clone
				var clone = stack.Pop();

				// Despawn it
				LeanPool.Despawn(clone,DespawnDelay);
			}
		}

		public AudioSource_ GetAudioSource_(Vector3 pos)
		{
			return SpawnPrefab (pawnPrefab, spawnedPawnPrefabs, pos).GetComponent<AudioSource_>();
		}
//		public EffectHit GetSmokeEffect(Vector3 pos)
//		{
//			return SpawnPrefab (EffectHit,spawnedSmokeEffect,pos).GetComponent<EffectHit>();
//		}

		public BaseAI GetPawnPrefab (Vector3 pos)
		{
			return SpawnPrefab (pawnPrefab, spawnedPawnPrefabs, pos).GetComponent<BaseAI>();
		}
	}
}
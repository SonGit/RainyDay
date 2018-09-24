using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lean.Pool
{
	public class AudioSource_ : MonoBehaviour 
	{
		
		public float count=0;
		public AudioSource audioSource ;

		private bool isOnEnable;

		private void OnEnable ()
		{
			
			isOnEnable = true;
		}
		private void OnDisable ()
		{
			isOnEnable = false;
		}

		public void Play ()
		{
			audioSource.Play ();
		}
		// Update is called once per frame
		void Update () {
			if (!isOnEnable) {
				return;
			}

			LeanPoolTestOF.instance.DespawnPrefab (LeanPoolTestOF.instance.spawnedPawnPrefabs,audioSource.clip.length);
		}
	}
}
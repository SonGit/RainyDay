using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

	public class AudioManager : MonoBehaviour 
	{

		public static AudioManager instance;

		private Dictionary<SoundFX,AudioClip> clips;

		[HideInInspector]
		public string isOnSound = "t";

		void Awake()
		{
			instance = this;
		}

		IEnumerator Start()
		{
			clips = new Dictionary<SoundFX, AudioClip> {
				{ SoundFX.None, null },
				{ SoundFX.bark1, Resources.Load<AudioClip>("Sounds/bark1") },
				{ SoundFX.bark2, Resources.Load<AudioClip>("Sounds/bark2") },
				{ SoundFX.bark3, Resources.Load<AudioClip>("Sounds/bark3") },
				{ SoundFX.die, Resources.Load<AudioClip>("Sounds/die") },
				{ SoundFX.ending, Resources.Load<AudioClip>("Sounds/ending") },
				{ SoundFX.hit, Resources.Load<AudioClip>("Sounds/hit") },
				{ SoundFX.ButtonPresses, Resources.Load<AudioClip>("Sounds/hit1") }
			};

			yield return new WaitForSeconds (1);
		}

		public enum SoundFX
		{
			None,
			bark1,
			bark2,
			bark3,
			die,
			ending,
			hit,
			ButtonPresses
		}

		public void PlayClip(SoundFX soundFX, Vector3 pos)
		{
			AudioClip clip;
			if (clips.TryGetValue (soundFX, out clip)) {
				AudioSource_ audioSource_ =  LeanPoolTestOF.instance.GetAudioSource_ (pos);
				audioSource_.audioSource.clip = clip;
				audioSource_.Play ();

				if (isOnSound == "t") {
					audioSource_.audioSource.volume = 1f;
				} else if (isOnSound == "f"){
					audioSource_.audioSource.volume = 0;
				}
			}
		}
//			
//		public void PlaySmokeEffect (Vector3 pos)
//		{
//			SmokeEffect smokeEffect = LeanPoolTestOF.instance.GetEffectHit_ (pos);
//			smokeEffect.Play ();
//		}

//		public void PlaySound (Vector3 pos)
//		{
//			SmokeEffect smokeEffect = ObjectPool.instance.GetSmokeEffect (pos);
//			smokeEffect.Play ();
//		}
	}

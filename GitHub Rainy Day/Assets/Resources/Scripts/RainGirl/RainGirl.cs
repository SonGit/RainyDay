using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainGirl : MonoBehaviour {

	public GirlType type;
	private SkinnedMeshRenderer skin;
	public AI ai;
	public Texture rand;
	private Animator Anim;
	float t;

	// Use this for initialization
	void Start () {
		ai = this.GetComponent<AI> ();
		skin = this.GetComponentInChildren<SkinnedMeshRenderer> ();
		Anim = this.GetComponentInChildren<Animator> ();

		//print (mat);
		RandomGirlType ();
	}

	// Update is called once per frame
	void Update () {
		Startle ();
	}

	public void FollowPathHome(Vector3 target)
	{
		ai.Anim.ResetTrigger ("Stun");
		ai.Anim.SetTrigger ("GPS");
		ai.GPS (target);
	}

	void RandomGirlType()
	{
		int rand = Random.Range (0,3);
		switch (rand) {

		case 0:
			SetGirlType (GirlType.BLUE);
			break;
		case 1:
			SetGirlType (GirlType.YELLOW);
			break;
		case 2:
			SetGirlType (GirlType.RED);
			break;
		}
	}

	void SetGirlType(GirlType newType)
	{
		
		switch (newType) {

		case GirlType.RED:
			rand = Resources.Load<Texture> ("Materials/Player/Boy_tex_red");
			skin.materials[0].mainTexture = rand;
			break;
		case GirlType.YELLOW:
			rand = Resources.Load<Texture> ("Materials/Player/Boy_tex_yellow");
			skin.materials[0].mainTexture = rand;
			break;
		case GirlType.BLUE:
			rand = Resources.Load<Texture> ("Materials/Player/Boy_tex_green");
			skin.materials[0].mainTexture = rand;
			break;
		}

		type = newType;
	}

	private void Startle(){
		if (WorldStates.instance.isStartle == true) {
			if (t <= 1) {
				Anim.SetFloat ("IsStartle", t += Time.deltaTime*0.5f);
			}
		} else {
			if (t >= 0) {
				Anim.SetFloat ("IsStartle", t -= Time.deltaTime*0.5f);
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_SD_Ani : MonoBehaviour {

	public const string IDLE	= "Idle";
	public const string RUN		= "Run";
	public const string ATTACK	= "Attack";
	public const string SKILL	= "Skill";
	public const string DAMAGE	= "Damage";
	public const string STUN	= "Stun";
	public const string DEATH	= "Death";

	Animation anim;

	void Start () {
		anim = GetComponent<Animation>();
	}

	public void IdleAni (){
		anim.CrossFade (IDLE);
	}

	public void RunAni (){
		anim.CrossFade (RUN);
	}

	public void AttackAni (){
		anim.CrossFade (ATTACK);
	}

	public void SkillAni (){
		anim.CrossFade (SKILL);
	}

	public void DamageAni (){
		anim.CrossFade (DAMAGE);
	}

	public void StunAni (){
		anim.CrossFade (STUN);
	}

	public void DeathAni (){
		anim.CrossFade (DEATH);
	}

}

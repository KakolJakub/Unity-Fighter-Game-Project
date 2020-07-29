﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltShockGrenadeAbility : Ability
{
    public Transform projectileSpawnPoint;
	public Transform projectile;
	public Animator animate;
	
	public ParticleSystem explosion;
	
	public int damageEffect;
	public float grenadeSpeed;
	public float grenadeLifeTime;
	
	void Start()
	{
		projectile.GetComponent<ProjectileLogic>().SetProjectileStats(abilityDamage, damageEffect, grenadeSpeed, grenadeLifeTime);
		projectile.GetComponent<ProjectileLogic>().SetProjectileEffects(explosion);
	}
	
	public override void ActivateAbility()
	{
		animate.SetTrigger("Ability_ShockGrenade");
	}
	
	public void ThrowGrenade()
	{
		Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
	}
	
	//TODO:
	//Add gravity to the Shock Grenade Projectile (the gravity value should be set in this script)
	//Add explosion radius to the Shock Grenade Projectile (the radius value should be set in this script)
	//Make an enum for damageEffects (characterCombat script OR playerStats script)
	
	
	//DONE: Create electric particles - used when the grenade detonates
	//DONE: Implement animation - add an animator reference, add a SetTrigger
	//DONE: Create a DealProjectileDamage method - used on collider collision (ProjectileLogic script)
	//DONE: Create GameObject which will serve as a projectile spawn point and Attach it to the character
	//DONE: Create a Projectile Logic Script
	//DONE: Create a GameObject that will serve as a projectile (later used as a prefab)
	//DONE: Create a Projectile Spawn method, it will be used via animation event
	
	

}

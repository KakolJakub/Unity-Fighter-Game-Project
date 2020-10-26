﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public abstract class RageMode : MonoBehaviour
{
    public PlayerStats playerStats;
	public Animator animate;
	public VideoClip rageClip;

	public delegate void PlayCutscene(VideoClip clip);
	public static event PlayCutscene OnCutscene;
	
	delegate void TakeAction();
	static event TakeAction OnCutsceneEnd;
	
	//TODO: Consider creating a RageCutscenePlayer GameObject, instead of adding that functionality here

	bool rageReady;
	bool rageInitiated;
	
	int rageMeterCap = 100;
	int rageMeter;
	
	float currentRageDuration;
	
	void OnEnable()
	{
		playerStats.OnDamageTaken += IncreaseRageMeter;
		playerStats.OnDamageDealt += IncreaseRageMeterByHalf;
		OnCutsceneEnd += ActivateRageMode;
	}
	
	void OnDisable()
	{
		playerStats.OnDamageTaken -= IncreaseRageMeter;
		playerStats.OnDamageDealt -= IncreaseRageMeterByHalf;
		OnCutsceneEnd -= ActivateRageMode;
	}
	
	void Start()
	{
		rageReady = false;
		rageInitiated = false; 
		rageMeter = 0;
	}
	
	protected void Update()
	{
		if(rageMeter >= rageMeterCap)
		{
			rageMeter = rageMeterCap;
			rageReady = true;
		}
		
		if(playerStats.rageActive)
		{
			rageMeter = 0;
			
			if(currentRageDuration > 0)
			{
				currentRageDuration -= Time.deltaTime;
				Debug.Log(currentRageDuration);
			}
			if(currentRageDuration <=0)
			{
				currentRageDuration = 0;
				DeactivateRageMode();
			}
		}
	}
	
	void IncreaseRageMeter(int amount)
	{
		rageMeter += amount;
		Debug.Log("rageMeter: " + rageMeter);
	}
	
	void IncreaseRageMeterByHalf(int amount)
	{
		double amountValue = amount;
		amountValue = Math.Round(amountValue / 2);
		rageMeter += (int)amountValue;
		Debug.Log("rageMeter: " + rageMeter);
	}

	public void Use()
	{
		if(!playerStats.rageActive)
		{
			if(rageReady)
			{
				InitiateRageMode();
			}
		}
	}

	public void PlayerEnteredRageCutscene()  
	{
		if(OnCutscene != null)
		{
			OnCutscene(rageClip);
		}
	}
	
	public static void RageCutsceneEnded()
	{
		if(OnCutsceneEnd != null)
		{
			OnCutsceneEnd();
		}
	}

	public void InitiateRageMode()
	{
		PlayerEnteredRageCutscene();
		rageInitiated = true;
	}
	
	public void ActivateRageMode()
	{
		if(rageInitiated)
		{
			playerStats.rageActive = true;
			currentRageDuration = playerStats.rageDuration;
			rageMeter = 0;
			AddBonusEffects();
			playerStats.PlayerActivatedRage();
		}
	}

	public void DeactivateRageMode()
	{
		//rageInitiated = false;
		rageMeter = 0;
		rageReady = false;
		playerStats.rageActive = false;
		RemoveBonusEffects();
		playerStats.PlayerDeactivatedRage();
	}
	
	public virtual void AddBonusEffects()
	{
		Debug.Log("Rage effects added.");
	}
	
	public virtual void RemoveBonusEffects()
	{
		Debug.Log("Rage effects removed.");
	}
	
	//TESTING ONLY:
	public int GetRageMeterInfo()
	{
		return rageMeter;
	}
	
	public float GetRageDuration()
	{
		return currentRageDuration;
	}

}

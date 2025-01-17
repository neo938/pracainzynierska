﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;
    public int adrenaline;
    public bool activeAimingAndShooting;

    public bool gameOver;
    public bool reachedFinalWaypoint;

    [Header("Audio clips")]
    public List<AudioClip> injuredSounds;
    public List<AudioClip> deadSounds;

    AudioSource audioSource;
    
    //public bool gameWon;

    [HideInInspector] public int healthMax;
    InjuryScript injuryScript;
    DeadScript deadScript;
    Transform EnemiesObject;
    WinScript winScript;
    bool winScriptLaunched;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        winScript = GameObject.Find("Win").GetComponent<WinScript>();
        EnemiesObject = GameObject.Find("Enemies").transform;
        healthMax = health;
        injuryScript = GameObject.Find("Injured").GetComponent<InjuryScript>();
        deadScript = GameObject.Find("Dead").GetComponent<DeadScript>();
    }
    public void TakeDamage(int strength)
    {
        health -= strength;
        injuryScript.Injured();
        if(health<=0)
        {
            deadScript.Dead();
            audioSource.PlayOneShot(deadSounds[(int)Random.Range(0f, deadSounds.Capacity-1)]);
            
        }
        else
        {
            audioSource.PlayOneShot(injuredSounds[(int)Random.Range(0f, injuredSounds.Capacity-1)]);
        }

    }

    public void SwitchActiveAimingAndShooting(bool condition)
    {
        activeAimingAndShooting = condition;
    }

    private void Update()
    {
        if(!winScriptLaunched && health>0 && reachedFinalWaypoint && EnemiesObject.childCount==0)
        {
            winScriptLaunched = true;
            winScript.Win();
        }
        
    }



}

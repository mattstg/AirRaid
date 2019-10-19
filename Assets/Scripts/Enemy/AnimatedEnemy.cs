﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimatedEnemy : MobileEnemy
{
<<<<<<< HEAD
    Animator anim;
    Vector3 targetBuilding;
=======

    protected Animator anim;
    protected float globalCoolDown; 
    protected float decisionTime; //Time it takes for the enemy to change decision CONSTANT - Makes the AI less reactive
    protected float timeSinceLastDecision;
    protected float detectionRadius;
    protected Vector2 attackRadius; // Could be set in Initialize, by cycling through the abilities to get the min and max range overall
    protected AnimatorOverrideController animatorOverride;
>>>>>>> 4b48f3f2ea684118ff94e451ab062684b4e23b7c
    public override void Initialize(float startingEnergy)
    {
        base.Initialize(startingEnergy);
        anim = GetComponent<Animator>();
    }

    public override void ModEnergy(float energyMod)
    {
        hp += energyMod;
        energy = 1;
    }
    public void SetTriggerAttack()
    {
        anim.SetTrigger("Attack");
    }
    public void SetAnimeVelocity(float velocity)
    {
        anim.SetFloat("Velocity",velocity);
    }

    public void DetectEnemyNearby() //Unsafe Function
    {
<<<<<<< HEAD
        int radius = 1;
        float buildingDistance = 0;
        float defenderDistance = 0;
        float distance;
        Vector3 positionBuilding = Vector3.zero;
=======
        int radius = 1; // Use the detectionRadius variable instead
        do
        {
            Collider[] collid = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask(""));
        } while (true); // This will Crash!!!!
>>>>>>> 4b48f3f2ea684118ff94e451ab062684b4e23b7c
        
        Collider[] collid = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Wall", "Building"));
        if(collid != null)
        {
            buildingDistance = Vector3.Distance(transform.position, collid[0].transform.position);
            foreach(Collider entity in collid)
            {
                distance = Vector3.Distance(transform.position, entity.transform.position);
                if(buildingDistance > distance)
                {
                    buildingDistance = distance;
                    positionBuilding = entity.transform.position;

                }
            }
            if (positionBuilding != targetBuilding)
            {
                targetBuilding = positionBuilding;
                navmeshAgent.SetDestination(targetBuilding);
            }
        }
    }
}

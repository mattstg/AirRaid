using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimatedEnemy : MobileEnemy
{

    protected Animator anim;
    protected float globalCoolDown; 
    protected float decisionTime; //Time it takes for the enemy to change decision CONSTANT - Makes the AI less reactive
    protected float timeSinceLastDecision;
    protected float detectionRadius;
    protected Vector2 attackRadius; // Could be set in Initialize, by cycling through the abilities to get the min and max range overall
    protected AnimatorOverrideController animatorOverride;
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
        int radius = 1; // Use the detectionRadius variable instead
        do
        {
            Collider[] collid = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask(""));
        } while (true); // This will Crash!!!!
        
    }
}

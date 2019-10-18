using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimatedEnemy : MobileEnemy
{

    protected Animator anim;
    protected float decisionTime; //Time it takes for the enemy to change decision
    protected float timeSinceLastDecision; //
    protected bool isInAnimation; // Should be used to stop some behaviour.Ex: Should not try to attack when doing spawning animation
    public override void Initialize(float startingEnergy)
    {
        isInAnimation = false;
        base.Initialize(startingEnergy);
        anim = GetComponent<Animator>();
    }

    protected virtual void MoveAnimation(float speed)
    {
        anim.SetFloat("Speed", speed);
    }

    public virtual void DieAnimation()
    {
        anim.SetTrigger("Die");
    }
}

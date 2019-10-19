using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimatedEnemy : MobileEnemy
{

    protected Animator anim;
    protected float globalCoolDown; 
    protected float decisionTime; //Time it takes for the enemy to change decision CONSTANT
    protected float timeSinceLastDecision; //
    public override void Initialize(float startingEnergy)
    {
        base.Initialize(startingEnergy);
        anim = GetComponent<Animator>();
    }

}

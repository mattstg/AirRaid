using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatedEnemy : MobileEnemy
{

    public GameObject target;
    public AnimationClip walk;
    public AnimationClip idle;
    public AnimationClip run;
    public AnimationClip death;
    protected Animator anim;
    protected float globalCoolDown; 
    protected float decisionTime; //Time it takes for the enemy to change decision CONSTANT - Makes the AI less reactive
    protected float timeSinceLastDecision;
    protected float detectionRadius;
    protected Vector2 attackRadius; // Could be set in Initialize, by cycling through the abilities to get the min and max range overall
    public EnemyAbilityManager enemyAbilityManager;
    [HideInInspector] public AnimatorOverrideController animatorOverrideController;
    public override void Initialize(float startingEnergy)
    {
        OverrideAnimatorController();
        enemyAbilityManager = new EnemyAbilityManager(this);
        base.Initialize(startingEnergy);
        
    }

    private void OverrideAnimatorController()
    {
        anim = GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = animatorOverrideController;
        animatorOverrideController["idle"] = idle;
        animatorOverrideController["death"] = death;
        animatorOverrideController["run"] = run;
        animatorOverrideController["walk"] = walk;
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
    public void SetAgent(Vector3 pos)
    {
        navmeshAgent.SetDestination(pos);
    }

    
}


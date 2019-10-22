using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatedEnemy : MobileEnemy
{
    public static int id;
    public int Id;
    public GameObject target;
    public Vector3 lastTargetPosition;
    public AnimationClip walk;
    public AnimationClip idle;
    public AnimationClip run;
    public AnimationClip death;
    public List<EnemyAbility> prefab_abilities;
    protected Animator anim;
    protected float globalCoolDown; 
    protected float decisionTime = 2; //Time it takes for the enemy to change decision CONSTANT - Makes the AI less reactive
    public float timeSinceLastDecision;
    public bool CanMakeDecision { get { return decisionTime + timeSinceLastDecision <= Time.time  ; } }
    protected float detectionRadius;
    protected Vector2 attackRadius; // Could be set in Initialize, by cycling through the abilities to get the min and max range overall
    [HideInInspector] public EnemyAbilityManager enemyAbilityManager;
    [HideInInspector] public AnimatorOverrideController animatorOverrideController;
    public override void Initialize(float startingEnergy)
    {
        Id = id;
        id++;
        base.Initialize(startingEnergy);
        


        enemyAbilityManager = new EnemyAbilityManager(this, CopiedAbilities());
        OverrideAnimatorController();
    }

    private List<EnemyAbility> CopiedAbilities()
    {
        List<EnemyAbility> abilities = new List<EnemyAbility>();
        foreach (EnemyAbility ability in prefab_abilities)
        {
            abilities.Add(Instantiate(ability));
        }
        return abilities;
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
        energy += energyMod;
    }
    public void SetTriggerAttack()
    {
        anim.SetTrigger("Attack");
    }

    public void SetAnimeVelocity(float velocity)
    {
        anim.SetFloat("Velocity",velocity);
    }

    public void SetAgent(Vector3 pos, Bounds enemyBounds)
    {
        Vector3 randomPos = new Vector3(Random.Range(-(enemyBounds.size.x) / 2, (enemyBounds.size.x) / 2), 0, Random.Range(-(enemyBounds.size.z) / 2, (enemyBounds.size.z) / 2)) + pos;
        navmeshAgent.SetDestination(randomPos);
    }

    public void ClearAgentDestination()
    {
        navmeshAgent.ResetPath();
    }

    public bool CheckTargetDestroy()
    {
        if (!target)
            return true;
        return false;
    }

    
}


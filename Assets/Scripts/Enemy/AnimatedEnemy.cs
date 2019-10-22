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
    public List<EnemyAbility> abilities;
    protected Animator anim;
    protected float globalCoolDown; 
    protected float decisionTime = 2; //Time it takes for the enemy to change decision CONSTANT - Makes the AI less reactive
    public float timeSinceLastDecision;
    public bool CanMakeDecision { get { return decisionTime + timeSinceLastDecision <= Time.time  ; } }
    protected float detectionRadius;
    protected Vector2 attackRadius; // Could be set in Initialize, by cycling through the abilities to get the min and max range overall
    public EnemyAbilityManager enemyAbilityManager;
    [HideInInspector] public AnimatorOverrideController animatorOverrideController;
    public override void Initialize(float startingEnergy)
    {
        OverrideAnimatorController();
        enemyAbilityManager = new EnemyAbilityManager(this, abilities);
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
    public void DieProcess(GameObject ghouleEvolution)
    {
        StartCoroutine(SpawnEvolution(ghouleEvolution));
        anim.SetTrigger("isDead");
    }
    public IEnumerator SpawnEvolution(GameObject ghouleEvolution)
    {
        yield return new WaitForSeconds(2);
        //GameObject troll = GameObject.Instantiate<GameObject>(ghouleEvolution, transform.parent);
        EnemyManager.Instance.SpawnEnemy(EnemyType.Troll, transform.position, 100);
        base.Die();
        
        //troll.transform.position = transform.position;
    }
}


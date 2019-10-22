using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Defender : MonoBehaviour, IHittable {
    public DefenderPck defenderInfos;
    protected AbilityAI myAbility;
    protected Animator myAnimator;

    protected NavMeshAgent agent;
    public FlockAgent flockAgent;

    public virtual void Initialize(Leader leader) { }
    protected virtual void Move() { }
    protected virtual void Rotate() { }

    protected virtual void PostInitialize(TypeDefender type, DefenderState currentState, AbilityAI ability, Leader leader) {
        this.defenderInfos = new DefenderPck(type, currentState);
        this.myAbility = ability;
        this.myAnimator = transform.GetComponent<Animator>();
        this.defenderInfos.myLeader = leader;

        this.agent = transform.GetComponent<NavMeshAgent>();
        this.flockAgent = GetComponent<FlockAgent>();
    }

    public virtual void Refresh() {
        this.defenderInfos.state = DefenderState.ON_FLOCK;
        if (DoIHaveTarget()) {
            //I have a target -> check if in combat range
            if (IsTargetInMyAttackRange()) {
                //Target in attack range -> attack
                this.defenderInfos.state = DefenderState.ON_ATTACK;
                DoAbility();
            }
            else if (IsTargetInMyVisionRange()) {
                //Target in vision range -> move toward target
                this.agent.SetDestination(this.defenderInfos.target.position);
            }
            else {
                //Target not in vision range yet -> move toward leader
                //this.agent.SetDestination(this.defenderInfos.myLeader.transform.position);
                List<Transform> context = GetNearbyObjects(flockAgent);

                //All behaviors have different CalculateMove()
                Vector3 move = defenderInfos.myLeader.flock.behavior.CalculateMove(flockAgent, context, defenderInfos.myLeader.flock);
                move *= defenderInfos.myLeader.flock.driveFactor;
                if (move.sqrMagnitude > defenderInfos.myLeader.flock.squareMaxSpeed)
                {
                    move = move.normalized * defenderInfos.myLeader.flock.maxSpeed;
                }
                flockAgent.Move(move);
            }
        }
        else {
            //Target not in vision range -> move toward leader
            //this.agent.SetDestination(this.defenderInfos.myLeader.transform.position);

            List<Transform> context = GetNearbyObjects(flockAgent);
            //All behaviors have different CalculateMove()
            Vector3 move = defenderInfos.myLeader.flock.behavior.CalculateMove(flockAgent, context, defenderInfos.myLeader.flock);
            move *= defenderInfos.myLeader.flock.driveFactor;
            if (move.sqrMagnitude > defenderInfos.myLeader.flock.squareMaxSpeed)
            {
                move = move.normalized * defenderInfos.myLeader.flock.maxSpeed;
            }
            flockAgent.Move(move);

        }
        this.defenderInfos.speed = this.agent.speed;
    }
    public virtual void PhysicRefresh() {
        RegenEnergy();
        GiveInfoToAnimator();
    }

    public virtual List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();

        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, defenderInfos.myLeader.flock.neighborRadius);

        foreach (Collider c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }

    protected virtual void DoAbility() {
        if (this.myAbility.UseAbility(this.defenderInfos, this.defenderInfos.target)) {
            this.myAnimator.SetTrigger("ON_ATTACK");
            this.defenderInfos.myLeader.PlaySound(this.defenderInfos.sounds[DefenderState.ON_ATTACK]);
        }
    }

    public virtual void Die() {
        this.myAnimator.SetTrigger("ON_DYING");
        this.defenderInfos.state = DefenderState.ON_DYING;
        this.defenderInfos.myLeader.PlaySound(this.defenderInfos.sounds[DefenderState.ON_DYING]);
        this.defenderInfos.myLeader.RemoveDefenderFromMyGroup(this);
    }

    private void RegenEnergy() {
        this.defenderInfos.energy += this.defenderInfos.energyRegen;
        this.defenderInfos.energy = Mathf.Clamp(this.defenderInfos.energy, 0, this.defenderInfos.energyMax);
    }

    protected bool DoIHaveTarget() {
        return this.defenderInfos.target != null;
    }

    protected bool IsTargetInMyAttackRange() {
        return IsTargetInRayCast(this.defenderInfos.attackRange);
    }

    protected bool IsTargetInMyVisionRange() {
        return IsTargetInRayCast(this.defenderInfos.visionRange);
    }

    private bool IsTargetInRayCast(float radius) {
        bool result = false;
        if (this.defenderInfos.target != null) {
            Collider[] hits = Physics.OverlapSphere(transform.position, radius);
            if (hits.Length > 0) {
                foreach (Collider hit in hits) {
                    if (hit.transform == this.defenderInfos.target.transform)
                        result = true;
                }
            }
        }
        return result;
    }

    public void SetTarget(Transform _target) {
        this.defenderInfos.target = _target;
    }

    private void GiveInfoToAnimator() {
        this.myAnimator.SetFloat("speedVelocity", this.defenderInfos.speed);
    }

    //Turn off because too loud
    private void Step() {
        //Called by event on the animation
        this.defenderInfos.myLeader.PlaySound(GetRandomWalkingSound());
    }

    private AudioClip GetRandomWalkingSound() {
        return this.defenderInfos.walkingSounds[Random.Range(0, this.defenderInfos.walkingSounds.Count - 1)];
    }

    public void HitByProjectile(float damage) {
        this.defenderInfos.hp -= damage;
        this.defenderInfos.hp = Mathf.Clamp(this.defenderInfos.hp, 0, this.defenderInfos.maxHp);
        if (this.defenderInfos.hp <= 0)
            Die();
    }
}

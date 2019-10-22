using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Defender : MonoBehaviour, IHittable {
    public DefenderPck defenderInfos;
    protected AbilityAI myAbility;
    protected Animator myAnimator;

    protected NavMeshAgent agent;

    public virtual void Initialize(Leader leader) { }
    protected virtual void Move() { }
    protected virtual void Rotate() { }

    protected virtual void PostInitialize(TypeDefender type, DefenderState currentState, AbilityAI ability, Leader leader) {
        this.defenderInfos = new DefenderPck(type, currentState);
        this.myAbility = ability;
        this.myAnimator = transform.GetComponent<Animator>();
        this.defenderInfos.myLeader = leader;

        this.agent = transform.GetComponent<NavMeshAgent>();
    }

    public virtual void Refresh() {
        HitByProjectile(0);
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
                this.agent.SetDestination(this.defenderInfos.myLeader.transform.position);
            }
        }
        else {
            //Target not in vision range -> move toward leader
            this.agent.SetDestination(this.defenderInfos.myLeader.transform.position);
        }
        this.defenderInfos.speed = this.agent.speed;
    }
    public virtual void PhysicRefresh() {
        RegenEnergy();
        GiveInfoToAnimator();
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
        bool result = false;
        if (this.defenderInfos.target != null) {
            Collider[] hits = Physics.OverlapSphere(transform.position, this.defenderInfos.attackRange);
            if (hits.Length > 0) {
                foreach (Collider hit in hits) {
                    if (hit.transform == this.defenderInfos.target.transform)
                        result = true;
                }
            }
        }
        return result;
    }

    protected bool IsTargetInMyVisionRange() {
        bool result = false;
        if (this.defenderInfos.target != null) {
            Collider[] hits = Physics.OverlapSphere(transform.position, this.defenderInfos.visionRange);
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
    private void OnDrawGizmos() {
        if (this.defenderInfos.target != null) {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, this.defenderInfos.attackRange);
        }
    }
}

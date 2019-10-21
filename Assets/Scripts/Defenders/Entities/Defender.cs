using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Defender : MonoBehaviour, IHittable {
    public DefenderPck defenderInfos;
    protected AbilityAI myAbility;
    protected Animator myAnimator;

    public virtual void Initialize() { }
    protected virtual void Move() { }
    protected virtual void Rotate() { }

    protected virtual void PostInitialize(TypeDefender type, DefenderState currentState, AbilityAI ability) {
        this.defenderInfos = new DefenderPck(type, currentState);
        this.myAbility = ability;
        this.myAnimator = transform.GetComponent<Animator>();
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
            else {
                //Target not in attack range -> move toward target
            }
        }
        else {
            //I don't have a target -> move toward leader
        }
    }
    public virtual void PhysicRefresh() {
        RegenEnergy();
        GiveInfoToAnimator();
    }
    protected virtual void DoAbility() {
        if (this.myAbility.UseAbility(this.defenderInfos, this.defenderInfos.target))
            this.myAnimator.SetTrigger("ON_ATTACK");
    }

    public virtual void Die() {
        this.myAnimator.SetTrigger("ON_DYING");
        this.defenderInfos.state = DefenderState.ON_DYING;
        this.defenderInfos.myLeader.RemoveDefenderFromMyGroup(this);
        GameObject.Destroy(gameObject);
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

    public void SetTarget(Transform _target) {
        this.defenderInfos.target = _target;
    }

    private void GiveInfoToAnimator() {
        this.myAnimator.SetFloat("speedVelocity", this.defenderInfos.speed);
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
            Gizmos.DrawSphere(this.defenderInfos.target.transform.position, 0.2f);
        }
    }
}

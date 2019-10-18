using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Defender : MonoBehaviour, IHittable {
    public DefenderPck defenderInfos;
    protected AbilityAI myAbility;
    protected Animator myAnimator;


    protected virtual void PostInitialize(TypeDefender type, DefenderState currentState, AbilityAI ability) {
        this.defenderInfos = new DefenderPck(type, currentState);
        this.myAbility = ability;
        this.myAnimator = transform.GetComponent<Animator>();
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

    public virtual void Initialize() { }
    public virtual void Refresh() { }
    public virtual void PhysicRefresh() {
        RegenEnergy();
        GiveInfoToAnimator();
    }
    protected virtual void Move() { }
    protected virtual void Rotate() { }
    protected virtual void DoAbility() {
        this.myAnimator.SetTrigger("ON_ATTACK");
    }
    public virtual void Die() {
        this.myAnimator.SetTrigger("ON_DYING");
    }

    private void OnDrawGizmos() {
        if (this.defenderInfos.target != null) {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(this.defenderInfos.target.transform.position, 0.5f);
        }
    }
    public void HitByProjectile(float damage) {
        this.defenderInfos.hp -= damage;
        this.defenderInfos.hp = Mathf.Clamp(this.defenderInfos.hp, 0, this.defenderInfos.maxHp);
        if (this.defenderInfos.hp <= 0)
            Die();
    }
}

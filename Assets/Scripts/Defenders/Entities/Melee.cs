using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Defender, IHittable
{
    public void Initialise() {
        base.Initialise(TypeDefender.MELEE, DefenderState.ON_IDLE, new MeleeAttack());
    }

    public override void Refresh() {
        if (base.DoIHaveTarget()) {
            //I have a target -> check if in combat range
            if (base.IsTargetInMyAttackRange()) {
                //Target in attack range -> attack
                DoAbility();
            }
            else {
                //Target not in attack range -> move toward target
            }
        }
        else {
            //I don't have a target -> find a target
        }
    }

    public override void PhysicRefresh() {
        Move();
        Rotate();
    }

    protected override void Move() {
    }

    protected override void Rotate() {
    }

    protected override void FindTarget() {
    }

    protected override void DoAbility() {
        ((MeleeAttack)this.myAbility).UseAbility(this.defenderInfos);
    }


    public override void Die() {
        //remove from managers
        GameObject.Destroy(gameObject);
    }

    public void HitByProjectile(float damage) {
        this.defenderInfos.hp -= damage;
        if (this.defenderInfos.hp <= 0)
            Die();
    }
}

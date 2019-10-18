using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DefenderSmart : Defender {
    protected override void PostInitialize(TypeDefender type, DefenderState currentState, AbilityAI ability) {
        base.PostInitialize(type, currentState, ability);
    }

    public override void Refresh() {
        base.Refresh();
        this.defenderInfos.state = DefenderState.ON_FLOCK;
        if (base.DoIHaveTarget()) {
            //I have a target -> check if in combat range
            if (base.IsTargetInMyAttackRange()) {
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
        Debug.Log("" + this.defenderInfos.state);
    }

    public override void PhysicRefresh() {
        base.PhysicRefresh();
    }

    protected override void DoAbility() {
        if (this.myAbility.UseAbility(this.defenderInfos, this.defenderInfos.target))
            base.DoAbility();
    }

    public override void Die() {
        base.Die();
        this.defenderInfos.state = DefenderState.ON_DYING;
        DefenderManager.Instance.RemoveDefenderToList(this);
        GameObject.Destroy(gameObject);
    }
}

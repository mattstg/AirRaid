using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Defender : MonoBehaviour {

    protected DefenderPck defenderInfos;
    protected AbilityAI myAbility;

    public void Initialise(TypeDefender type, DefenderState currentState, AbilityAI ability) {
        this.defenderInfos = new DefenderPck(type, currentState);
        this.myAbility = ability;
    }

    public bool DoIHaveTarget() {
        return this.defenderInfos.target != null;
    }

    public bool IsTargetInMyAttackRange() {
        return true;
        //Do a raycast with range
    }
    public virtual void Refresh() { }
    public virtual void PhysicRefresh() { }
    protected virtual void Move() { }
    protected virtual void Rotate() { }
    protected virtual void FindTarget() { }
    protected virtual void DoAbility() { }
    public virtual void Die() { }
}

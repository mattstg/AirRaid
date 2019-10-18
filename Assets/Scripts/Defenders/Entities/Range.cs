using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : DefenderSmart {

    public override void Initialize() {
        base.PostInitialize(TypeDefender.RANGE, DefenderState.ON_IDLE, new RangeAttack());
    }

    public override void PhysicRefresh() {
        base.PhysicRefresh();
        Move();
        Rotate();
    }

    protected override void Move() {
        
    }

    protected override void Rotate() {
        
    }
}




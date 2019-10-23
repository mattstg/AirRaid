using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : Defender {

    public override void Initialize(Leader leader) {
        base.PostInitialize(TypeDefender.RANGE, DefenderState.ON_IDLE, new RangeAttack(), leader);
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




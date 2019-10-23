using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Defender {

    public override void Initialize(Leader leader) {
        base.PostInitialize(TypeDefender.MELEE, DefenderState.ON_IDLE, new MeleeAttack(), leader);
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


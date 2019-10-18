using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : DefenderSmart {

    public override void Initialize() {
        base.PostInitialize(TypeDefender.MELEE, DefenderState.ON_IDLE, new MeleeAttack());
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


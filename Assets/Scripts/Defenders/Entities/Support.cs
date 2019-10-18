using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Support : DefenderSmart {

    public override void Initialize() {
        base.PostInitialize(TypeDefender.SUPPORT, DefenderState.ON_IDLE, new HealAttack());
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Support : Defender {

    public override void Initialize(Leader leader) {
        base.PostInitialize(TypeDefender.SUPPORT, DefenderState.ON_IDLE, new HealAttack(), leader);
        
    }

    //Special for support because dont always attack : ex: if all units are full hp
    public override void Refresh() {
        this.defenderInfos.state = DefenderState.ON_FLOCK;
        if (base.DoIHaveTarget()) {
            //I have a target -> check if in combat range
            if (base.IsTargetInMyAttackRange() && DoTargetNeedHealing()) {
                //Target in attack range -> attack
                this.defenderInfos.state = DefenderState.ON_ATTACK;
                DoAbility();
            }
            else if (DoTargetNeedHealing() && !base.IsTargetInMyAttackRange()) {
                //Target not in attack range -> move toward target
                this.agent.SetDestination(this.defenderInfos.target.position);
            }
            else {
                //Target doesnt need healing -> move toward leader
                this.agent.SetDestination(this.defenderInfos.myLeader.transform.position);
            }
        }
        else {
            //I don't have a target -> move toward leader
            this.agent.SetDestination(this.defenderInfos.myLeader.transform.position);
        }
        this.defenderInfos.speed = this.agent.speed;
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

    private bool DoTargetNeedHealing() {
        bool result = false;
        DefenderPck infoOnTarget = this.defenderInfos.target.GetComponent<Defender>().defenderInfos;
        if (infoOnTarget.hp < infoOnTarget.maxHp)
            result = true;
        return result;
    }
}

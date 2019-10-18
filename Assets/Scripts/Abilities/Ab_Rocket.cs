using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_Rocket : Ability {
    readonly float REGENERATION_AMOUNT = 5;

    public Ab_Rocket(PlayerController _pc) : base(_pc) {
        stats = new AbilityStats(this, Abilities.EnergyBoost, UpdateType.FixedUpdate, .5f, 5f);

    }
    public override void AbilityPressed() {
        base.AbilityPressed();
    }

    public override void AbilityHeld() {
        UseAbility();
        base.AbilityHeld();
    }

    public override bool UseAbility() {
        if (base.UseAbility()) {
            Debug.Log("Shoot Rocket");
            return true;
        }
        return false;
    }

    public override void AbilityRelease() {
        base.AbilityRelease();
    }

    public override void AbilityUpdate() {
        base.AbilityUpdate();
    }


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_NitroBoost : Ability {
    readonly float NITRO_BOOST = 5;

    public Ab_NitroBoost(PlayerController _pc) : base(_pc) {
        stats = new AbilityStats(this, Abilities.NitroBoost, UpdateType.FixedUpdate, 1f, 5f);

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
            Debug.Log("NITROBOOST: " + NITRO_BOOST);
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
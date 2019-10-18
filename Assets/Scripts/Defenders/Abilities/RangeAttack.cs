using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : AbilityAI {

    public RangeAttack() : base(GlobalDefendersVariables.Instance.RA_DELAY_ABILITY, GlobalDefendersVariables.Instance.RA_ABILITY_COST) { }

    public override bool UseAbility(DefenderPck defenderPck, Transform target) {
        if (base.AbilityAvailable(defenderPck)) {
            //call animation, instantiate if something to, etc   (Personal stuff about melee attack)
            base.UseAbility(defenderPck, target);
            return true;
        }
        else
            return false;

    }
}

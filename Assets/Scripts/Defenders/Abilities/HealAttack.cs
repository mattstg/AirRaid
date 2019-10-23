using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAttack : AbilityAI {

    public HealAttack() : base(GlobalDefendersVariables.Instance.SP_DELAY_ABILITY, GlobalDefendersVariables.Instance.SP_ABILITY_COST) { }

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

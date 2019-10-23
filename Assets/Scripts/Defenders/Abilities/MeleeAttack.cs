using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : AbilityAI {

    public MeleeAttack() : base(GlobalDefendersVariables.Instance.ML_DELAY_ABILITY, GlobalDefendersVariables.Instance.ML_ABILITY_COST) { }

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

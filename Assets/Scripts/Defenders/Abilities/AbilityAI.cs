using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAI {
    private float coldown;
    private float energyCost;
    private float lastTimeAbilityUsed;

    public AbilityAI(float _coldown, float _energyCost) {
        this.coldown = _coldown;
        this.energyCost = _energyCost;
    }

    public virtual bool UseAbility(DefenderPck defenderPck, Transform target) {
        target.GetComponent<IHittable>().HitByProjectile(defenderPck.damage);
        defenderPck.energy -= this.energyCost;
        this.lastTimeAbilityUsed = Time.time;
        return true;
    }

    public bool AbilityAvailable(DefenderPck defenderPck) {
        bool result = false;
        if (Time.time - this.lastTimeAbilityUsed >= this.coldown && defenderPck.energy >= this.energyCost) {
            result = true;
        }
        return result;
    }
}

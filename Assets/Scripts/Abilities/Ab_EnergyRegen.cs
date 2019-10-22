using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_EnergyRegen : Ability
{
    readonly float REGENERATION_AMOUNT = 5;

    public Ab_EnergyRegen(PlayerController _pc) : base(_pc)
    {
        stats = new AbilityStats(this, Abilities.EnergyBoost, UpdateType.FixedUpdate, 1f, 5f);

    }
    public override void AbilityPressed()
    {
        base.AbilityPressed();
    }

    public override void AbilityHeld()
    {
        UseAbility();
        base.AbilityHeld();
    }

    public override bool UseAbility()
    {
        if (base.UseAbility())  
        {
            PlayerManager.Instance.player.stats.maxEnergy += REGENERATION_AMOUNT;
            Debug.Log("new max energy: " + PlayerManager.Instance.player.stats.maxEnergy + REGENERATION_AMOUNT);
            return true;
        }
        return false;
    }

    public override void AbilityRelease()
    {
        base.AbilityRelease();
    }

    public override void AbilityUpdate()
    {
        base.AbilityUpdate();
    }

    
}

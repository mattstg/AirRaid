using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Machine gun ability, rapidly shoot from players turrets
public class Ab_MachineGun : Ability
{
    public Ab_MachineGun(PlayerController _pc) : base(_pc)
    {
        updateType = UpdateType.Update;
        abilityType = Abilities.Turrets;

    }
    public override void AbilityPressed()
    {
        Debug.Log("Here comes the...");
        base.AbilityPressed();
    }

    public override void AbilityHeld()
    {
        Debug.Log("brrrrrrrrrrrrttttttttttttttttttttttttttttttttttttttttttt");
        base.AbilityHeld();
    }

    public override void AbilityRelease()
    {
        Debug.Log("Ability Released");
        base.AbilityRelease();
    }

    public override void AbilityUpdate()
    {
        Debug.Log("Ability Updated");
        base.AbilityUpdate();
    }

    
}

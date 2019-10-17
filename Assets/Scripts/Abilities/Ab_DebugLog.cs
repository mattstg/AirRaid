using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_DebugLog : Ability
{

    public Ab_DebugLog(PlayerController _pc) : base(_pc)
    {

        stats = new AbilityStats(this, Abilities.Turrets, UpdateType.Update, .2f, 5f);
    }
    // Start is called before the first frame update
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
            Debug.Log("that's pretty cool!");
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

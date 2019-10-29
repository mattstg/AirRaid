using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_Rewind : Ability
{
    readonly float BULLET_SPEED = 200;
    readonly float BULLET_LIFESPAN = 5;
    readonly float ABILITY_COUNTER = 2f;
    

    public Ab_Rewind(PlayerController _pc) : base(_pc)
    {
        stats = new AbilityStats(this, Abilities.Rewind, UpdateType.FixedUpdate, 0.01f, 0.1f);

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
            Rewind();     
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

    public void Rewind()
    {
        pc.isRecording = false;
        if (pc.recPos.Count > 0)
        {
            Debug.Log("aa");
            pc.transform.position = (Vector3)pc.recPos[pc.recPos.Count - 1];
            pc.recPos.RemoveAt(pc.recPos.Count - 1);
        }
        else
            pc.isRecording = true;
    }

}

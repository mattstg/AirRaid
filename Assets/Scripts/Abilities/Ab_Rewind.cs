using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_Rewind : Ability
{
    readonly float BULLET_SPEED = 200;
    readonly float BULLET_LIFESPAN = 5;
    readonly float ABILITY_COUNTER = 2f;
    float timer = 3f;

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
        pc.recPos.Clear();
        pc.isRecording = true;
        timer = 3f;
    }

    public override void AbilityUpdate()
    {
        base.AbilityUpdate();
    }

    public void Rewind()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 3f;
            AbilityRelease();
        }
        if (pc.recPos.Count > 0)
        {
            pc.isRecording = false;
            pc.transform.position = (Vector3)pc.recPos[pc.recPos.Count - 1];
            pc.recPos.RemoveAt(pc.recPos.Count - 1);
        }
        
    }

}

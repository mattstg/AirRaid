using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_Rewind : Ability
{
    readonly float ENERGY_COST = 0.5f;
    readonly float COOLDOWN = 0.01f;
    readonly float MAXIMUM_DURATION = 3f;
    float timer = 3f;

    public Ab_Rewind(PlayerController _pc) : base(_pc)
    {
        stats = new AbilityStats(this, Abilities.Rewind, UpdateType.FixedUpdate, COOLDOWN, ENERGY_COST);

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
        pc.recordingArray.Clear();
        //pc.recPos.Clear();
        pc.isRecording = true;
        timer = MAXIMUM_DURATION;
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
            timer = MAXIMUM_DURATION;
            AbilityRelease();
        }
        if (pc.recordingArray.Count > 0)
        {
            pc.isRecording = false;
            PlayerController.PlayerRecording pr2 = (PlayerController.PlayerRecording) pc.recordingArray[pc.recordingArray.Count - 1];
            pc.transform.position = pr2.pos;
            pc.transform.rotation = pr2.rot;
            pc.rb.velocity = pr2.velo;
            pc.recordingArray.RemoveAt(pc.recordingArray.Count - 1);
            //pc.transform.position = (Vector3)pc.recordingArray[pc.recPos.Count - 1];
            //pc.recPos.RemoveAt(pc.recPos.Count - 1);
        }
        
    }

}

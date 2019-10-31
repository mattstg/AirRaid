using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_Rewind : Ability
{
    readonly float ENERGY_COST = 0.5f;
    readonly float COOLDOWN = 0.01f;
    readonly float MAXIMUM_COOLDOWN_DURATION = 3f;
    float counter = 0;
    bool isRecording = true;
    float rewindCooldown = 4f;
    int numofelements;
    ArrayList recordingArray = new ArrayList();

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
        if (rewindCooldown <= 0)
        {
            if (base.UseAbility())
            {
                Rewind();
                return true;
            }
        }
        return false;
    }

    public override void AbilityRelease()
    {
        base.AbilityRelease();
        isRecording = true;
        pc.DefaultMaterial();
        if (recordingArray.Count < numofelements)
            rewindCooldown = MAXIMUM_COOLDOWN_DURATION;
    }

    public override void AbilityUpdate()
    {
        base.AbilityUpdate();
        if(isRecording)
            PlayerRecorder(MAXIMUM_COOLDOWN_DURATION);
    }

    private void Rewind()
    {
        if (recordingArray.Count > 0 && pc.stats.currentEnegy > 0)
        {
            isRecording = false;
            pc.ChangeMaterial(pc.blue);
            pc.audioSrc.PlayOneShot(pc.rewindSFX);
            PlayerController.PlayerRecording pr2 = (PlayerController.PlayerRecording)recordingArray[recordingArray.Count - 1];
            pc.transform.position = pr2.pos;
            pc.transform.rotation = pr2.rot;
            pc.rb.velocity = pr2.velo;
            recordingArray.RemoveAt(recordingArray.Count - 1);
        }
        else
        {
            AbilityRelease();
        }


    }
    private void PlayerRecorder(float timeToTrack)
    {
        counter += Time.deltaTime;
        if (isRecording)
        {
            rewindCooldown -= Time.deltaTime;
            PlayerController.PlayerRecording pr = new PlayerController.PlayerRecording(pc.transform.position, pc.rb.velocity, pc.transform.rotation);
            recordingArray.Add(pr);
        }

        if (counter <= timeToTrack)
        {
            numofelements = recordingArray.Count;
        }
        if (recordingArray.Count > numofelements)
        {
            recordingArray.RemoveRange(0, recordingArray.Count - numofelements);
        }
    }

}

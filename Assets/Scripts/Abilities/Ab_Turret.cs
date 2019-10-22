using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_Turret : Ability
{
    protected PlayerController pc;

    private GameObject turretPrefab;
    private Transform turretParent;

    public Ab_Turret(PlayerController _pc) : base(_pc)
    {
        stats = new AbilityStats(this, Abilities.TurretDrop, UpdateType.FixedUpdate, 5f, 5f);
        pc = _pc;
        turretPrefab = Resources.Load<GameObject>("Prefabs/Turret");
        turretParent = (new GameObject("TurretParent")).transform;
    }



    public override void AbilityPressed()
    {
        UseAbility();
        base.AbilityPressed();

    }
    public override void AbilityHeld()
    {
        base.AbilityHeld();

    }
    public override void AbilityRelease()
    {
        base.AbilityRelease();

    }
    //returns if the ability is used, should be overwritten by child, check Ab_MachineGun
    public override bool UseAbility()
    {
        if (stats.canUseAbility)
        {
            Debug.Log("spawning turret");
            Vector3 bombBay = pc.bodyParts[BodyPart.BodyPart_BombBay][0];
            GameObject.Instantiate(turretPrefab, (bombBay + pc.transform.position), Quaternion.identity, turretParent.transform);
        }
        return false;
    }

    //Update every frame, regardless of pressed or not
    public override void AbilityUpdate()
    {
        base.AbilityUpdate();
    }

    
}

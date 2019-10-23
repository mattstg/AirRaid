using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_Turret : Ability
{


    private GameObject turretPrefab;
    private Transform turretParent;
    private float turretAbTimer;

    public Ab_Turret(PlayerController _pc) : base(_pc)
    {
        stats = new AbilityStats(this, Abilities.TurretDrop, UpdateType.FixedUpdate, 10f, 20f);

        turretPrefab = Resources.Load<GameObject>("Prefabs/Turret");
        turretParent = (new GameObject("TurretParent")).transform;
        turretAbTimer = 5f;
    }



    public override void AbilityPressed()
    {
        if (turretAbTimer < 0)
        {
            UseAbility();
            base.AbilityPressed();
        }

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
            pc.stats.currentEnegy -= stats.energyCost;
            GameObject.Instantiate(turretPrefab, (bombBay + pc.transform.position), Quaternion.identity, turretParent.transform);
            turretAbTimer = 5f;
            return true;
        }
        return false;
    }

    //Update every frame, regardless of pressed or not
    public override void AbilityUpdate()
    {
        turretAbTimer -= Time.deltaTime;
        if (turretAbTimer < 0)
        {
            Debug.Log("Turret ready to spawn");
        }
        base.AbilityUpdate();
    }

    
}

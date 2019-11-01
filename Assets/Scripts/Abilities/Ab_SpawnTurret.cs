using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_SpawnTurret : Ability
{

    const float TURRET_LIFESPAN = 20;
    public Ab_SpawnTurret(PlayerController _pc) : base(_pc)
    {
        stats = new AbilityStats(this, Abilities.Turrets, UpdateType.Update, .2f, 30f);
    }
    // Start is called before the first frame update
    public override void AbilityPressed()
    {
        UseAbility();
        base.AbilityPressed();
    }

    public override void AbilityHeld()
    {
        base.AbilityHeld();
    }

    public override bool UseAbility()
    {
        if (base.UseAbility())
        {
           Turret t = TurretManager.Instance.SpawnTurret(pc.bodyParts[BodyPart.BodyPart_Turret][0] + pc.transform.position, TURRET_LIFESPAN);
            t.Initialize();
            t.PostInitialize();
            Collider c = t.gameObject.GetComponentInChildren<Collider>();
            Physics.IgnoreCollision(c, pc.GetComponent<Collider>()); 
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

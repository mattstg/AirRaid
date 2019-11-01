using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_SpawnTurret : Ability
{

    const float TURRET_LIFESPAN = 60;
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
            Projectile p = BulletManager.Instance.CreateProjectile(ProjectileType.Turret,pc.bodyParts[BodyPart.BodyPart_Turret][0] + pc.transform.position, -pc.transform.up, pc.rb.velocity, TURRET_LIFESPAN, 0);
            Collider c = p.gameObject.GetComponentInChildren<Collider>();
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

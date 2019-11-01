using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_ShootDown : Ability
{
    readonly float BULLET_SPEED = 20;
    readonly float BULLET_LIFESPAN = 5;

    public Ab_ShootDown(PlayerController _pc) : base(_pc)
    {
        stats = new AbilityStats(this, Abilities.Turrets, UpdateType.FixedUpdate, .2f, 5f,0f);

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
            foreach (Vector3 gunTurretLocation in pc.bodyParts[BodyPart.BodyPart_Turret])
                BulletManager.Instance.CreateProjectile(ProjectileType.BasicBullet, gunTurretLocation + pc.transform.position, -pc.transform.up, pc.rb.velocity, BULLET_LIFESPAN, BULLET_SPEED);
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

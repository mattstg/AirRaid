using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Machine gun ability, rapidly shoot from players turrets
public class Ab_BombDrop : Ability
{
    readonly float BOMB_LIFESPAN = 6;

    public Ab_BombDrop(PlayerController _pc) : base(_pc)
    {
        stats = new AbilityStats(this, Abilities.Bomb, UpdateType.Update, .2f, 30f);

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

    public override bool UseAbility()
    {
        if (base.UseAbility())  
        {
            foreach (Vector3 gunTurretLocation in pc.bodyParts[BodyPart.BodyPart_BombBay])
                BulletManager.Instance.CreateProjectile(ProjectileType.BasicBullet, gunTurretLocation + pc.transform.position, -pc.transform.up, pc.rb.velocity, BOMB_LIFESPAN, 0);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Machine gun ability, rapidly shoot from players turrets
public class Ab_BombDrop : Ability
{
    readonly float BOMB_LIFESPAN = 6;

    public Ab_BombDrop(PlayerController _pc) : base(_pc)
    {
        stats = new AbilityStats(this, Abilities.Bomb, UpdateType.Update, 2f, 30f);
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
            Projectile p = BulletManager.Instance.CreateProjectile(ProjectileType.Bomb, pc.bodyParts[BodyPart.BodyPart_BombBay][0] + pc.transform.position, -pc.transform.up, pc.rb.velocity, BOMB_LIFESPAN, 0);
            Collider c = p.gameObject.GetComponentInChildren<Collider>();
            Physics.IgnoreCollision(c, pc.GetComponent<Collider>());  //make bomb not collide with player
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

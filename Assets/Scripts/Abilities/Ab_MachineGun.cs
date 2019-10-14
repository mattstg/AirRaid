using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Machine gun ability, rapidly shoot from players turrets
public class Ab_MachineGun : Ability
{
    public Ab_MachineGun(PlayerController _pc) : base(_pc)
    {
        stats = new AbilityStats(this, Abilities.Turrets, UpdateType.FixedUpdate, .2f, 5f);

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
                BulletManager.Instance.CreateProjectile(ProjectileType.BasicBullet, gunTurretLocation + pc.transform.position, pc.transform.forward, pc.rb.velocity);
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

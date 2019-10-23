using UnityEngine;

//Machine gun ability, rapidly shoot from players turrets
public class Ab_MachineGun : Ability
{
    readonly float BULLET_SPEED = 200;
    readonly float BULLET_LIFESPAN = 5;
    private AudioSource[] ShipSounds;
    private AudioSource machineGun;

    public Ab_MachineGun(PlayerController _pc) : base(_pc)
    {
        stats = new AbilityStats(this, Abilities.Turrets, UpdateType.FixedUpdate, .2f, 5f);
        ShipSounds = pc.GetComponents<AudioSource>();
        foreach(AudioSource sounds in ShipSounds)
        {
            if(sounds.clip.name == "machineGun&Turret")
            {
                machineGun = sounds;
            }
        }

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
            {
                machineGun.Play();
                BulletManager.Instance.CreateProjectile(ProjectileType.BasicBullet, gunTurretLocation + pc.transform.position, pc.transform.forward, pc.rb.velocity, BULLET_LIFESPAN, BULLET_SPEED);
            }
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

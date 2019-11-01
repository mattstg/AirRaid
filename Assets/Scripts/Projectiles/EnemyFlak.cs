using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlak : Projectile
{
    readonly float ENERGY_PER_FLAK = .75f;
    readonly float SIZE_MULT = 1.5f;

    float energy;
    float timeExplodes;

    public override void Initialize(Vector3 _firingDir, Vector3 _playerVelocityOnLaunch, float _lifespan, float _speed)
    {
        base.Initialize(_firingDir, _playerVelocityOnLaunch, _lifespan, _speed);

    }

    //Called specially by the enemy flak shooter
    public void InitializeEnemyFlak(float _energy)
    {
        energy = _energy;
        transform.localScale = Mathf.Pow(_energy, 1 / 3f) * SIZE_MULT * Vector3.one; //Make the size porportional to the amount of energy inside
    }

    public override void UpdateProjectile()
    {
        base.UpdateProjectile();
    }

    protected override void LifespanExpired()
    {
        ExplodeIntoFlak();
        base.LifespanExpired(); //kills projectile
    }

    protected override void HitTarget(IHittable targetHit, string layerMask)
    {
        targetHit.HitByProjectile(energy);
    }

    private void ExplodeIntoFlak()
    {
        GetComponent<AudioSource>().Play();
        int numberOfFlak = (int)(energy / ENERGY_PER_FLAK);
        for(int i = 0; i < numberOfFlak; i++)
        {
            Vector3 dir = Random.onUnitSphere;
            BulletManager.Instance.CreateProjectile(ProjectileType.BasicBullet, transform.position, dir, Vector3.zero, 1.5f, 15);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Vector3 firingDir;
    protected Vector3 playerVelocityOnLaunch;  //to be added to the speed

    public virtual void Initialize(Vector3 _firingDir, Vector3 _playerVelocityOnLaunch)
    {
        firingDir = _firingDir.normalized;  //just in case the caller didnt give us a normalized one, but they should
        playerVelocityOnLaunch = _playerVelocityOnLaunch;
    }

    public virtual void UpdateProjectile()
    {

    }

    public virtual void DestroyProjectile()
    {
        BulletManager.Instance.ProjectileDied(this);
    }

}

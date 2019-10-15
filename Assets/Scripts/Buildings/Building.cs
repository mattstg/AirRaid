using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IHittable
{
    public float hp;

    public void Initialize(float _hp)
    {
        hp = _hp;
    }

    public void HitByProjectile(float damage)
    {
        hp -= damage;
        if (hp <= 0)
            BuildingDied();
    }

    public virtual void BuildingDied()
    {
        BuildingManager.Instance.BuildingDied(this);
    }
}

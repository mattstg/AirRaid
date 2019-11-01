using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IHittable
{
    public float hp;
    public AudioSource audioObject;

    public void Initialize(float _hp)
    {
        hp = _hp;
        audioObject = GameObject.Find("AudioObject").GetComponent<AudioSource>();
    }

    public void HitByProjectile(float damage)
    {
        hp -= damage;
        if (hp <= 0)
            BuildingDied();
    }

    public virtual void BuildingDied()
    {
        audioObject.transform.position = this.transform.position;
        audioObject.Play();
        BuildingManager.Instance.BuildingDied(this);
    }
}

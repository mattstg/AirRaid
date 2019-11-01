﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IHittable
{
    public float hp, maxHP;
    public AudioSource audioObject;
    public GameObject Smoke;

    public void Initialize(float _hp)
    {
        hp = _hp;
        maxHP = hp;
        audioObject = GameObject.Find("AudioObject").GetComponent<AudioSource>();
        Smoke = Resources.Load<GameObject>("Prefabs/Smoke/WhiteSmoke");
    }

    public void Refresh()
    {
        DoShader();
    }

    public void HitByProjectile(float damage)
    {
        hp -= damage;
        if (hp <= maxHP / 2)
            DoSmoke();
        if (hp <= 0)
            BuildingDied();
    }

    public virtual void BuildingDied()
    {
        audioObject.transform.position = this.transform.position;
        audioObject.Play();
        BuildingManager.Instance.BuildingDied(this);
    }

    public void DoShader()
    {
        Renderer r = this.GetComponent<Renderer>();
        r.material.SetFloat("GlowEmissionMultiplier", (maxHP - hp) / maxHP * 30);
        r.material.SetFloat("GlowColorIntensity", (maxHP - hp) / maxHP * 10);
        r.material.SetFloat("GlowBaseFrequency", (maxHP -  hp) / maxHP * 3.5f);
    }

    public void DoSmoke()
    {
        var buildingSmoke = GameObject.Instantiate(Smoke, transform.position,Quaternion.Euler(-90,0,0));
        buildingSmoke.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        buildingSmoke.transform.SetParent(this.transform);
    }
}

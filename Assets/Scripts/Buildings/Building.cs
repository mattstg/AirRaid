using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IHittable
{
    public void HitByProjectile(float damage)
    {
        Debug.Log("Building was hit: " + damage);
    }
}

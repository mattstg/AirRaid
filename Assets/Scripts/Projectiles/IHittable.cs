using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    void HitByProjectile(float damage); 
}

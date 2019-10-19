using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType { Melee, AOE }
public class EnemyAbility : ScriptableObject
{
    public Enemy enemy;
    public LayerMask hittableLayer;
    public Vector2 distance;
    public float damage;
    public float cooldown;
    public float timeLastUsed;
    public float timeBeforeHit; // Depends on the animation. 0 would be instant
    public virtual void Initialize(Enemy _enemy)
    {

    }
    public virtual void TriggerAbility()
    {

    }
}

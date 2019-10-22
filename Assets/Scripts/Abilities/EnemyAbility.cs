using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum AbilityType { Melee, AOE }
public abstract class EnemyAbility : ScriptableObject
{
    protected AnimatedEnemy enemy;
    public AnimationClip animation;
    public LayerMask hittableLayer;
    public Vector2 range;
    public float damage;
    public float cooldown;
    [HideInInspector] public float timeLastUsed;
    public float timeBeforeHit;
    public string triggerParam;

    public bool canUseAbility { get { return Time.time - timeLastUsed >= cooldown; } }

    public abstract void Initialize(AnimatedEnemy _enemy);
    public abstract void UseAbility();
    public abstract bool WillHitTarget();

}

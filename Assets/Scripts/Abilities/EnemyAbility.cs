using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum AbilityType { Melee, AOE }
public class EnemyAbility : ScriptableObject
{
    protected Enemy enemy;
    public AnimationClip animation;
    public LayerMask hittableLayer;
    public Vector2 Range;
    public float damage;
    public float cooldown;
    [HideInInspector] public float timeLastUsed;
    public float timeBeforeHit;
    public string triggerParam;

    public bool canUseAbility { get { return Time.time - timeLastUsed >= cooldown; } }

    public virtual void Initialize(Enemy _enemy)
    {

    }
    public virtual void UseAbility()
    {
    }

}

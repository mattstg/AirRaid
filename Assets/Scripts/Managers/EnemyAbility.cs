using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType { Melee, AOE }
public class EnemyAbility
{
    public AbilityStats stats;
    protected Enemy enemy;
    public EnemyAbility(Enemy _enemy)
    {
        enemy = _enemy;
    }

    public virtual bool UseAbility()
    {
        if (stats.canUseAbility)
        {
            stats.timeAbilityLastUsed = Time.time;
            return true;
        }
        return false;
    }

    [CreateAssetMenu (menuName = "Ability")]
    public class AbilityStats : ScriptableObject
    {
        public EnemyAbility abilityParent;
        public UpdateType updateType = UpdateType.Update;
        public AbilityType abilityType;
        public float cooldown = 1;
        public float timeBeforeHit;
        public float timeAbilityLastUsed;

        public bool canUseAbility { get { return Time.time - timeAbilityLastUsed >= cooldown; } }


        public AbilityStats(EnemyAbility abilityParent, AbilityType abilityType, UpdateType updateType, float cooldown, float timeBeforeHit)
        {
            this.abilityParent = abilityParent;
            this.abilityType = abilityType;
            this.updateType = updateType;
            this.cooldown = cooldown;
            this.timeBeforeHit = timeBeforeHit;
        }
    }
}

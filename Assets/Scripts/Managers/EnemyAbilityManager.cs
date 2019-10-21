using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAbilityManager
{
    AnimatedEnemy enemy;
    public List<EnemyAbility> abilities;
    //public bool abilityInUse;

    public EnemyAbilityManager(AnimatedEnemy _enemy)
    {
        enemy = _enemy;
        //abilities = new List<EnemyAbility>(); 
    }

    public void AddAbility(EnemyAbility ability) // Might be useless now
    {
        abilities.Add(ability);
    }
    public void Refresh()
    {
        
    }

    //Update each ability whose ability type happens during an Fixed-update phase
    public void PhysicsRefresh()
    {
        
    }
}

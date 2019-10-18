using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbilityManager
{
    Enemy enemy; 
    EnemyAbility[] abilities;
    public bool abilityInUse;

    public EnemyAbilityManager(Enemy _enemy)
    {
        enemy = _enemy;
        abilities = new EnemyAbility[3]; //max 3 abilities , Should be fix
    }

    public void AddAbility(EnemyAbility ability)
    {
        abilities[0] = ability; //ToChange
    }
    public void Refresh()
    {
        
    }

    //Update each ability whose ability type happens during an Fixed-update phase
    public void PhysicsRefresh(InputManager.InputPkg playerInputPkg)
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            if (abilities[i] != null && abilities[i].stats.updateType == UpdateType.FixedUpdate)
            {
                //UpdateAbility(abilities[i], playerInputPkg.abilityKeyPress[i]);
            }
        }
    }

    private void UpdateAbility(Ability ability, InputManager.InputPressType pressType)
    {
        switch (pressType)
        {
            case InputManager.InputPressType.None:
                break;
            case InputManager.InputPressType.FirstPress:
                ability.AbilityPressed();
                break;
            case InputManager.InputPressType.Held:
                ability.AbilityHeld();
                break;
            case InputManager.InputPressType.Release:
                ability.AbilityRelease();
                break;
            default:
                Debug.LogError("Unhandled switch: " + pressType); //Always catch an error
                break;
        }
        ability.AbilityUpdate();
    }
}

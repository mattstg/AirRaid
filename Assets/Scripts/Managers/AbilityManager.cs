using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager
{
    //Not a singleton manager, because there could be one ability manager per player
    PlayerController pc; //link to player
    public Ability[] abilities; //pair player ability key at index to abilities
    public PlayerSounds playerSounds;


    public AbilityManager(PlayerController _pc)
    {
        pc = _pc;
        playerSounds = pc.gameObject.GetComponent<PlayerSounds>();
        playerSounds.Initialize();
        abilities = new Ability[2];  //atm two hardcoded abilities, will refactor to make it scalable  (do as i say, not as i do)
    }

    //Update each ability whose ability type happens during an update phase
    public void Refresh(InputManager.InputPkg playerInputPkg)
    {
        for (int i = 0; i < abilities.Length; i++)
            if (abilities[i] != null && abilities[i].stats.updateType == UpdateType.Update)
            {
                UpdateAbility(abilities[i], playerInputPkg.abilityKeyPress[i]);
            }
        for (int i = 0; i < abilities.Length; i++)
            if (abilities[i] != null && !abilities[i].stats.canUseAbility)//Time.time -  abilities[i].stats.timeAbilityLastUsed <= abilities[i].stats.cooldown)
            {
                playerSounds.PlayShots(i);
            }
    }

    //Update each ability whose ability type happens during an Fixed-update phase
    public void PhysicsRefresh(InputManager.InputPkg playerInputPkg)
    {
        for (int i = 0; i < abilities.Length; i++)
            if (abilities[i] != null && abilities[i].stats.updateType == UpdateType.FixedUpdate)
            {
                UpdateAbility(abilities[i], playerInputPkg.abilityKeyPress[i]);
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


    //This is not a great way of doing this for now, but I need to test my ability framework (it is functional, just not scalable like we'll see later)
    public void AddAbilities(Ability abilityToAdd, int index)
    {
        abilities[index] = abilityToAdd;
        //If i were to make this permanent, I would have to have some try catches and safety nets around here.
    }
}

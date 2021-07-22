using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager
{
    //Not a singleton manager, because there could be one ability manager per player
    PlayerController pc; //link to player
    Dictionary<KeyCode, Ability> abilityMap = new Dictionary<KeyCode, Ability>(); //Maps keys to specific abilities
    

    public AbilityManager(PlayerController _pc)
    {
        pc = _pc;

        abilityMap.Add(KeyCode.H, new Ab_MachineGun(_pc)); //Not the best way of adding an ability, it's a little unstable since it's not coupled with the inputSystem (for key pressing purposes)
        abilityMap.Add(KeyCode.J, new Ab_BombDrop(_pc));   //There is now two hardcoded places with Input, better systems exist, but require something better like Rewired, Unity's new input system, or more complicated generics/axis commands  
    }

    //Update each ability whose ability type happens during an update phase
    public void Refresh(InputManager.InputPkg playerInputPkg)
    {
        foreach(KeyValuePair<KeyCode,Ability> kv in abilityMap)
        {
            if(kv.Value.stats.updateType == UpdateType.Update)
                UpdateAbility(kv.Value, playerInputPkg.GetInputPressTypeOfSpecificKey(kv.Key));
        }

    }

    //Update each ability whose ability type happens during an Fixed-update phase
    public void PhysicsRefresh(InputManager.InputPkg playerInputPkg)
    {
        foreach (KeyValuePair<KeyCode, Ability> kv in abilityMap)
        {
            if (kv.Value.stats.updateType == UpdateType.FixedUpdate)
                UpdateAbility(kv.Value, playerInputPkg.GetInputPressTypeOfSpecificKey(kv.Key));
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

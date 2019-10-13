using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class for all abilities, a virtual framework for abilities to inherit from
//WARNING! Make an ability based around normal update or fixed update, trying to build a hybrid one may have issues


public abstract class Ability
{
    protected PlayerController pc;
    public Abilities abilityType;                       //should be set by child type
    public UpdateType updateType = UpdateType.Update;   //should be set by child type -- AbilityManager will decide which frametype to call the methods based on this enum

    public Ability(PlayerController _pc) 
    {
        updateType = UpdateType.Update; 
        pc = _pc;
    }

    

    public virtual void AbilityPressed()
    {

    }


    public virtual void AbilityHeld()
    {

    }

    public virtual void AbilityRelease()
    {

    }

    
    //Update every frame, regardless of pressed or not
    public virtual void AbilityUpdate()
    {

    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class for all abilities, a virtual framework for abilities to inherit from
//WARNING! Make an ability based around normal update or fixed update, trying to build a hybrid one may have issues


public abstract class Ability
{
    protected PlayerController pc;
    public AbilityStats stats; //should be set by child type

    public Ability(PlayerController _pc) 
    {
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

    //returns if the ability is used, should be overwritten by child, check Ab_MachineGun
    public virtual bool UseAbility()
    {
        if (stats.canUseAbility)
        {
            stats.timeAbilityLastUsed = Time.time;
            pc.ModEnergy(-stats.energyCost);
            return true;
        }
        return false;
    }

    
    
    //Update every frame, regardless of pressed or not
    public virtual void AbilityUpdate()
    {

    }

    //This class should be set by the child
    public class AbilityStats
    {
        public Ability abilityParent;
        public Abilities abilityType;                           //
        public UpdateType updateType = UpdateType.Update;       //AbilityManager will decide which frametype to call the methods based on this enum

        public float cooldown = 1;                                     //amount of time between uses, should be "constant"
        public float timeAbilityLastUsed;
        public float energyCost = 1;
        public bool canUseAbility { get{ return Time.time - timeAbilityLastUsed >= cooldown && abilityParent.pc.stats.currentEnegy > energyCost; } }
        
        //Autogenerate constructors by highlighting the variables you want, right click, quick action and refactor, generate constructor
        public AbilityStats(Ability abilityParent, Abilities abilityType, UpdateType updateType, float cooldown, float energyCost)
        {
            this.abilityParent = abilityParent;
            this.abilityType = abilityType;
            this.updateType = updateType;
            this.cooldown = cooldown;
            this.energyCost = energyCost;
        }
    }
}

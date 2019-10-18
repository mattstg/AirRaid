using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderPck
{
    public TypeDefender type;
    public DefenderState state;
    public float speed;
    public float hp;
    public float maxHp;
    public float damage;
    public float energy;
    public float energyMax;
    public float energyRegen;
    public float attackRange;
    public float visionRange;
    public float delayForAbility;
    public float abilityEnergyCost;
    public Leader myLeader;
    public Transform target;

    public DefenderPck(TypeDefender _type, DefenderState currentState) {
        this.type = _type;
        this.state = currentState;
        FillInfoPck(type);
    }

    private void FillInfoPck(TypeDefender type) {
        switch (type) {
            case TypeDefender.MELEE:
                GetInfoMelee();
                break;
            case TypeDefender.RANGE:
                GetInfoRange();
                break;
            case TypeDefender.SUPPORT:
                GetInfoSupport();
                break;
            default:
                Debug.LogError("Missing info for this TypeDefender");
                break;
        }
    }

    //Make 1 function and just change the beginning of the variable using 2 string
    private void GetInfoMelee() {
        this.hp = GlobalDefendersVariables.Instance.ML_HP;
        this.maxHp = GlobalDefendersVariables.Instance.ML_MAX_HP;
        this.damage = GlobalDefendersVariables.Instance.ML_DAMAGE;
        this.energy = GlobalDefendersVariables.Instance.ML_ENERGY_START;
        this.energyMax = GlobalDefendersVariables.Instance.ML_ENERGY_MAX;
        this.energyRegen = GlobalDefendersVariables.Instance.ML_ENERGY_REGEN;
        this.attackRange = GlobalDefendersVariables.Instance.ML_ATTACK_RANGE;
        this.visionRange = GlobalDefendersVariables.Instance.ML_VISION_RANGE;
        this.delayForAbility = GlobalDefendersVariables.Instance.ML_DELAY_ABILITY;
        this.abilityEnergyCost = GlobalDefendersVariables.Instance.ML_ABILITY_COST;
    }
    private void GetInfoRange() {
        this.hp = GlobalDefendersVariables.Instance.RA_HP;
        this.maxHp = GlobalDefendersVariables.Instance.RA_MAX_HP;
        this.damage = GlobalDefendersVariables.Instance.RA_DAMAGE;
        this.energy = GlobalDefendersVariables.Instance.RA_ENERGY_START;
        this.energyMax = GlobalDefendersVariables.Instance.RA_ENERGY_MAX;
        this.energyRegen = GlobalDefendersVariables.Instance.RA_ENERGY_REGEN;
        this.attackRange = GlobalDefendersVariables.Instance.RA_ATTACK_RANGE;
        this.visionRange = GlobalDefendersVariables.Instance.RA_VISION_RANGE;
        this.delayForAbility = GlobalDefendersVariables.Instance.RA_DELAY_ABILITY;
        this.abilityEnergyCost = GlobalDefendersVariables.Instance.RA_ABILITY_COST;
    }
    private void GetInfoSupport() {
        this.hp = GlobalDefendersVariables.Instance.SP_HP;
        this.maxHp = GlobalDefendersVariables.Instance.SP_MAX_HP;
        this.damage = GlobalDefendersVariables.Instance.SP_DAMAGE;
        this.energy = GlobalDefendersVariables.Instance.SP_ENERGY_START;
        this.energyMax = GlobalDefendersVariables.Instance.SP_ENERGY_MAX;
        this.energyRegen = GlobalDefendersVariables.Instance.SP_ENERGY_REGEN;
        this.attackRange = GlobalDefendersVariables.Instance.SP_ATTACK_RANGE;
        this.visionRange = GlobalDefendersVariables.Instance.SP_VISION_RANGE;
        this.delayForAbility = GlobalDefendersVariables.Instance.SP_DELAY_ABILITY;
        this.abilityEnergyCost = GlobalDefendersVariables.Instance.SP_ABILITY_COST;
    }
}

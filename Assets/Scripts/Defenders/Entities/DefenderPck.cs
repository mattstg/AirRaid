using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderPck
{
    public TypeDefender type;
    public DefenderState state;
    public float hp;
    public float energy;
    public float energyMax;
    public float attackRange;
    public float visionRange;
    public float delayForAbility;
    public Transform target;

    private GameObject prefab;

    public DefenderPck(TypeDefender _type, DefenderState currentState) {
        this.type = _type;
        this.state = currentState;
        FillInfoPck(type);
        GetPrefab();
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
    private void GetPrefab() {
        switch (this.type) {
            case TypeDefender.MELEE:
                this.prefab = Resources.Load<GameObject>("Prefabs/Defenders/Melee");
                break;
            case TypeDefender.RANGE:
                this.prefab = Resources.Load<GameObject>("Prefabs/Defenders/Range");
                break;
            case TypeDefender.SUPPORT:
                this.prefab = Resources.Load<GameObject>("Prefabs/Defenders/Support");
                break;
            default:
                Debug.LogError("Missing prefab for this TypeDefender");
                break;
        }
    }


    //Make 1 function and just change the beginning of the variable using 2 string
    private void GetInfoMelee() {
        this.hp = GlobalDefendersVariables.Instance.ML_HP;
        this.energy = GlobalDefendersVariables.Instance.ML_ENERGY_START;
        this.energyMax = GlobalDefendersVariables.Instance.ML_ENERGY_MAX;
        this.attackRange = GlobalDefendersVariables.Instance.ML_ATTACK_RANGE;
        this.visionRange = GlobalDefendersVariables.Instance.ML_VISION_RANGE;
        this.delayForAbility = GlobalDefendersVariables.Instance.ML_DELAY_ABILITY;
    }
    private void GetInfoRange() {
        this.hp = GlobalDefendersVariables.Instance.RA_HP;
        this.energy = GlobalDefendersVariables.Instance.RA_ENERGY_START;
        this.energyMax = GlobalDefendersVariables.Instance.RA_ENERGY_MAX;
        this.attackRange = GlobalDefendersVariables.Instance.RA_ATTACK_RANGE;
        this.visionRange = GlobalDefendersVariables.Instance.RA_VISION_RANGE;
        this.delayForAbility = GlobalDefendersVariables.Instance.RA_DELAY_ABILITY;
    }
    private void GetInfoSupport() {
        this.hp = GlobalDefendersVariables.Instance.SP_HP;
        this.energy = GlobalDefendersVariables.Instance.SP_ENERGY_START;
        this.energyMax = GlobalDefendersVariables.Instance.SP_ENERGY_MAX;
        this.attackRange = GlobalDefendersVariables.Instance.SP_ATTACK_RANGE;
        this.visionRange = GlobalDefendersVariables.Instance.SP_VISION_RANGE;
        this.delayForAbility = GlobalDefendersVariables.Instance.SP_DELAY_ABILITY;
    }
}

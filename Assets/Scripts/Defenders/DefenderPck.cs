using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderPck
{
    TypeDefender type;
    private float hp;
    private float energy;
    private float energyMax;
    private float range;
    private float delayForAbility;

    private GameObject prefab;

    public DefenderPck(TypeDefender _type) {
        this.type = _type;
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
                this.prefab = Resources.Load<GameObject>("");
                break;
            case TypeDefender.RANGE:
                this.prefab = Resources.Load<GameObject>("");
                break;
            case TypeDefender.SUPPORT:
                this.prefab = Resources.Load<GameObject>("");
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
        this.range = GlobalDefendersVariables.Instance.ML_RANGE;
        this.delayForAbility = GlobalDefendersVariables.Instance.ML_DELAY_ABILITY;
    }
    private void GetInfoRange() {
        this.hp = GlobalDefendersVariables.Instance.RA_HP;
        this.energy = GlobalDefendersVariables.Instance.RA_ENERGY_START;
        this.energyMax = GlobalDefendersVariables.Instance.RA_ENERGY_MAX;
        this.range = GlobalDefendersVariables.Instance.RA_RANGE;
        this.delayForAbility = GlobalDefendersVariables.Instance.RA_DELAY_ABILITY;
    }
    private void GetInfoSupport() {
        this.hp = GlobalDefendersVariables.Instance.SP_HP;
        this.energy = GlobalDefendersVariables.Instance.SP_ENERGY_START;
        this.energyMax = GlobalDefendersVariables.Instance.SP_ENERGY_MAX;
        this.range = GlobalDefendersVariables.Instance.SP_RANGE;
        this.delayForAbility = GlobalDefendersVariables.Instance.SP_DELAY_ABILITY;
    }
}

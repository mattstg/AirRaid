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

    public DefenderPck(TypeDefender _type, float _hp, float energyStartingWith, float energyMax, float range, float delayForAbility) {
        this.type = _type;
        this.hp = _hp;
        this.energy = energyStartingWith;
        this.energyMax = energyMax;
        this.range = range;
        this.delayForAbility = delayForAbility;
        GetPrefab();
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
}

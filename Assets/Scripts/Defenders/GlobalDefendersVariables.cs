using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDefendersVariables {

    #region Singleton
    private static GlobalDefendersVariables instance;
    private GlobalDefendersVariables() { }
    public static GlobalDefendersVariables Instance { get { return instance ?? (instance = new GlobalDefendersVariables()); } }
    #endregion

    //Melee
    public readonly float ML_HP = 200f;
    public readonly float ML_ENERGY_START = 150f;
    public readonly float ML_ENERGY_MAX = 150f;
    public readonly float ML_ATTACK_RANGE = 0.5f;
    public readonly float ML_VISION_RANGE = 10f;
    public readonly float ML_DELAY_ABILITY = 0.7f;
    public readonly float ML_ABILITY_COST = 20f;

    //Range
    public readonly float RA_HP = 100f;
    public readonly float RA_ENERGY_START = 120f;
    public readonly float RA_ENERGY_MAX = 120f;
    public readonly float RA_ATTACK_RANGE = 7f;
    public readonly float RA_VISION_RANGE = 17f;
    public readonly float RA_DELAY_ABILITY = 1f;

    //Support
    public readonly float SP_HP = 80f;
    public readonly float SP_ENERGY_START = 250f;
    public readonly float SP_ENERGY_MAX = 250f;
    public readonly float SP_ATTACK_RANGE = 10f;
    public readonly float SP_VISION_RANGE = 20f;
    public readonly float SP_DELAY_ABILITY = 3f;

    //Spawner Variables
    public readonly int MAX_AMOUNT_DEFENDERS = 200;
    public int amountDefenderOnMap = 0;
    public readonly float DELAY_SPAWN_DEFENDER = 5f;
}

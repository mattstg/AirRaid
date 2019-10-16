using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDefendersVariables {

    #region Singleton
    private static GlobalDefendersVariables instance;
    private GlobalDefendersVariables() { }
    public static GlobalDefendersVariables Instance { get { return instance ?? (instance = new GlobalDefendersVariables()); } }
    #endregion

    [Header("Melee")]
    [SerializeField] public const float ML_HP = 200f;
    [SerializeField] public const float ML_ENERGY_START = 150f;
    [SerializeField] public const float ML_ENERGY_MAX = 150f;
    [SerializeField] public const float ML_RANGE = 0.5f;
    [SerializeField] public const float ML_DELAY_ABILITY = 0.7f;


    [Header("Range")]
    [SerializeField] public const float RA_HP = 100f;
    [SerializeField] public const float RA_ENERGY_START = 120f;
    [SerializeField] public const float RA_ENERGY_MAX = 120f;
    [SerializeField] public const float RA_RANGE = 7f;
    [SerializeField] public const float RA_DELAY_ABILITY = 1f;


    [Header("Support")]
    [SerializeField] public const float SP_HP = 80f;
    [SerializeField] public const float SP_ENERGY_START = 250f;
    [SerializeField] public const float SP_ENERGY_MAX = 250f;
    [SerializeField] public const float SP_RANGE = 10f;
    [SerializeField] public const float SP_DELAY_ABILITY = 3f;
}

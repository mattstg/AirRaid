using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StatsScreen : MonoBehaviour
{
    PlayerManager pm;

    [Header("Energy")]
    public Text maxEnergy;
    public Text currentEnegy;
    public Text energyRegenPerSec;
    public Text energyPerThrustSecond;
    [Header("Player Movement")]
    public Text maxSpeed;
    public Text minSpeed;
    public Text acceleration;
    [Header("Stats")]
    public Text hp;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayFinalStats(float _maxenergy,float _currentenergy,float _energyregenpersec,float _energyperthrustsec,float _maxspeed,float _minspeed,float _acceleration,float _hp)
    {
        maxEnergy.text = "" + _maxenergy;
        currentEnegy.text = ""  + _currentenergy;
        energyRegenPerSec.text = "" + _energyregenpersec;
        energyPerThrustSecond.text = "" + _energyperthrustsec;
        maxSpeed.text = "" + _maxspeed;
        minSpeed.text = "" + _minspeed;
        acceleration.text = "" + _acceleration;
        maxEnergy.text = "" + _maxenergy;
        hp.text = "" + _hp;

    }
}

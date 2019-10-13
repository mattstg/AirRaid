using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Abilities { Turrets, Rocket }
public class PlayerController : MonoBehaviour
{
    public static readonly int ABILITY_COUNT_MAX = 6; //max number of abilites, to change this number, you would have to add more Axis in Editor->InputManager and UI ability parent grid column count
    [HideInInspector] public bool isAlive;

    [HideInInspector] public PlayerStats stats;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Camera playerCam;

    public Transform[] rocketSpots;     //Linked in editor
    public Transform[] gunTurretSpots;  //Linked in editor

    public void Initialize()
    {
        //Create stats, add two starter abilities
        stats = new PlayerStats(this);
        stats.abilities.Add(Abilities.Turrets);
        stats.abilities.Add(Abilities.Rocket);

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; //using custom gravity
        rb.velocity = transform.forward * stats.maxSpeed * .7f;

        playerCam = GetComponentInChildren<Camera>();
        isAlive = true;
    }

    public void PostInitialize()
    {
        
    }

    public void Refresh(InputManager.InputPkg inputPkg)
    {
        stats.currentEnegy = Mathf.Clamp(stats.currentEnegy + stats.energyRegenPerSec * Time.deltaTime,0,stats.maxEnergy);
        //Debug.Log("Energy: " + stats.currentEnegy);
    }

    public void PhysicsRefresh(InputManager.InputPkg inputPkg)
    {
        Throttle(inputPkg.throttleAmount);                                                  //increase or decrease speed based on holding down the throttle amount (-1 to 1)
        rb.AddForce(-Vector3.up * Mathf.Lerp(0, 9.81f, Mathf.Clamp01( 1 - ((stats.relativeLocalVelo .z* 2)/stats.maxSpeed)))); //add the force of custom gravity, relative to our speed (faster speed @ 50%, less gravity due to "air-lift")
        //This could be done way better, using dot product to determine the speed relative to my facing direction/perpendicular to the ground
        
        rb.angularVelocity = transform.TransformDirection( new Vector3(stats.pitchSpeed * inputPkg.dirPressed.y, 0, stats.rollSpeed * inputPkg.dirPressed.x));
    }

    private void Throttle(float deltaThrottle)  //-1 to 1
    {

        rb.AddRelativeForce(Vector3.forward * deltaThrottle * stats.acceleration);

        //https://answers.unity.com/questions/193398/velocity-relative-to-local.html
        Vector3 relativeLocalVelocity = stats.relativeLocalVelo;
        //Debug.Log("foward velo: " + relativeLocalVelocity);
        if(relativeLocalVelocity.z > stats.maxSpeed)  //if our foward is greater than max speed
        {
            relativeLocalVelocity.z = stats.maxSpeed;
            rb.velocity = transform.TransformDirection(relativeLocalVelocity);
        }
        //consume energy based on speed
       // Debug.Log("RelativeLocalVeloZ: " + relativeLocalVelocity.z + ", stats.MaxSpeed: " + stats.maxSpeed + " stats.energySpeedCostThreshold: " + stats.energySpeedCostThreshold);

        if (relativeLocalVelocity.z > stats.energySpeedCostThreshold)
        {
            float speedAboveThreshold = relativeLocalVelocity.z - stats.energySpeedCostThreshold;
            float energyDeduct = (speedAboveThreshold) * stats.energyPerUnitSpeedAboveThreshold * Time.fixedDeltaTime;
            
            stats.currentEnegy = Mathf.Clamp(stats.currentEnegy - energyDeduct, 0, stats.maxEnergy);

            //Debug.Log("stats.currentEnegy: " + stats.currentEnegy + ", speedAboveThreshold: " + speedAboveThreshold + " energyDeduct: " + energyDeduct);
            //CurrentEnergy = CurrentEnergy - (Amount of speed above threshold)*(energy cost per speed above threshold)  clamped between 0 and maxEnergy.
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //At this moment, any physical collision should destroy the ship
        ShipDestroyed();
    }

    private void ShipDestroyed()
    {
        PlayerManager.Instance.PlayerDied();
        isAlive = false;
    }

    //I put the players data in a seperate data class so it's easy to pass to the UI, and for being able to save the game
    public class PlayerStats
    {
        public PlayerController player;
        [Header("Energy")]
        public float speedPerctangeThresholdToCostEnergy = .8f;  //above 85% max speed, energy begins to be consumed
        public float energyPerUnitSpeedAboveThreshold = 1.5f; //X energy per unit speed above threshold costs
        public float energySpeedCostThreshold { get { return maxSpeed * speedPerctangeThresholdToCostEnergy; } } //threshold where it starts costing energy for velocity
        public float maxEnergy = 100;
        public float currentEnegy = 100;
        public float energyRegenPerSec = 10;
        [Header("Player Movement")]
        public float maxSpeed = 55;
        public float acceleration = 30;
        public float pitchSpeed = .6f;
        public float rollSpeed = 3;
        [Header("Stats")]
        public float hp;
        public float maxHp = 100;

        public Vector3 relativeLocalVelo { get { return player.transform.InverseTransformDirection(player.rb.velocity); ; } }  //returns velo in local co-rd, since rb.velo is world
        public List<Abilities> abilities = new List<Abilities>();

        public PlayerStats(PlayerController pc) { player = pc; currentEnegy = maxEnergy; hp = maxHp; }
    }
}

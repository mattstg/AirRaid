using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum Abilities { Turrets, Rocket, EnergyBoost, NitroBoost }
public enum BodyPart { BodyPart_Turret, BodyPart_WingSlots, BodyPart_BombBay, BodyPart_FrontCannon }  //These enums tags must EXCATLY match the tag names
public class PlayerController : MonoBehaviour, IHittable
{
    public static readonly int ABILITY_COUNT_MAX = 6; //max number of abilites, to change this number, you would have to add more Axis in Editor->InputManager and UI ability parent grid column count
    [HideInInspector] public bool isAlive;

    [HideInInspector] public PlayerStats stats;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Camera playerCam;
    [HideInInspector]
    public AbilityManager abilityManager;

    public Dictionary<BodyPart, List<Vector3>> bodyParts; //This uses transforms on the player that uses the correct BodyPart_ tag, those parts are deleted after consumed, and thier localPosition is saved

    public void Initialize()
    {
        //Create stats, add two starter abilities
        abilityManager = new AbilityManager(this);
        abilityManager.AddAbilities(new Ab_MachineGun(this), 0); //Not the best way of adding an ability, it's a little unstable since it's not coupled with the inputSystem (for key pressing purposes)
        //but it's important that I test now that my ability system is all in place.

        abilityManager.AddAbilities(new Ab_EnergyRegen(this), 2);
        abilityManager.AddAbilities(new Ab_NitroBoost(this), 3);
        abilityManager.AddAbilities(new Ab_Rocket(this), 1);
        stats = new PlayerStats(this);
        stats.abilities.Add(Abilities.Turrets);
        stats.abilities.Add(Abilities.Rocket);
        stats.abilities.Add(Abilities.EnergyBoost);
        stats.abilities.Add(Abilities.NitroBoost);

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; //using custom gravity
        rb.velocity = transform.forward * stats.maxSpeed * .7f;

        playerCam = GetComponentInChildren<Camera>();

        //Link all body parts
        bodyParts = new Dictionary<BodyPart, List<Vector3>>();

        Transform[] allChildren = transform.GetComponentsInChildren<Transform>();
        HashSet<string> bodyPartEnums = new HashSet<string>(System.Enum.GetNames(typeof(BodyPart))); //The code to get a hashset of enums represented as strings
        for(int i = allChildren.Length - 1; i >= 0; i--)  //since i will be deleting elements from this array, need to backwards for loop
        { //grab each object who has the correct body part tag
            if(bodyPartEnums.Contains(allChildren[i].tag))  //if the child tag matches one of the bodypart tags
            {
                BodyPart bodyPart = (BodyPart)System.Enum.Parse(typeof(BodyPart), allChildren[i].tag);  //Syntax to change the body part from a string to an enum
                //So we found a body part, add it to the dictionary
                if(!bodyParts.ContainsKey(bodyPart))
                {  //very first entry, need to add it to the dictionary and create the list
                    bodyParts.Add(bodyPart, new List<Vector3>());
                }
                bodyParts[bodyPart].Add(allChildren[i].transform.localPosition);
                GameObject.Destroy(allChildren[i].gameObject); //Dont need the transform anymore, just wanted it's local position
            }
        }
        isAlive = true;
    }

    public void PostInitialize()
    {
        
    }

    public void Refresh(InputManager.InputPkg inputPkg)
    {
        abilityManager.Refresh(inputPkg);
        stats.currentEnegy = Mathf.Clamp(stats.currentEnegy + stats.energyRegenPerSec * Time.deltaTime,0,stats.maxEnergy);
        if (stats.hp <= 0)
            ShipDestroyed();
        //Debug.Log("Energy: " + stats.currentEnegy);
    }

    public void PhysicsRefresh(InputManager.InputPkg inputPkg)
    {
        abilityManager.PhysicsRefresh(inputPkg);
        Throttle(inputPkg.throttleAmount);//increase or decrease speed based on holding down the throttle amount (-1 to 1)
        rb.AddForce(-Vector3.up * Mathf.Lerp(0, 9.81f, Mathf.Clamp01( 1 - ((stats.relativeLocalVelo .z)/stats.forwardSpeedAtWhichGravityIsCanceled)))); //add the force of custom gravity, relative to our speed (faster speed @ 50%, less gravity due to "air-lift")
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
            ModEnergy(-energyDeduct);

            //Debug.Log("stats.currentEnegy: " + stats.currentEnegy + ", speedAboveThreshold: " + speedAboveThreshold + " energyDeduct: " + energyDeduct);
            //CurrentEnergy = CurrentEnergy - (Amount of speed above threshold)*(energy cost per speed above threshold)  clamped between 0 and maxEnergy.
        }
        
    }

    public bool ActiveStorePanel(InputManager.InputPressType ipt)
    {
        if(ipt== InputManager.InputPressType.FirstPress)
        {
            return true;
        }
        return false;
    }

    public void ModEnergy(float modBy)
    {
        stats.currentEnegy = Mathf.Clamp(stats.currentEnegy + modBy, 0, stats.maxEnergy);
        //Eventaully this would be the spot to cause the engine stall
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

    public void HitByProjectile(float damage)
    {
        stats.hp -= damage;
        Debug.Log("took dmg: " + damage);
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
        public float maxSpeed = 15;
        public float acceleration = 10;
        public float pitchSpeed = .6f;
        public float rollSpeed = 3;
        public readonly float forwardSpeedAtWhichGravityIsCanceled = 10;
        [Header("Stats")]
        public float hp;
        public float maxHp = 100;

        public Vector3 relativeLocalVelo { get { return player.transform.InverseTransformDirection(player.rb.velocity); ; } }  //returns velo in local co-rd, since rb.velo is world
        public List<Abilities> abilities = new List<Abilities>();

        public PlayerStats(PlayerController pc) { player = pc; currentEnegy = maxEnergy; hp = maxHp; }
    }
}

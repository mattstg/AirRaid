using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Abilities { Turrets, Rocket }
public class PlayerController : MonoBehaviour
{
    public static readonly int ABILITY_COUNT_MAX = 6; //max number of abilites, to change this number, you would have to add more Axis in Editor->InputManager and UI ability parent grid column count
    
    public Stats stats;
    [HideInInspector] public Rigidbody rb;

    public Transform[] rocketSpots;     //Linked in editor
    public Transform[] gunTurretSpots;  //Linked in editor

    public void Initialize()
    {
        //Create stats, add two starter abilities
        stats = new Stats(this);
        stats.abilities.Add(Abilities.Turrets);
        stats.abilities.Add(Abilities.Rocket);

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; //using custom gravity
        rb.velocity = transform.forward * stats.maxSpeed * .7f;
    }

    public void PostInitialize()
    {
    }

    public void Refresh(InputManager.InputPkg inputPkg)
    {

    }

    public void PhysicsRefresh(InputManager.InputPkg inputPkg)
    {
        Throttle(inputPkg.throttleAmount);                                                  //increase or decrease speed based on holding down the throttle amount (-1 to 1)
        rb.AddForce(-Vector3.up * Mathf.Lerp(0, 9.81f, 1 - (stats.currentSpeed/stats.maxSpeed))); //add the force of custom gravity, relative to our speed (faster speed, less gravity due to "air-lift")
        
    }

    private void Throttle(float deltaThrottle)  //-1 to 1
    {

        rb.AddRelativeForce(Vector3.forward * deltaThrottle * stats.acceleration);
        
        //stats.currentSpeed = Mathf.Clamp(.currentSpeed, 0, stats.maxSpeed);
        
    }

    //I put the players data in a seperate data class so it's easy to pass to the UI, and for being able to save the game
    [System.Serializable] //Allows it to be visible in the editor
    public class Stats
    {
        public PlayerController player;
        public float maxSpeed = 45;
        public float acceleration = 10;
        public float currentSpeed { get { return player.rb.velocity.magnitude; } }
        public List<Abilities> abilities = new List<Abilities>();

        public Stats(PlayerController pc) { player = pc; }
    }
}

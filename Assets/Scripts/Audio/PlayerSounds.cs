using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioClip sound,audio,throttle;
    public AudioSource shootSounds,playerNoise;
    public PlayerController player;
    public void Initialize()
    {
        audio = Resources.Load<AudioClip>("Music/Bomb");
        throttle = audio;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        var aSources = player.gameObject.GetComponents<AudioSource>();
        shootSounds = aSources[0];
        playerNoise = aSources[1];
        
        playerNoise.clip = throttle;
        playerNoise.loop = true;
        playerNoise.Play();
    }

    public void PhysicsRefresh()
    {
    }

    public void PostInitialize()
    {
    }

    public void Refresh()
    {
        DoThrottleSounds();
    }

    public void PlayDamage()
    {
        shootSounds.PlayOneShot(audio);
    }

    public void DoThrottleSounds()
    {
        playerNoise.pitch = InputManager.Instance.refreshInputPkg.throttleAmount+1f;
    }

    
}

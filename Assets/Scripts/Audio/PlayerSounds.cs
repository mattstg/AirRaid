using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioClip sound,playerDamage,throttle;
    public AudioClip[] abilitySounds;
    public AudioSource shootSounds,playerNoise;
    public PlayerController player;
    public void Initialize()
    {
        playerDamage = Resources.Load<AudioClip>("Music/PlayerDamage");  
        throttle = Resources.Load<AudioClip>("Music/Throttle");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        var aSources = player.gameObject.GetComponents<AudioSource>();
        shootSounds = aSources[0];
        playerNoise = aSources[1];
        
        playerNoise.clip = throttle;
        playerNoise.loop = true;
        //playerNoise.Play();

        abilitySounds[0] = Resources.Load<AudioClip>("Music/Gunshot");
        abilitySounds[1] = Resources.Load<AudioClip>("Music/Bomb"); 
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
        shootSounds.PlayOneShot(playerDamage);
    }

    public void DoThrottleSounds()
    {
        playerNoise.pitch = InputManager.Instance.refreshInputPkg.throttleAmount+1f;
    }

    public void PlayShots(int ability)
    {
        shootSounds.PlayOneShot(abilitySounds[ability]);
    }

    
}

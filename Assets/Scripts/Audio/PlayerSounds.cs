using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioClip sound,audio,throttle;
    public AudioClip[] abilitySounds;
    public AudioSource shootSounds,playerNoise;
    public PlayerController player;
    public void Initialize()
    {
        audio = Resources.Load<AudioClip>("Music/Bomb"); //Resources.Load<AudioClip>("Music/Damage");  
        throttle = audio;//Resources.Load<AudioClip>("Music/Throttle");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        var aSources = player.gameObject.GetComponents<AudioSource>();
        shootSounds = aSources[0];
        playerNoise = aSources[1];
        
        playerNoise.clip = throttle;
        playerNoise.loop = true;
        //playerNoise.Play();

        abilitySounds[0] = throttle; //Resources.Load<AudioClip>("Music/Gun");
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
        shootSounds.PlayOneShot(audio);
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

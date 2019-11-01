using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioClip sound,audio;
    public AudioSource shootSounds,playerNoise;
    public PlayerController player;
    public void Initialize()
    {
        audio = Resources.Load<AudioClip>("Music/Bomb");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        shootSounds = player.gameObject.GetComponent<AudioSource>();
        //shootSounds.PlayOneShot(audio);
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
        shootSounds.pitch = InputManager.Instance.refreshInputPkg.throttleAmount+1f;
    }

    
}

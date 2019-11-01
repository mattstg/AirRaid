using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : IManagable
{
    #region Singleton
    private static AudioManager instance = null;

    private AudioManager()
    {
    }

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AudioManager();
            }
            return instance;
        }
    }

    #endregion

    public AudioSource shootSounds,flakSounds;
    public AudioClip audio;
    public PlayerSounds player;

    public void Initialize()
    {

        

        player.Initialize();
    }

    public void PhysicsRefresh()
    {
        player.PhysicsRefresh();
    }

    public void PostInitialize()
    {
        player.PostInitialize();
    }

    public void Refresh()
    {
        player.Refresh();
    }

    
    }



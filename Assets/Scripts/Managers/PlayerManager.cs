using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : IManagable
{
    #region Singleton
    private static PlayerManager instance;
    private PlayerManager() { }
    public static PlayerManager Instance { get { return instance ?? (instance = new PlayerManager()); } }
    #endregion
    public PlayerController player;
    public void Initialize()
    {
        GameObject newPlayer = GameObject.Instantiate(Resources.Load<GameObject>(PrefabFileDir.PLAYER_RESOURCE_PATH), GameLinks.gl.playerSpawn);
        player = newPlayer.GetComponent<PlayerController>();
        player.transform.position = GameLinks.gl.playerSpawn.position;
        //player.transform.localEulerAngles = 
        player.Initialize();
    }

    public void PhysicsRefresh()
    {
        player.PhysicsRefresh(InputManager.Instance.physicsRefreshInputPkg);
    }

    public void Refresh()
    {
        player.Refresh(InputManager.Instance.refreshInputPkg);
    }

    public void PostInitialize()
    {
        player.PostInitialize();
    }

   
}

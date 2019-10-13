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
        player.Initialize();  //isAlive = true
    }

    public void PostInitialize()
    {
        player.PostInitialize();
    }

    public void PhysicsRefresh()
    {
        if(player.isAlive)
            player.PhysicsRefresh(InputManager.Instance.physicsRefreshInputPkg);
    }

    public void Refresh()
    {
        if (player.isAlive)
            player.Refresh(InputManager.Instance.refreshInputPkg);
    }
    

    public void PlayerDied()
    {
        Debug.Log("Player has lost the game");
        player.stats.hp = 0; //in case player died from crashing
        player.playerCam.gameObject.SetActive(false);
        GameLinks.gl.postDeathCam.gameObject.SetActive(true);
        player.gameObject.SetActive(false);
        player.isAlive = false;
    }
}

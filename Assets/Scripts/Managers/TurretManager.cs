using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager
{
    GameObject go;
    GameObject turretParent;
    public List<Turret> turrets;
    Turret turret;
    
    #region
    private static TurretManager instance;
    private TurretManager() { }
    public static TurretManager Instance { get { return instance ?? (instance = new TurretManager()); } }
    #endregion
    public void Initialize() {

        go = Resources.Load<GameObject>("Prefabs/Turret");
        turrets = new List<Turret>();
        turretParent = new GameObject("turretParent");

    }
   
    public void PostInitialize() {

    }
   
    public void PhysicsRefresh() {

        for (int i = turrets.Count - 1; i >= 0; i--)
            turrets[i]?.PhysicsRefresh();


    }
    public void Refresh()
    {
        for (int i = turrets.Count - 1; i >= 0; i--)
            turrets[i]?.Refresh();
    }
    public Turret GetRandomTurret()
    {
        return turrets.GetRandomElement<Turret>();
    }
    public Turret SpawnTurret(Vector3 spawnLoc, float lifeSpan)
    {

       
        turret = GameObject.Instantiate(go, PlayerManager.Instance.player.transform.position, Quaternion.identity, turretParent.transform).GetComponent<Turret>();
        turrets.Add(turret);
        turret.timeOfExpire = lifeSpan;

        return turret;
    }

}

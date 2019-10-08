using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : IManagable
{
    #region Singleton
    private static InputManager instance;
    private InputManager() { }
    public static InputManager Instance { get { return instance ?? (instance = new InputManager()); } }
    #endregion
    public static bool invertedYAxis = true;
    public InputPkg refreshInputPkg = new InputPkg();
    public InputPkg physicsRefreshInputPkg = new InputPkg();

    

    public void Initialize()
    {
        refreshInputPkg.abilityUsed = new bool[PlayerController.ABILITY_COUNT_MAX];
        physicsRefreshInputPkg.abilityUsed = new bool[PlayerController.ABILITY_COUNT_MAX];
    }

    public void PhysicsRefresh()
    {
        SetInputPkg(physicsRefreshInputPkg);
    }

    private void SetInputPkg(InputPkg ip)
    {
        ip.throttleAmount = Input.GetAxis("Throttle");
        ip.dirPressed = new Vector2(Input.GetAxis("Horizontal") * (invertedYAxis?-1:1), Input.GetAxis("Vertical"));
        for(int i = 0; i < PlayerController.ABILITY_COUNT_MAX;i++)
        {
            ip.abilityUsed[i] = Input.GetButton("Ability" + i);
        }
    }

    public void Refresh()
    {
        SetInputPkg(refreshInputPkg);
    }

    public void PostInitialize()
    {
        
    }


    //InputPkg that player will give to UIManager so it can draw the UI
    public class InputPkg
    {
        public Vector2 dirPressed;
        public float throttleAmount;
        public bool[] abilityUsed;

        public override string ToString()
        {
            return $"DirPressed: {dirPressed}, throttleAmount: {throttleAmount}, dirPressed {dirPressed}, " + abilityUsed.CollectionToStringArray();
        }
    }
}

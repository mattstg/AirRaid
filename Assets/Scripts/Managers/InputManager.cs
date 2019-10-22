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
    public enum InputPressType { None, FirstPress, Held, Release }

    public static bool invertedYAxis = true;
    public InputPkg refreshInputPkg = new InputPkg();
    public InputPkg physicsRefreshInputPkg = new InputPkg();

    public void Initialize()
    {
        refreshInputPkg.abilityKeyPress = new InputPressType[PlayerController.ABILITY_COUNT_MAX];
        physicsRefreshInputPkg.abilityKeyPress = new InputPressType[PlayerController.ABILITY_COUNT_MAX];
    }

    public void PhysicsRefresh()
    {
        SetInputPkg(physicsRefreshInputPkg);
    }

    private void SetInputPkg(InputPkg ip)
    {
        ip.throttleAmount = Input.GetAxis("Throttle");
        ip.dirPressed = new Vector2(Input.GetAxis("Horizontal") * (invertedYAxis ? -1 : 1), Input.GetAxis("Vertical"));
        for (int i = 0; i < PlayerController.ABILITY_COUNT_MAX; i++)
        {
            ip.abilityKeyPress[i] = GetInputPressType("Ability" + i);
        }
    }

    public void Refresh()
    {
        SetInputPkg(refreshInputPkg);
        if (Input.GetKeyDown(KeyCode.Z)) {
            StoreManager.Instance.openStore = !StoreManager.Instance.openStore;
            if (StoreManager.Instance.openStore) {
                StoreManager.Instance.storeActive = true;
            } else {
                StoreManager.Instance.storeActive = false;
            }
        }
    }

    public void PostInitialize()
    {

    }

    //Get the current state of a requested key
    public InputPressType GetInputPressType(string axisName)
    {
        if (Input.GetButtonDown(axisName))
            return InputPressType.FirstPress;
        if (Input.GetButton(axisName))
            return InputPressType.Held;
        if (Input.GetButtonUp(axisName))
            return InputPressType.Release;

        return InputPressType.None;
    }


    //InputPkg that player will give to UIManager so it can draw the UI
    public class InputPkg
    {
        public Vector2 dirPressed;
        public float throttleAmount;
        public InputPressType[] abilityKeyPress;

        public override string ToString()
        {
            return $"DirPressed: {dirPressed}, throttleAmount: {throttleAmount}, dirPressed {dirPressed}, " + abilityKeyPress.CollectionToStringArray();
        }
    }

    
}
 
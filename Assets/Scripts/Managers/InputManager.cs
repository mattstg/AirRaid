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
        
    }

    public void PostInitialize()
    {

    }


    public void PhysicsRefresh()
    {
        SetInputPkg(physicsRefreshInputPkg);
    }

    private void SetInputPkg(InputPkg ip)
    {
        ip.throttleAmount = Input.GetAxis("Throttle");
        ip.yawPressed = Input.GetAxis("Yaw");
        ip.dirPressed = new Vector2(Input.GetAxis("Horizontal") * (invertedYAxis ? -1 : 1), Input.GetAxis("Vertical"));
        ip.H_Key = GetInputPressType(KeyCode.H);
        ip.J_Key = GetInputPressType(KeyCode.J);
        ip.K_Key = GetInputPressType(KeyCode.K);
    }

    public void Refresh()
    {
        SetInputPkg(refreshInputPkg);
    }

    

    //Get the current state of a requested key
    public InputPressType GetInputPressType(KeyCode keyToCheck)
    {
        if (Input.GetKeyDown(keyToCheck))
            return InputPressType.FirstPress;
        if (Input.GetKey(keyToCheck))
            return InputPressType.Held;
        if (Input.GetKeyUp(keyToCheck))
            return InputPressType.Release;

        return InputPressType.None;
    }


    //InputPkg that player will give to UIManager so it can draw the UI
    public class InputPkg
    {
        public Vector2 dirPressed;
        public float throttleAmount;
        public float yawPressed;
        public InputPressType H_Key;
        public InputPressType J_Key;
        public InputPressType K_Key;

        public InputPressType GetInputPressTypeOfSpecificKey(KeyCode keyToCheck)
        {
            switch(keyToCheck)
            {
                case KeyCode.H:
                    return H_Key;
                case KeyCode.J:
                    return J_Key;
                case KeyCode.K:
                    return K_Key;
                default:
                    Debug.LogError("Unhandled switch for key " + keyToCheck);
                    return InputPressType.None;
            }
        }

        //public override string ToString()
        //{
        //    return $"DirPressed: {dirPressed}, throttleAmount: {throttleAmount}, dirPressed {dirPressed}, Yaw {yawPressed}";
        //}
    }


}
 
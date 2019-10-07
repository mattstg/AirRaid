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
    public InputPkg refreshInputPkg = new InputPkg();
    public InputPkg physicsRefreshInputPkg = new InputPkg();

    public void Initialize()
    {
    }

    public void PhysicsRefresh()
    {
        SetInputPkg(physicsRefreshInputPkg);
    }

    private void SetInputPkg(InputPkg ip)
    {
        ip.jumpPressed = Input.GetButtonDown("Jump");
        ip.jumpHeld = Input.GetButton("Jump");
        ip.dirPressed = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public void Refresh()
    {
        SetInputPkg(refreshInputPkg);
    }

    public void PostInitialize()
    {
        
    }

    public class InputPkg
    {
        public Vector2 dirPressed;
        public bool jumpPressed;
        public bool jumpHeld;

        public override string ToString()
        {
            return $"DirPressed: {dirPressed}, jumpFirstPressed: {jumpPressed}, jumpHeld {jumpHeld}";
        }
    }
}

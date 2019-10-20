using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : IManagable {
    #region Singleton
    private static InventoryManager instance;
    private InventoryManager() { }
    public static InventoryManager Instance { get { return instance ?? (instance = new InventoryManager()); } }
    #endregion

    public static Dictionary<string, Item> itemList;

    public void Initialize() {
        itemList = new Dictionary<string, Item>();
    }

    public void PostInitialize() {
    }

    public void PhysicsRefresh() {
    }

    public void Refresh() {
    }

    //get slot from next available dictionairy item
    public void AddItem(int slot, Item item) {
        itemList.Add("Slot_" + slot, item);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : IManagable {
    #region Singleton
    private static InventoryManager instance;
    private InventoryManager() { }
    public static InventoryManager Instance { get { return instance ?? (instance = new InventoryManager()); } }
    #endregion

    public Dictionary<string, Item> itemList;

    public void Initialize() {
        itemList = new Dictionary<string, Item>();
    }

    public void PostInitialize() { }

    public void PhysicsRefresh() { }

    public void Refresh() { }

    public void AddItem(int slot, Item item) {
        itemList.Add("Slot_" + slot, item);
    }

    public void AddItem(string slot, Item item) {
        itemList.Add(slot, item);
    }
}

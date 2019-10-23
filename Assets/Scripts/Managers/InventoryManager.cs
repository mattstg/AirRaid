using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryType { SELLGRID, BODYPART };
public class InventoryManager : IManagable {
    #region Singleton
    private static InventoryManager instance;
    private InventoryManager() { }
    public static InventoryManager Instance { get { return instance ?? (instance = new InventoryManager()); } }
    #endregion

    public Dictionary<string, Item> itemList;
    public Dictionary<BodyPart, Item> bodyPartList;
    public Dictionary<string, Item> sellList;
    [HideInInspector] public InventoryType inventoryType;

    [HideInInspector] public float energySellAmount;

    public void Initialize() {
        itemList = new Dictionary<string, Item>();
        sellList = new Dictionary<string, Item>();
        bodyPartList = new Dictionary<BodyPart, Item>();
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

    public void AddItemToBodyPart(BodyPart part, Item item) {
        bodyPartList.Add(part, item);
    }

    public void AddItemToSellGrid(int slot, Item item) {
        sellList.Add("Slot_" + slot, item);
    }
}

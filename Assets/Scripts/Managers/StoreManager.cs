using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : IManagable {
    #region Singleton
    private static StoreManager instance;
    private StoreManager() { }
    public static StoreManager Instance { get { return instance ?? (instance = new StoreManager()); } }
    #endregion

    public Dictionary<string, Item> itemList;

    public void Initialize() {
        itemList = new Dictionary<string, Item>();
    }

    public void PostInitialize() {
        Item[] sObject = Resources.LoadAll<Item>("ScirptableObject/");
        for (int i = 1; i <= 9; i++) {
            itemList.Add("Slot_" + i, sObject[i-1]);
        }
    }

    public void PhysicsRefresh() {
    }

    public void Refresh() {
    }
}

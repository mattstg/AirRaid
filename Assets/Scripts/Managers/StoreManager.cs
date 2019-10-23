using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : IManagable {
    #region Singleton
    private static StoreManager instance;
    private StoreManager() { }
    public static StoreManager Instance { get { return instance ?? (instance = new StoreManager()); } }
    #endregion

    public Dictionary<string, Item> itemList;
    [HideInInspector] public bool storeActive;
    [HideInInspector] public bool openStore;

    public void Initialize() {
        itemList = new Dictionary<string, Item>();
    }

    public void PostInitialize() {
        Item[] sObject = Resources.LoadAll<Item>("ScriptableObject/");
        for (int i = 0; i < 9; i++) {
            itemList.Add("Slot_" + (i + 1), sObject[i]);
            GameObject.Find("Slot_" + (i + 1) + "_Image").GetComponentInChildren<Image>().sprite = sObject[i].imgSprite;
        }
    }

    public void PhysicsRefresh() {
    }

    public void Refresh() {
    }

    public void SetupItemInStoreBoard() {
        Item[] sObject = Resources.LoadAll<Item>("ScriptableObject/");
        for (int i = 0; i < 9; i++) {
            GameObject slot = GameObject.Find("Slot_" + (i + 1) + "_Image");
            slot.GetComponentInChildren<Image>().sprite = sObject[i].imgSprite;
            if (InventoryManager.Instance.itemList.ContainsValue(sObject[i])) {
                slot.transform.GetChild(0).gameObject.SetActive(true);
                slot.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Purchased";
            } else {
                slot.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public Item GetItemByName(string name) {
        Item itemHolder = null;
        foreach (Item item in itemList.Values) {
            if (item.Name.Equals(name)) {
                itemHolder = item;
            }
        }
        return itemHolder;
    }
}

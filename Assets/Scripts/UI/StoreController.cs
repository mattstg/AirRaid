using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreController : MonoBehaviour {

    private Item clickedItem;
    private string clickedSlot;

    public void Switch() {
        string text = UILinks.instance.goTo.text;
        if (text == "Go To Inventory") {
            CloseShop();
            OpenInventory();
        } else if (text == "Go To Store") {
            CloseInventory();
            OpenShop();
        }
    }

    public void OpenDetail(string slot) {
        if (StoreManager.Instance.storeActive) {
            UILinks.instance.descriptionPanel.SetActive(true);
            Item item = StoreManager.Instance.itemList[slot];
            UILinks.instance.itemTitle.text = item.name;
            UILinks.instance.description.text = item.Description;
            UILinks.instance.cost.text = "Cost: " + item.Cost;
            clickedItem = item;
            clickedSlot = slot;
        }
    }

    public void PurchaseItem() {
        if (clickedItem.Cost <= PlayerManager.Instance.player.stats.currentEnegy) {
            if (!InventoryManager.Instance.itemList.ContainsValue(clickedItem)) {
                PlayerManager.Instance.player.stats.currentEnegy -= clickedItem.Cost;
                InventoryManager.Instance.AddItem(InventoryManager.Instance.itemList.Count + 1, clickedItem);
                GameObject.Find(clickedSlot + "_Image").transform.GetChild(0).gameObject.SetActive(true);
            } else {
                UILinks.instance.errorMessage.text = "You already purchased this item!";
                UILinks.instance.errorMessage.gameObject.SetActive(true);
                UIManager.Instance.isErrorMessageActive = true;
            }
        }
    }

    public void RemoveAbilityFromBodyPart() {
        Sprite sprite = Resources.Load<Sprite>("External/Sprites/PNG/Icons/Crystal_Icon");

        BodyPart part = (BodyPart)Enum.Parse(typeof(BodyPart), EventSystem.current.currentSelectedGameObject.name);
        InventoryManager.Instance.bodyPartList.Remove(part);
        EventSystem.current.currentSelectedGameObject.transform.GetChild(1).GetComponent<Image>().sprite = sprite;

        switch (part) {
            case BodyPart.BodyPart_Turret: UILinks.instance.abilityOne.GetComponent<Image>().sprite = sprite; break;
            case BodyPart.BodyPart_FrontCannon: UILinks.instance.abilityFour.GetComponent<Image>().sprite = sprite; break;
            case BodyPart.BodyPart_BombBay: UILinks.instance.abilityThree.GetComponent<Image>().sprite = sprite; break;
            case BodyPart.BodyPart_WingSlots: UILinks.instance.abilityTwo.GetComponent<Image>().sprite = sprite; break;
        }
        //temporary fix, figure out a way to fix this another way.... makes the inventory slots reset because without you need to click go to store and back hm weird
        StoreManager.Instance.SetupItemInStoreBoard();
        SetupItemInInventoryBoard();
    }

    public void SellItemsInSellGrid() {
        Sprite sprite = Resources.Load<Sprite>("External/Sprites/PNG/Icons/Crystal_Icon");
        for (int i = 0; i < 9; i++) {
            GameObject slotImage = GameObject.Find("Sell_Slot_" + (i + 1)).transform.GetChild(0).gameObject;
            slotImage.GetComponent<Image>().sprite = sprite;
        }
        InventoryManager.Instance.sellList.Clear();
        PlayerManager.Instance.player.stats.currentEnegy += InventoryManager.Instance.energySellAmount;
        InventoryManager.Instance.energySellAmount = 0;
        UILinks.instance.sellAmount.text = "Energy: ";

        //temporary fix, figure out a way to fix this another way...
        SwitchInventory();
        SwitchInventory();
        SetupItemInInventoryBoard();
    }

    public void SwitchInventory() {
        if (UILinks.instance.bodyPartPanel.activeInHierarchy) {
            UILinks.instance.bodyPartPanel.SetActive(false);
            UILinks.instance.sellGrid.SetActive(true);
            InventoryManager.Instance.inventoryType = InventoryType.SELLGRID;
        } else if (UILinks.instance.sellGrid.activeInHierarchy) {
            UILinks.instance.bodyPartPanel.SetActive(true);
            UILinks.instance.sellGrid.SetActive(false);
            InventoryManager.Instance.inventoryType = InventoryType.BODYPART;
        }
    }

    private void OpenShop() {
        UILinks.instance.title.text = "Store";
        UILinks.instance.goTo.text = "Go To Inventory";
    }

    private void CloseShop() {
        UILinks.instance.descriptionPanel.SetActive(false);
        UILinks.instance.switchInventoryType.SetActive(false);
        StoreManager.Instance.storeActive = false;
        SetupItemInInventoryBoard();
    }

    private void SetupItemInInventoryBoard() {
        Sprite sprite = Resources.Load<Sprite>("External/Sprites/PNG/Icons/Crystal_Icon");
        for (int i = 0; i < 9; i++) {
            GameObject slotImage = GameObject.Find("Slot_" + (i + 1) + "_Image");
            if (InventoryManager.Instance.itemList.ContainsKey("Slot_" + (i + 1))) {
                Item item = InventoryManager.Instance.itemList["Slot_" + (i + 1)];
                slotImage.GetComponentInChildren<Image>().sprite = item.imgSprite;
                slotImage.transform.GetChild(0).gameObject.SetActive(false);
                if (InventoryManager.Instance.bodyPartList.ContainsValue(item)) {
                    slotImage.transform.GetChild(0).GetComponent<Text>().text = item.Part.ToString().Replace("BodyPart_", "");
                    slotImage.transform.GetChild(0).gameObject.SetActive(true);
                }
            } else {
                slotImage.GetComponentInChildren<Image>().sprite = sprite;
                slotImage.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    private void OpenInventory() {
        UILinks.instance.title.text = "Inventory";
        UILinks.instance.goTo.text = "Go To Store";
        UILinks.instance.switchInventoryType.SetActive(true);
        UILinks.instance.bodyPartPanel.SetActive(true);
        InventoryManager.Instance.inventoryType = InventoryType.BODYPART;
    }

    private void CloseInventory() {
        UILinks.instance.switchInventoryType.SetActive(false);
        UILinks.instance.bodyPartPanel.SetActive(false);
        UILinks.instance.sellGrid.SetActive(false);
        UILinks.instance.switchInventoryType.SetActive(false);
        StoreManager.Instance.storeActive = true;
        StoreManager.Instance.SetupItemInStoreBoard();
    }
}

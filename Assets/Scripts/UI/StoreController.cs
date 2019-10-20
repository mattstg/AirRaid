using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreController : MonoBehaviour {

    private Item clickedItem;

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
        if (UIManager.Instance.storeActive) {
            UILinks.instance.descriptionPanel.SetActive(true);
            Item item = StoreManager.Instance.itemList[slot];
            UILinks.instance.itemTitle.text = item.name;
            UILinks.instance.description.text = item.Description;
            UILinks.instance.cost.text = "Cost: " + item.Cost;
            clickedItem = item;
        }
    }

    public void PurchaseItem() {
        if (clickedItem.Cost <= PlayerManager.Instance.player.stats.currentEnegy) {
            PlayerManager.Instance.player.stats.currentEnegy -= clickedItem.Cost;
            InventoryManager.Instance.AddItem(InventoryManager.Instance.itemList.Count + 1, clickedItem);
        } else {

        }
    }

    public void SwitchInventory() {
        if (UILinks.instance.bodyPartPanel.activeInHierarchy) {
            UILinks.instance.bodyPartPanel.SetActive(false);
            UILinks.instance.sellGrid.SetActive(true);
        } else if (UILinks.instance.sellGrid.activeInHierarchy) {
            UILinks.instance.bodyPartPanel.SetActive(true);
            UILinks.instance.sellGrid.SetActive(false);
        }
    }

    private void OpenShop() {
        UILinks.instance.title.text = "Store";
        UILinks.instance.goTo.text = "Go To Inventory";
        UIManager.Instance.storeActive = true;
    }

    private void CloseShop() {
        UILinks.instance.descriptionPanel.SetActive(false);
        UILinks.instance.switchInventoryType.SetActive(false);
        UIManager.Instance.storeActive = false;
    }

    private void OpenInventory() {
        UILinks.instance.title.text = "Inventory";
        UILinks.instance.goTo.text = "Go To Store";
        UILinks.instance.switchInventoryType.SetActive(true);
        UILinks.instance.bodyPartPanel.SetActive(true);
    }

    private void CloseInventory() {
        UILinks.instance.switchInventoryType.SetActive(false);
        UILinks.instance.bodyPartPanel.SetActive(false);
        UILinks.instance.sellGrid.SetActive(false);
        UILinks.instance.switchInventoryType.SetActive(false);
    }
}

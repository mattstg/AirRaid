﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreController : MonoBehaviour {

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

    public void OpenDetail() {
        if (UIManager.Instance.storeActive) {
            UILinks.instance.descriptionPanel.SetActive(true);
            //UILinks.instance.itemTitle = 
        }
    }

    private void OpenShop() {
        UILinks.instance.title.text = "Store";
        UILinks.instance.goTo.text = "Go To Inventory";
        UIManager.Instance.storeActive = true;
    }

    private void CloseShop() {
        UILinks.instance.descriptionPanel.SetActive(false);
        UIManager.Instance.storeActive = false;
    }

    private void OpenInventory() {
        UILinks.instance.title.text = "Inventory";
        UILinks.instance.goTo.text = "Go To Store";
        UILinks.instance.bodyPartPanel.SetActive(true);
    }

    private void CloseInventory() {
        UILinks.instance.switchInventoryType.SetActive(false);
        UILinks.instance.bodyPartPanel.SetActive(false);
        UILinks.instance.sellGrid.SetActive(false);
    }
}

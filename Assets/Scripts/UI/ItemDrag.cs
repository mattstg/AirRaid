using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler {
    GameObject copie;
    Vector3 initialPos;
    public static GameObject itemStart;
    //Transform startParent;
    public Color purchasedColor;
    public Color normalColor;
    Sprite startSprite;
    [HideInInspector] public bool spriteChange;

    public void OnBeginDrag(PointerEventData eventData) {
        copie = gameObject;
        itemStart = gameObject;
        initialPos = transform.position;
        //startParent = transform.parent;
        startSprite = GetComponent<Image>().sprite;
        spriteChange = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        if (!transform.GetComponent<Image>().sprite.name.Equals("Crystal_Icon")) {
            if (!StoreManager.Instance.storeActive) {
                transform.position = eventData.position;
                if (spriteChange)
                    transform.position = initialPos;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        itemStart = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (spriteChange) {
            if (InventoryManager.Instance.inventoryType == InventoryType.BODYPART) {
                Item item = InventoryManager.Instance.itemList[transform.parent.name];
                transform.GetChild(0).GetComponent<Text>().text = item.Part.ToString().Replace("BodyPart_", "");
                transform.GetChild(0).gameObject.SetActive(true);
                InventoryManager.Instance.AddItemToBodyPart(item.Part, item);
                switch (item.Part) {
                    case BodyPart.BodyPart_Turret: UILinks.instance.abilityOne.GetComponent<Image>().sprite = item.imgSprite; break;
                    case BodyPart.BodyPart_FrontCannon: UILinks.instance.abilityTwo.GetComponent<Image>().sprite = item.imgSprite; break;
                    case BodyPart.BodyPart_BombBay: UILinks.instance.abilityThree.GetComponent<Image>().sprite = item.imgSprite; break;
                    case BodyPart.BodyPart_WingSlots: UILinks.instance.abilityFour.GetComponent<Image>().sprite = item.imgSprite; break;
                }
            } else if (InventoryManager.Instance.inventoryType == InventoryType.SELLGRID) {
                if (InventoryManager.Instance.itemList.ContainsKey(transform.parent.name)) {
                    Item item = InventoryManager.Instance.itemList[transform.parent.name];
                    InventoryManager.Instance.AddItemToSellGrid(InventoryManager.Instance.sellList.Count + 1, item);
                    InventoryManager.Instance.energySellAmount += item.Cost;
                    UILinks.instance.sellAmount.text = "Energy: " + InventoryManager.Instance.energySellAmount;
                    InventoryManager.Instance.itemList.Remove(transform.parent.name);
                } else {
                    UILinks.instance.errorMessage.text = "Item is already in the sell grid!";
                }
            }
        }
        transform.position = initialPos;
    }
}

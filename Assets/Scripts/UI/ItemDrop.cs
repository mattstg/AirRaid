using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDrop : MonoBehaviour, IDropHandler {

    public void OnDrop(PointerEventData eventData) {
        ItemDrag itemDrag = eventData.pointerDrag.GetComponent<ItemDrag>();
        Item item = InventoryManager.Instance.itemList[eventData.pointerDrag.transform.parent.name];
        switch (InventoryManager.Instance.inventoryType) {
            case InventoryType.BODYPART: DropItemOnBodyPart(eventData, itemDrag, item); break;
            case InventoryType.SELLGRID: DropItemOnSellGrid(eventData, itemDrag, item); break;
        }
    }

    private void DropItemOnBodyPart(PointerEventData eventData, ItemDrag itemDrag, Item item) {
        if (itemDrag != null && item.Part.ToString().Equals(transform.parent.name) && !eventData.pointerDrag.transform.GetChild(0).gameObject.activeSelf) {
            if (!InventoryManager.Instance.bodyPartList.ContainsKey(item.Part)) {
                GetComponent<Image>().sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
                itemDrag.spriteChange = true;
            }
        }
    }

    private void DropItemOnSellGrid(PointerEventData eventData, ItemDrag itemDrag, Item item) {
        if (itemDrag != null) {
            if (!InventoryManager.Instance.sellList.ContainsValue(item) && !InventoryManager.Instance.bodyPartList.ContainsValue(item)) {
                GetComponent<Image>().sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
                itemDrag.spriteChange = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDrop : MonoBehaviour, IDropHandler//,IPointerEnterHandler,IPointerExitHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        ItemDrag itemDrag = eventData.pointerDrag.GetComponent<ItemDrag>();
        //Item item= eventData.pointerDrag.transform.parent.GetComponent<Item>();
        Item item=StoreManager.Instance.itemList[eventData.pointerDrag.transform.parent.name];
        if (itemDrag != null && item.Part.ToString()==transform.parent.name && !eventData.pointerDrag.transform.GetChild(0).gameObject.activeSelf)
        {

            //ItemDrag.itemStart.transform.SetParent(transform);
            GetComponent<Image>().sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
            itemDrag.spriteChange = true;
            InventoryManager.AddItem(eventData.pointerDrag.transform.parent.name, item);
            //itemDrag.transform.position = transform.position;

            //item.GetComponent<RectTransform>().sizeDelta = new Vector2( rect.sizeDelta.x,rect.sizeDelta.y);
        }
    }
    
    /*public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDrop : MonoBehaviour, IDropHandler//,IPointerEnterHandler,IPointerExitHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        RectTransform rect = transform as RectTransform;
        if (!RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition))
        {
            Debug.Log("drop item");
        }
        
        ItemDrag itemDrag = eventData.pointerDrag.GetComponent<ItemDrag>();
        Item item= eventData.pointerDrag.transform.parent.GetComponent<Item>();
        if (itemDrag != null && item.Part.ToString()==transform.parent.name)
        {

            //ItemDrag.itemStart.transform.SetParent(transform);
            GetComponent<Image>().sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
            itemDrag.spriteChange = true;
            //itemDrag.transform.position = transform.position;

            //item.GetComponent<RectTransform>().sizeDelta = new Vector2( rect.sizeDelta.x,rect.sizeDelta.y);
        }
    }

    public GameObject GetItem
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(1).gameObject;
            }
            return null;
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

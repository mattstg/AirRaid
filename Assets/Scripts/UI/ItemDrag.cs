using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDrag : MonoBehaviour,IDragHandler,IEndDragHandler,IBeginDragHandler
{
    GameObject copie;
    Vector3 initialPos;
    public static GameObject itemStart;
    //Transform startParent;
    public Color purchesedColor;
    public Color normalColor;
    Sprite startSprite;
    [HideInInspector]public bool spriteChange;


    public void OnBeginDrag(PointerEventData eventData)
    {
        copie = gameObject;
        itemStart = gameObject;
        initialPos = transform.position;
        //startParent = transform.parent;
        startSprite = GetComponent<Image>().sprite;
        spriteChange=false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        if (spriteChange/*transform.parent!=startParent*/)
            
            transform.position = initialPos;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemStart = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (spriteChange/*transform.parent == startParent*/)
        {
            //transform.GetChild(0).GetComponent<Text>().text = transform.parent.GetComponent<Item>().Part.ToString();
            transform.GetChild(0).gameObject.SetActive(true);
        }
        transform.position = initialPos;

        //Destroy(copie);
    }
}

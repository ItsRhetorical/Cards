using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

    public Transform parentToReturnTo = null;
    public Transform initialPlaceholder = null;
    GameObject placeholder = null;
    Vector2 dragOffset = new Vector2(0f, 0f);

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragOffset = eventData.position - (Vector2)this.transform.position;

        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent);
        placeholder.name = "Placeholder";
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = this.GetComponent<LayoutElement>().flexibleWidth;
        le.flexibleHeight = this.GetComponent<LayoutElement>().flexibleHeight;
        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        parentToReturnTo = this.transform.parent;
        initialPlaceholder = parentToReturnTo;
        this.transform.SetParent(parentToReturnTo.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(placeholder);
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position - dragOffset;

        if (placeholder.transform.parent != initialPlaceholder)
            placeholder.transform.SetParent(initialPlaceholder);

        int newSiblingIndex = initialPlaceholder.childCount;
        for (int i = 0; i < initialPlaceholder.childCount; i++)
        {
            if (this.transform.position.x < initialPlaceholder.GetChild(i).position.x)
            {
                newSiblingIndex = i;
                if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                    newSiblingIndex--;
                break;
            }
        }
        placeholder.transform.SetSiblingIndex(newSiblingIndex);


    }

}

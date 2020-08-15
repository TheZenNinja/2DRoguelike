using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : ItemSlotCore, IPointerClickHandler//, IPointerEnterHandler, IPointerExitHandler
{
    private void Awake()
    {
        OnItemChange += UpdateUI;
    }

    private void OnEnable()
    {
        UpdateUI();
    }

    private void OnValidate()
    {
        UpdateUI();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        InventoryManager.instance.OnClick(this);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        ItemDescription.instance.Show(this);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        ItemDescription.instance.Clear();
    }
}
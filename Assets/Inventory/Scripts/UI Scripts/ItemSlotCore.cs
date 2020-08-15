using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public abstract class ItemSlotCore : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] protected Item item;
    [SerializeField] protected Image sprite;

    public Action OnItemChange;

    private void OnEnable() => UpdateUI();

    public bool hasItem() => item != null;

    public void SetItem(Item item)
    {
        this.item = item;
        OnItemChange?.Invoke();
    } 

    public Item GetItem() => item;

    public void ClearItem()
    {
        item = null;
        OnItemChange?.Invoke();
    }

    public virtual void UpdateUI()
    {
        if (!gameObject.activeInHierarchy)
            return;
        if (hasItem())
        {
            sprite.enabled = true;
            sprite.sprite = item.GetSprite();
        }
        else
        {
            sprite.enabled = false;
        }
    }

    public abstract void OnPointerClick(PointerEventData eventData);
    public abstract void OnPointerExit(PointerEventData eventData);
    public abstract void OnPointerEnter(PointerEventData eventData);
}

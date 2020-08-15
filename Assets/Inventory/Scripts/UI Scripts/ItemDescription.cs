using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ItemDescription : MonoBehaviour
{
    public static ItemDescription instance;
    public ItemDescription()
    {
        instance = this;
    }

    ItemSlotCore slot;

    public Image sprite;
    public TextMeshProUGUI nameTxt, descTxt;

    private void Start() => Clear();

    public void Update()
    {
        if (slot && slot.hasItem())
        {
            sprite.enabled = true;
            sprite.sprite = slot.GetItem().GetSprite();
            nameTxt.text = slot.GetItem().GetName();
            descTxt.text = slot.GetItem().GetDescription();
        }
        else
        {
            slot = null;
            sprite.enabled = false;

            nameTxt.text = "";
            descTxt.text = "";
        }
    }
    public void Show(ItemSlotCore slot) => this.slot = slot;

    public void Clear() => slot = null;
}

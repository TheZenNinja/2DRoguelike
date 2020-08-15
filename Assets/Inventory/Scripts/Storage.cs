using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public List<Item> items = new List<Item>(16);

    private List<ItemSlot> slots;

    public Transform grid;

    private void Start()
    {
        slots = new List<ItemSlot>(grid.GetComponentsInChildren<ItemSlot>());
        for (int i = 0; i < items.Count; i++)
        {
            slots[i].SetItem(items[i]);
        }
    }

    public bool IsFull()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
                return false;
        }
        return true;
    }
    public bool ContainsItem(Item itemToFind)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
                if (items[i].SameItem(itemToFind))
                    return true;
        }
        return false;
    }
    public int FindItemIndex(Item itemToFind)
    {
        int index = -1;
        for (int i = 0; i < items.Count; i++)
            if (items[i] != null)
                if (items[i].SameItem(itemToFind))
                {
                    index = i;
                    break;
                }
        return index;
    }
    public int FindNextEmtpySlot()
    {
        int index = -1;
        for (int i = 0; i < items.Count; i++)
            if (items[i] == null)
            {
                index = i;
                break;
            }
        return index;
    }
    public bool AddItem(Item item)
    {
        if (IsFull())
            return false;

        int i = FindNextEmtpySlot();
        items[i] = item;
        return true;
    }
}

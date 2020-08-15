using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;

public class WorldItem : MonoBehaviour
{
    [Space]
    public Item item;

    public void Pickup()
    {
        Storage inv = InventoryManager.instance.playerInventory;

        if (inv.AddItem(item))
            Destroy(gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        throw new System.NotImplementedException();
    }

    public static GameObject CreateWorldItem(Item item, int count)
    {
        WorldItem worldItem = Resources.Load<GameObject>("WorldItem").GetComponent<WorldItem>();

        worldItem.item = item;

        return worldItem.gameObject;
    }
}

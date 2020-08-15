using UnityEngine;
using System.Collections;

public abstract class InventoryUIBase : MonoBehaviour
{
    public GameObject bg;
    public bool isOpen;

    public virtual void Open()
    {
        isOpen = true;
        bg.SetActive(true);
    }

    public virtual void Close()
    {
        isOpen = false;
        bg.SetActive(false);
    }
}

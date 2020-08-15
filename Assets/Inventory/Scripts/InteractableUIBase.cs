using UnityEngine;
using System.Collections;

public abstract class InteractableUIBase : MonoBehaviour
{
    public bool isOpen;
    public GameObject background;

    public virtual void Start()
    {
        Close();
    }

    public virtual void Open()
    {
        isOpen = true;
        background.SetActive(true);
    }
    public virtual void Close()
    {
        isOpen = false;
        background.SetActive(false);
    }

}

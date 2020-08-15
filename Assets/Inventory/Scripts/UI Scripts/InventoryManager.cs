using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Inventory;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public InventoryManager() => instance = this;

    //public PlayerSaveData saveData;
    [Header("Controls")]
    public bool inUI;
    public Storage playerInventory;
    public InventoryUIBase inventoryUI;
    public EquipmentManager equipmentMenu;

    [SerializeField] private Item dragItem;
    [SerializeField] private Image sprite;


    [Header("PauseMenu")]
    public GameObject pauseMenuBG;
    public bool pauseMenuOpen;


    private void Start()
    {

        pauseMenuBG.SetActive(false);
    }

    private void Update()
    {
        if (inUI)
        {
            if (dragItem != null)
            {
                sprite.enabled = true;
                sprite.sprite = dragItem.GetSprite();
            }
            else
            {
                sprite.enabled = false;
            }
        }
        else if (dragItem)
        {
            if (!playerInventory.IsFull())
            {
                playerInventory.AddItem(dragItem);
                dragItem = null;
            }
        }
        else
        {
            sprite.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {

        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inUI)
                Close();
            else
                Open();
        }
    }
    public void Open()
    {
        inventoryUI.Open();
    }
    public void Close()
    {

    }

    #region Item Swapping
    public void OnClick(ItemSlot slot)
    {
        if (dragItem != null)
        {
            if (slot.hasItem())
            {
                Item i = slot.GetItem();
                slot.SetItem(dragItem);
                dragItem = i;
            }
            else
            {
                slot.SetItem(dragItem);
                dragItem = null;
            }
        }
        else //If an item isnt being dragged
        {
            if (!slot.hasItem())
                return;

            dragItem = slot.GetItem();
        }
    }
    #endregion
    
    private void OnDestroy()
    {
        //SaveData();
    }

    public void ExitGame()
    {
        throw new System.NotImplementedException();
        //FindObjectOfType<SceneLoader>().LoadScene(0);
    }
}

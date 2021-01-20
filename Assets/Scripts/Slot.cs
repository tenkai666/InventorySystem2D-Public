using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item slotItem;
    public Image slotImage;

    //public Text slotNum;
    public TMP_Text slotNum;

    public int slotID;//空格ID 等于 物品ID
    public string slotInfo;

    public GameObject itemInSlot;

    public void ItemOnClicked()
    {
        //InventoryManager.UpdateItemInfo(slotItem.itemInfo);
        InventoryManager.UpdateItemInfo(slotInfo);
    }

    public void SetupSlot(Item item)
    {
        if (item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }

        slotImage.sprite = item.itemImage;
        slotNum.text = item.itemHeld.ToString();
        slotInfo = item.itemInfo;
    }
}

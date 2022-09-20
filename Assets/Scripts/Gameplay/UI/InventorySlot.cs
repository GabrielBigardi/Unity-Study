using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    public Item SlotItem;
    public int SlotAmount;

    public Image SlotIconImage;
    public TMP_Text SlotAmountText;

    public void RefreshSlot()
	{
        if(SlotItem == null)
		{
            SlotIconImage.gameObject.SetActive(false);
            SlotAmountText.gameObject.SetActive(false);
            return;
		}

        SlotIconImage.gameObject.SetActive(true);
        if(SlotAmount > 1) SlotAmountText.gameObject.SetActive(true);

        SlotIconImage.sprite = SlotItem.Sprite;
        SlotAmountText.SetText($"{SlotAmount}");
	}
}

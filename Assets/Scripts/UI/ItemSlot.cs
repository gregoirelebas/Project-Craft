using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
	[SerializeField] private Image icon = null;

	private Item item = null;

	public void SetItem(Item item)
	{
		this.item = item;

		if (item != null)
		{
			icon.sprite = item.sprite;
		}
		else
		{
			icon.sprite = null;
		}
	}
}

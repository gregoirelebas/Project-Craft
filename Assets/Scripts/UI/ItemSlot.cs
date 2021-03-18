using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
	[SerializeField] private Sprite ressourceIcon = null;
	[SerializeField] private Sprite toolIcon = null;

	[SerializeField] private Image icon = null;

	private Item item = null;

	public void SetItem(Item item)
	{
		this.item = item;

		if (item != null)
		{
			switch (item.type)
			{
				case Item.ItemType.Ressource:
					icon.sprite = ressourceIcon;
					break;

				case Item.ItemType.Tool:
					icon.sprite = toolIcon;
					break;

				default:
					Debug.LogError("Unknown item type : " + item.type.ToString());
					break;
			}
		}
		else
		{
			icon.sprite = null;
		}
	}
}

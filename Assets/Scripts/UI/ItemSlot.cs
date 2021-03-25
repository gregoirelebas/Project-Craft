using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField] private TextMeshProUGUI stackText = null;

	private InventoryDisplay display = null;
	private Item item = null;
	private Image icon = null;
	private int itemCount = 0;

	private void Awake()
	{
		icon = GetComponent<Image>();
	}

	public void ClearSlot()
	{
		item = null;
		icon.sprite = null;
		itemCount = 0;
		stackText.text = "";
	}

	/// <summary>
	/// Set the inventory display component.
	/// </summary>
	public void SetInventoryDisplay(InventoryDisplay display)
	{
		this.display = display;
	}

	/// <summary>
	/// Called on mouse down.
	/// </summary>
	public void OnPointerDown(PointerEventData eventData)
	{
		display.OnSelectionDown(this);
	}

	/// <summary>
	/// Called on mouse up.
	/// </summary>
	public void OnPointerUp(PointerEventData eventData)
	{
		ItemSlot slot;
		for (int i = 0; i < eventData.hovered.Count; i++)
		{
			slot = eventData.hovered[i].GetComponent<ItemSlot>();
			if (slot != null)
			{
				display.OnSelectionUp(slot);
				return;
			}
		}

		display.OnSelectionUp(null);
	}

	/// <summary>
	/// Set the item to diplay.
	/// </summary>
	public void SetItem(Item item, int count = 1)
	{
		this.item = item;
		itemCount = count;

		if (item != null)
		{
			if (item.isStackable && itemCount > 1)
			{
				stackText.text = itemCount.ToString();
			}
			else
			{
				stackText.text = "";
			}

			icon.sprite = item.sprite;
		}
		else
		{
			icon.sprite = null;
		}
	}

	/// <summary>
	/// Return the current item.
	/// </summary>
	public Item GetItem()
	{
		return item;
	}

	/// <summary>
	/// Return the current count of this item.
	/// </summary>
	/// <returns></returns>
	public int GetItemCount()
	{
		return itemCount;
	}
}

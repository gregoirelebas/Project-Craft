using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	private InventoryDisplay display = null;
	private Item item = null;
	private Image icon = null;

	private void Awake()
	{
		icon = GetComponent<Image>();
	}

	private void OnDisable()
	{
		item = null;
		icon.sprite = null;
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
		ItemSlot slot = eventData.pointerEnter.GetComponent<ItemSlot>();
		if (slot != null)
		{
			display.OnSelectionDown(slot, slot.GetItem());
		}
	}

	/// <summary>
	/// Called on mouse up.
	/// </summary>
	public void OnPointerUp(PointerEventData eventData)
	{
		ItemSlot slot = eventData.pointerEnter.GetComponent<ItemSlot>();
		if (slot != null)
		{
			display.OnSelectionUp(slot, slot.GetItem());
		}
		else
		{
			display.OnSelectionUp(null, null);
		}
	}

	/// <summary>
	/// Set the item to diplay.
	/// </summary>
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

	/// <summary>
	/// Return the current item.
	/// </summary>
	public Item GetItem()
	{
		return item;
	}
}

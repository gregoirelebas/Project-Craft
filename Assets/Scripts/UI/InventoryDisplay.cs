using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
	[SerializeField] private Transform itemGridContainer = null;
	[SerializeField] private Image selectedIcon = null;

	private Inventory inventory = null;
	private List<ItemSlot> slots = new List<ItemSlot>();

	private ItemSlot selection = null;
	private RectTransform selectedIconTransform = null;

	private void Awake()
	{
		selectedIcon.gameObject.SetActive(true);
		selectedIconTransform = selectedIcon.GetComponent<RectTransform>();
		selectedIcon.gameObject.SetActive(false);

		selection = null;

		for (int i = 0; i < itemGridContainer.childCount; i++)
		{
			ItemSlot slot = itemGridContainer.GetChild(i).GetComponent<ItemSlot>();
			slot.SetInventoryDisplay(this);
			slots.Add(slot);
		}
	}

	private void Update()
	{
		if (selection != null)
		{
			Vector2 mousePosition = Mouse.current.position.ReadValue();
			selectedIconTransform.anchoredPosition = GameManager.Instance.GetMousePositionInCanvas(mousePosition);
		}
	}

	/// <summary>
	/// Clear all slots and set inventory's items.
	/// </summary>
	private void RefreshDisplay()
	{
		if (inventory != null)
		{
			for (int i = 0; i < slots.Count; i++)
			{
				slots[i].gameObject.SetActive(false);
			}

			int itemCount = inventory.GetItemCount();
			for (int i = 0; i < inventory.GetCapacity(); i++)
			{
				slots[i].gameObject.SetActive(true);

				if (i < itemCount)
				{
					Inventory.InventorySlot slot = inventory.GetInventorySlot(i);
					slots[i].SetItem(slot.item, slot.count);
				}
			}
		}
	}

	/// <summary>
	/// Set the inventory to display.
	/// </summary>
	public void SetInventory(Inventory inventory)
	{
		this.inventory = inventory;

		RefreshDisplay();
	}

	/// <summary>
	/// Set selection after mouse down on slot. [slot] can't be null!
	/// </summary>
	public void OnSelectionDown(ItemSlot slot)
	{
		Item item = slot.GetItem();

		if (item != null)
		{
			selection = slot;

			selectedIcon.gameObject.SetActive(true);
			selectedIcon.sprite = item.sprite;
		}
	}

	/// <summary>
	/// Call when mouse is up after selection. [slot] can be null!
	/// </summary>
	public void OnSelectionUp(ItemSlot slot)
	{
		if (slot != null && selection != null)
		{
			Item item = slot.GetItem();
			int count = slot.GetItemCount();

			Item itemBuffer = selection.GetItem();
			int countBuffer = selection.GetItemCount();

			if (item == itemBuffer && count < item.stackCount)
			{
				int sub = item.stackCount - count;

				int toAdd;
				int countLeft;

				if (sub >= countBuffer)
				{
					toAdd = countBuffer;
					countLeft = 0;
				}
				else
				{
					toAdd = sub;
					countLeft = countBuffer - sub;
				}

				if (countLeft == 0)
				{
					selection.SetItem(null, 0);
				}
				else
				{
					selection.SetItem(item, countLeft);
				}

				slot.SetItem(item, count + toAdd);
			}
			else
			{
				selection.SetItem(item, count);
				slot.SetItem(itemBuffer, countBuffer);
			}
		}

		selectedIcon.gameObject.SetActive(false);

		selection = null;
	}
}

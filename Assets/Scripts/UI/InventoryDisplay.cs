using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
	private struct ItemSelection
	{
		public ItemSlot slot;
		public Item item;

		public void Clear()
		{
			slot = null;
			item = null;
		}
	}

	[SerializeField] private Transform itemGridContainer = null;
	[SerializeField] private Image selectedIcon = null;

	private Inventory inventory = null;
	private List<ItemSlot> slots = new List<ItemSlot>();

	private ItemSelection selection;
	private RectTransform selectedIconTransform = null;
	private CanvasScaler scaler = null;

	private void Awake()
	{
		scaler = GetComponentInParent<CanvasScaler>();

		selectedIcon.gameObject.SetActive(true);
		selectedIconTransform = selectedIcon.GetComponent<RectTransform>();
		selectedIcon.gameObject.SetActive(false);

		selection.Clear();

		for (int i = 0; i < itemGridContainer.childCount; i++)
		{
			ItemSlot slot = itemGridContainer.GetChild(i).GetComponent<ItemSlot>();
			slot.SetInventoryDisplay(this);
			slots.Add(slot);
		}
	}

	private void Update()
	{
		if (selection.slot != null)
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
					slots[i].SetItem(inventory.GetItem(i));
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
	public void OnSelectionDown(ItemSlot slot, Item item)
	{
		if (item != null)
		{
			selection.slot = slot;
			selection.item = item;

			selectedIcon.gameObject.SetActive(true);
			selectedIcon.sprite = item.sprite;
		}
	}

	/// <summary>
	/// Call when mouse is up after selection. [slot] can be null!
	/// </summary>
	public void OnSelectionUp(ItemSlot slot, Item item)
	{
		if (slot != null && selection.slot != null)
		{
			selection.slot.SetItem(item);
			slot.SetItem(selection.item);
		}

		selectedIcon.gameObject.SetActive(false);

		selection.Clear();
	}
}

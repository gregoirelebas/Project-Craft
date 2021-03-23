using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item", order = 0)]
public class Item : ScriptableObject
{
	public enum ItemType
	{
		Ressource,
		Tool
	}

	public string label = "";
	public ItemType type = ItemType.Ressource;
	public Sprite sprite = null;
	public bool isStackable = false;
	public int stackCount = 1;
}

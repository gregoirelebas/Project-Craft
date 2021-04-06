using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftButton : MonoBehaviour
{
	[SerializeField] private Image icon = null;
	
	private CraftBook book = null;
	private Button btn = null;

	private void Awake()
	{
		btn = GetComponent<Button>();
	}

	public void SetRecipe(CraftBook book, CraftRecipe recipe)
	{
		this.book = book;

		if (recipe != null)
		{
			icon.sprite = recipe.icon;

			btn.onClick.RemoveAllListeners();
			btn.onClick.AddListener(() => book.CraftItem(recipe));
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftBook : MonoBehaviour
{
	[SerializeField] private CraftRecipeBank recipeBank = null;

	[SerializeField] private CraftButton stickBtn = null;
	[SerializeField] private CraftButton shovelBtn = null;
	[SerializeField] private CraftButton swordBtn = null;

	private Inventory playerInventory = null;	

	private void Start()
	{
		playerInventory = GameManager.Instance.GetPlayer().GetInventory();

		CraftRecipe recipe = recipeBank.GetRecipeByLabel("Stick");	
		stickBtn.SetRecipe(this, recipe);

		recipe = recipeBank.GetRecipeByLabel("Stone Shovel");
		shovelBtn.SetRecipe(this, recipe);

		recipe = recipeBank.GetRecipeByLabel("Stone Sword");
		swordBtn.SetRecipe(this, recipe);
	}

	public void CraftItem(CraftRecipe recipe)
	{
		if (playerInventory.HasFreeSpace(recipe.item, recipe.count))
		{
			//Check if the player have all required items in his inventory.
			for (int i = 0; i < recipe.parts.Count; i++)
			{
				if (playerInventory.GetItemCount(recipe.parts[i].item) < recipe.parts[i].count)
				{
					Debug.Log("Can't create item, missing part : " + recipe.parts[i].item.label);
					return;
				}
			}

			//Then remove all items used in craft and add crafted item
			for (int i = 0; i < recipe.parts.Count; i++)
			{
				playerInventory.RemoveItem(recipe.parts[i].item, recipe.parts[i].count);
			}

			playerInventory.AddItem(recipe.item, recipe.count);

			Debug.Log("Crafted item : " + recipe.item.label);
		}
	}
}

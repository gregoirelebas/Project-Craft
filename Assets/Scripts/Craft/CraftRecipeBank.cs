using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipeBank", menuName = "Craft/RecipeBank", order = 1)]
public class CraftRecipeBank : ScriptableObject
{
	[SerializeField] private List<CraftRecipe> recipes = null;

	public CraftRecipe GetRecipeByLabel(string label)
	{
		CraftRecipe toReturn = recipes.Find(x => x.recipeName.Equals(label));

		if (toReturn == null)
		{
			Debug.LogWarning("Couldn't find recipe with label : " + label + ", return null");
		}

		return toReturn;
	}
}

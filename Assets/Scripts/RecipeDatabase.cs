using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public static class RecipeDatabase
    {

        public static Dictionary<string, Dictionary<string, Recipe>> recipes = new Dictionary<string, Dictionary<string, Recipe>>(StringComparer.InvariantCultureIgnoreCase);

        static RecipeDatabase()
        {
            //foreach (KeyValuePair<string, ScriptableObject> recipe in (Resources.Load(@"ScriptableObjects/RecipeDatabase", typeof(ScriptableObjectDatabase)) as ScriptableObjectDatabase).database)
            //{
            //    RecipeScriptableObject recipeSO = recipe.Value as RecipeScriptableObject;

            //    if (!recipes.TryGetValue(recipeSO.Type, out Dictionary<string, Recipe> existingRecipeType))
            //    { 
            //        recipes.Add(recipeSO.Type, new Dictionary<string, Recipe>(StringComparer.InvariantCultureIgnoreCase));
            //    }

            //    recipes[recipeSO.Type].Add(recipe.Key, new Recipe(recipe.Value as RecipeScriptableObject));
            //}

            foreach (KeyValuePair<string, Dictionary<string, ScriptableObject>> recipeType in (Resources.Load(@"ScriptableObjects/NewRecipeDatabase", typeof(RecipeScriptableObjectDatabase)) as RecipeScriptableObjectDatabase).database)
            {
                recipes.Add(recipeType.Key, new Dictionary<string, Recipe>(StringComparer.InvariantCultureIgnoreCase));

                foreach (KeyValuePair<string, ScriptableObject> recipeTypeRecipes in recipeType.Value)
                {
                    recipes[recipeType.Key].Add(recipeTypeRecipes.Key, new Recipe(recipeTypeRecipes.Value as RecipeScriptableObject));
                }
            }
        }

        internal static Recipe GetRecipe(string recipeType, string chosenRecipe)
        {
            if (recipes.TryGetValue(recipeType, out Dictionary<string, Recipe> existingRecipeType))
            {
                if (existingRecipeType.TryGetValue(chosenRecipe, out Recipe recipe))
                {
                    return recipe;
                }
            }

            return recipes["(None)"]["(None)"];
        }

        internal static Dictionary<string, Recipe> GetRecipesForType(string recipeType)
        {
            if (recipes.TryGetValue(recipeType, out Dictionary<string, Recipe> existingRecipe))
            {
                return existingRecipe;
            }

            return null;
        }
    }
}

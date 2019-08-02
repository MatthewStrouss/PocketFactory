using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class RecipeDatabase
    {
        private static RecipeDatabase instance;

        public Dictionary<string, Dictionary<string, Recipe>> recipes = new Dictionary<string, Dictionary<string, Recipe>>(StringComparer.InvariantCultureIgnoreCase);

        public static RecipeDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RecipeDatabase();
                }

                return instance;
            }
        }

        public RecipeDatabase()
        {
            recipes = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Recipe>>>(Resources.Load(@"Data/Recipes").ToString());
        }

        public RecipeDatabase(Recipe recipe)
        {
            RegisterRecipe(recipe);
        }

        public RecipeDatabase(string json)
        {

        }

        public Resource GetResource(string name)
        {
            ResourceDatabase.Instance.resources.TryGetValue(name, out Resource resource);
            return new Resource(resource);
        }

        public Resource GetResource(string name, int quantity)
        {
            ResourceDatabase.Instance.resources.TryGetValue(name, out Resource resource);
            return new Resource(resource);
        }

        public void RegisterRecipe(Recipe recipe)
        {
            if (recipes.TryGetValue(recipe.Type, out Dictionary<string, Recipe> recipeTypeDictEntry))
            {
                if (recipeTypeDictEntry.TryGetValue(recipe.Name, out Recipe existingRecipe))
                {
                    //Debug.Log(string.Format("Recipe already exists: {0}-{1}", recipe.Type, recipe.Name));
                }
                else
                {
                    recipeTypeDictEntry.Add(recipe.Name, recipe);
                }
            }
            else
            {
                recipes.Add(recipe.Type, new Dictionary<string, Recipe>(StringComparer.InvariantCultureIgnoreCase));
                recipes[recipe.Type].Add(recipe.Name, recipe);
            }
        }

        public Recipe GetRecipe(string type, string recipeName)
        {
            this.recipes.TryGetValue(type, out Dictionary<string, Recipe> recipeType);
            recipeType.TryGetValue(recipeName, out Recipe recipe);
            return recipe;
        }

        public Dictionary<string, Recipe> GetRecipesForType(string type)
        {
            this.recipes.TryGetValue(type, out Dictionary<string, Recipe> recipeType);
            return recipeType;
        }

        public void UnlockRecipe(Recipe recipe)
        {
            this.recipes.TryGetValue(recipe.Type, out Dictionary<string, Recipe> recipeType);

            if (recipeType != null)
            {
                recipeType.TryGetValue(recipe.Name, out Recipe existingRecipe);

                if (existingRecipe != null)
                {
                    PlayerScript playerScript = Camera.main.GetComponent<PlayerScript>();

                    if (playerScript.Money - existingRecipe.UnlockCost >= 0)
                    {
                        playerScript.AddMoney(-1 * Convert.ToInt64(existingRecipe.UnlockCost));
                        existingRecipe.IsUnlocked = true;
                    }
                }
            }
        }

        public List<string> GetAllRecipeTypes()
        {
            return this.recipes.Keys.ToList();
        }
    }

    public enum RecipeTypeEnum
    {
        BASIC,
        GEAR,
        LIQUID,
        WIRE,
        PLATE,
        RECIPE,
    }
}

using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipesPanelScript : MonoBehaviour
{
    public GameObject content;
    public Button buttonPrefab;
    public Action callback;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(GameObject starter, Action callback)
    {
        this.callback = callback;

        foreach (Transform child in this.content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        Dictionary<string, Recipe> basicRecipes = RecipeDatabase.Instance.GetRecipesForType("Basic");

        foreach(KeyValuePair<string, Recipe> recipe in basicRecipes)
        {
            Button newButton = Instantiate<Button>(this.buttonPrefab, this.content.transform);
            newButton.GetComponentInChildren<Text>().text = recipe.Value.Result.name;
            newButton.image.sprite = SpriteDatabase.Instance.GetSprite("Resource", recipe.Value.Result.name);
            newButton.GetComponent<RectTransform>().sizeDelta = new Vector2(64, 64);
            newButton.onClick.AddListener(() =>
            {
                starter.GetComponent<StarterController>().SetRecipe(recipe.Value);
                this.Deactivate();
            });
        }



        //Dictionary<string, GameObject> machines = PrefabDatabase.Instance.GetPrefabsForType("Machine");

        //foreach (KeyValuePair<string, GameObject> machine in machines)
        //{
        //    Button newButton = Instantiate<Button>(this.buttonPrefab, this.content.transform);
        //    newButton.GetComponentInChildren<Text>().text = machine.Key;
        //    newButton.image.sprite = machine.Value.GetComponent<SpriteRenderer>().sprite;
        //    newButton.GetComponent<RectTransform>().sizeDelta = new Vector2(64, 64);
        //    newButton.onClick.AddListener(() =>
        //    {
        //        Camera.main.GetComponent<PlayerScript>().SetMachine(machine.Value);
        //        this.gameObject.SetActive(false);
        //        //    GlobalManager.instance._playerManager.playerStateEnum = PlayerStateEnum.PLACE_TILE;
        //        //    GlobalManager.instance._playerManager.placeID = x.ID;
        //    });
        //}

        this.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        this.callback();
        this.gameObject.SetActive(false);
    }

    public void XButton_Click()
    {
        this.Deactivate();
    }
}

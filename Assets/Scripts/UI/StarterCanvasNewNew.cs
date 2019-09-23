using Assets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StarterCanvasNewNew : MonoBehaviour
{
    [SerializeField] private MoreLessScript MoreLessScript;
    [SerializeField] private GameObject Content;
    [SerializeField] private Toggle SelectionTogglePrefab;
    [SerializeField] private ToggleGroup ToggleGroup;

    private StarterController StarterController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Activate()
    {
        this.MoreLessScript.Activate(
            min: 1,
            current: this.StarterController.SpawnCount,
            max: 3,
            moreAction: this.UpdateCount,
            lessAction: this.UpdateCount
            );

        this.UpdateUI();
        this.gameObject.SetActive(true);
    }

    public void Activate(StarterController starterController)
    {
        this.StarterController = starterController;
        this.Activate();
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        this.CreateButtons();
    }

    public void UpdateCount(int count)
    {
        this.StarterController.SpawnCount = count;
    }

    public void CreateButtons()
    {
        foreach(Transform child in this.Content.transform)
        {
            Destroy(child.gameObject);
        }

        RecipeDatabase.GetRecipesForType("Basic").Values.OrderBy(x => x.Name).ToList().ForEach(x =>
        {
            Toggle newToggle = Instantiate(this.SelectionTogglePrefab, this.Content.transform);
            newToggle.transform.Find("Background").transform.Find("Label").GetComponent<Text>().text = x.Name;
            newToggle.transform.Find("Background").GetComponent<Image>().sprite = x.Result.Sprite;
            newToggle.group = this.ToggleGroup;
            newToggle.isOn = this.StarterController.ChosenRecipe == x;
            newToggle.onValueChanged.AddListener((value) =>
            {
                if (value)
                {
                    this.SetStarterRecipe(x);
                }

            });
        });
    }

    public void SetStarterRecipe(Recipe recipe)
    {
        this.StarterController.SetRecipe(recipe);
    }
}

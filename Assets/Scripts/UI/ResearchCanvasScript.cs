using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ResearchCanvasScript : MonoBehaviour
{
    public PlayerScriptableObject playerScriptableObject;
    public Button buttonPrefab;
    public ToggleGroup toggleGroup;
    public GameObject content;
    public GameObject XButton;

    public DatabaseScriptableObject researchDatabase;
    public DatabaseScriptableObject machineDatabase;
    public DatabaseScriptableObject recipeDatabase;

    public GameManagerController gameManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleChanged()
    {
        this.UpdateUI();
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
        this.XButton.GetComponent<CancelCanvasScript>().Activate(this.gameObject);
        this.UpdateUI();
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
        this.XButton.GetComponent<CancelCanvasScript>().Deactivate();
    }

    public void UpdateUI()
    {
        List<KeyValuePair<string, ScriptableObject>> items = this.GetUnlockables();
        foreach (Transform t in this.content.transform)
        {
            Destroy(t.gameObject);
        }

        items.OrderBy(x => (x.Value as ResearchScriptableObject).Cost).ToList().ForEach(x =>
        {
            Button newButton = Instantiate(this.buttonPrefab, this.content.transform);

            newButton.name = (x.Value as ResearchScriptableObject).Name;
            newButton.transform.Find("Image").GetComponent<Image>().sprite = (x.Value as ResearchScriptableObject).Sprite;
            newButton.transform.Find("NameText").GetComponent<Text>().text = (x.Value as ResearchScriptableObject).Name;
            newButton.transform.Find("DescriptionText").GetComponent<Text>().text = (x.Value as ResearchScriptableObject).Description;
            newButton.transform.Find("PriceText").GetComponent<Text>().text = (x.Value as ResearchScriptableObject).Cost.ToString();

            newButton.onClick.AddListener(() => { (x.Value as ResearchScriptableObject).Unlock(); });
        });
    }

    public List<KeyValuePair<string, ScriptableObject>> GetUnlockables()
    {
        Toggle currentToggle = toggleGroup.ActiveToggles().First();
        List<KeyValuePair<string, ScriptableObject>> items = new List<KeyValuePair<string, ScriptableObject>>();

        if (currentToggle.name == "MachinesToggle")
        {
            items = this.machineDatabase.database.Where(x => !(x.Value as ResearchScriptableObject).IsUnlocked).ToList();
            //items = this.gameManager.machineDatabase.machines.Where(x => x.Value.IsUnlocked)
        }
        else if (currentToggle.name == "UpgradesToggle")
        {
            items = this.recipeDatabase.database.Where(x => !(x.Value as ResearchScriptableObject).IsUnlocked).ToList();
        }
        else if (currentToggle.name == "RecipesToggle")
        {
            items = this.recipeDatabase.database.Where(x => !(x.Value as ResearchScriptableObject).IsUnlocked).ToList();
        }

        return items;
    }

    public void OnResearchUpdate(ResearchScriptableObject researchScriptableObject)
    {
        //List<Button> children = this.content.GetComponentsInChildren<Button>().ToList();
        //Button buttonTochange = children.FirstOrDefault(x => x.name == researchScriptableObject.Name);

        GameObject.Destroy(this.content.GetComponentsInChildren<Button>().ToList().FirstOrDefault(x => x.name == researchScriptableObject.Name).gameObject);
    }
}

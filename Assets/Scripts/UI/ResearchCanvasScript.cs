using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ResearchCanvasScript : MonoBehaviour
{
    public Button buttonPrefab;
    public ToggleGroup toggleGroup;
    public GameObject content;
    public GameObject XButton;

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
        List<KeyValuePair<string, Research>> items = this.GetUnlockables();
        foreach (Transform t in this.content.transform)
        {
            Destroy(t.gameObject);
        }

        items.OrderBy(x => x.Value.Cost).ToList().ForEach(x =>
        {
            Button newButton = Instantiate(this.buttonPrefab, this.content.transform);

            newButton.name = x.Value.Name;
            newButton.transform.Find("Image").GetComponent<Image>().sprite = x.Value.Sprite;
            newButton.transform.Find("NameText").GetComponent<Text>().text = x.Value.Name;
            newButton.transform.Find("DescriptionText").GetComponent<Text>().text = x.Value.Description;
            newButton.transform.Find("PriceText").GetComponent<Text>().text = x.Value.Cost.ToString();
            newButton.interactable = Player.playerModel.Money - x.Value.Cost > 0;

            newButton.onClick.AddListener(() => 
            {
                Player.playerModel.SubMoney(x.Value.Cost, false);
                x.Value.Unlock();
                Destroy(newButton.gameObject);
            });
        });
    }

    public List<KeyValuePair<string, Research>> GetUnlockables()
    {
        Toggle currentToggle = toggleGroup.ActiveToggles().First();
        //List<KeyValuePair<string, ScriptableObject>> items = new List<KeyValuePair<string, ScriptableObject>>();
        List<KeyValuePair<string, Research>> items = new List<KeyValuePair<string, Research>>();

        if (currentToggle.name == "MachinesToggle")
        {
            //items = this.machineDatabase.database.Where(x => !(x.Value as ResearchScriptableObject).IsUnlocked).ToList();
            //items = this.gameManager.machineDatabase.machines.Where(x => x.Value.IsUnlocked)
            items = ResearchDatabase.database["Machine"].Where(x => !x.Value.IsUnlocked).ToList();
        }
        else if (currentToggle.name == "UpgradesToggle")
        {
            //items = this.recipeDatabase.database.Where(x => !(x.Value as ResearchScriptableObject).IsUnlocked).ToList();
            items = ResearchDatabase.database["Upgrade"].Where(x => !x.Value.IsUnlocked).ToList();
        }
        else if (currentToggle.name == "RecipesToggle")
        {
            //items = this.recipeDatabase.database.Where(x => !(x.Value as ResearchScriptableObject).IsUnlocked).ToList();
            items = ResearchDatabase.database["Recipe"].Where(x => !x.Value.IsUnlocked).ToList();
        }

        return items;
    }
}

using Assets;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json;
using UnityEngine.EventSystems;

public class GameManagerController : MonoBehaviour
{
    public GUIManagerController gUIManagerController;

    private static GameManagerController instance;
    public static GameManagerController Instance
    {
        get
        {
            if (instance is null)
            {
                instance = new GameManagerController();
            }

            return instance;
        }
    }

    private bool isPaused;
    public bool IsPaused
    {
        get => this.isPaused;
    }

    public GameManagerController()
    {
        this.isPaused = false;
    }

    [SerializeField] public PlayerScriptableObject playerScriptableObject;

    private void Awake()
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, "PlayerSave.json")))
        {
            GameSaveModel gameSaveModel = Newtonsoft.Json.JsonConvert.DeserializeObject<GameSaveModel>(File.ReadAllText(Path.Combine(Application.persistentDataPath, "PlayerSave.json")));

            gameSaveModel.PlacedMachineModels.ToGameObjectList();/*.ForEach(x =>
            {
                GameObject go = Instantiate(x.gameObject, x.transform.position, x.transform.rotation);
                go.GetComponent<MachineController>().SetControllerValues(x.GetComponent<MachineController>().controller);
            });*/

            gameSaveModel.PlayerModel.ToPlayerScript(this.playerScriptableObject);

            //MachineDatabase.Instance.machines = gameSaveModel.MachineDatabase;
            //PrefabDatabase.Instance.GetPrefabsForType("Machine").Values.ToList().ForEach(x => x.GetComponent<MachineController>().SetupMachine());

            //RecipeDatabase.Instance.recipes = gameSaveModel.RecipeDatabase;

            ResearchDatabase.OverwriteFromSave(gameSaveModel.ResearchDatabase);
        }
        else
        {
            this.playerScriptableObject.AddMoney(100000, false);
        }
    }

    private void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            this.Save();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnApplicationQuit()
    {
        this.Save();
    }

    private void Save()
    {
        File.Delete(Path.Combine(Application.persistentDataPath, "PlayerSave.json.bak.old"));

        if (File.Exists(Path.Combine(Application.persistentDataPath, "PlayerSave.json")))
        {
            File.Move(Path.Combine(Application.persistentDataPath, "PlayerSave.json"), Path.Combine(Application.persistentDataPath, "PlayerSave.json.bak.old"));
        }

        GameSaveModel gameSaveModel = new GameSaveModel();
        gameSaveModel.PlacedMachineModels = UnityEngine.Object.FindObjectsOfType<GameObject>().Where(x => x.layer == 8).ToList().ToMachineModelList();
        gameSaveModel.PlayerModel = this.playerScriptableObject.ToPlayerModel();
        //gameSaveModel.MachineDatabase = MachineDatabase.Instance.machines;
        //gameSaveModel.RecipeDatabase = RecipeDatabase.Instance.recipes;
        gameSaveModel.ResearchDatabase = ResearchDatabase.database;


        File.WriteAllText(Path.Combine(Application.persistentDataPath, "PlayerSave.json"), Newtonsoft.Json.JsonConvert.SerializeObject(gameSaveModel, new Newtonsoft.Json.JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }));

        //BinaryFormatter bf = new BinaryFormatter();
        //using (var ms = new MemoryStream())
        //{
        //    bf.Serialize(ms, gameSaveModel);
        //    File.WriteAllBytes(@"Assets/StreamingAssets/PlayerSave", ms.ToArray());
        //}
    }

    public bool PlayPause()
    {
        this.isPaused = !this.isPaused;
        return this.isPaused;
    }
}

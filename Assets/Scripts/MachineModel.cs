using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class MachineModel
    {
        public MachineModel()
        {

        }

        public MachineModel(GameObject machine)
        {

        }

        //private MachineController machineController;
        //public MachineController MachineController
        //{
        //    get => this.machineController;
        //    set => this.machineController = value;
        //}

        private string machineName;
        public string MachineName
        {
            get => this.machineName;
            set => this.machineName = value;
        }

        private int[] position;
        [JsonProperty("Pos")]
        public int[] Position
        {
            get => this.position;
            set => this.position = value;
        }

        private int rotation;
        [JsonProperty("Rot")]
        public int Rotation
        {
            get => this.rotation;
            set => this.rotation = value;
        }

        private int spawnCount;
        public int SpawnCount
        {
            get => this.spawnCount;
            set => this.spawnCount = value;
        }

        private string chosenRecipe;
        public string ChosenRecipe
        {
            get => this.chosenRecipe;
            set => this.chosenRecipe = value;
        }

        private List<Resource> inventory;
        public List<Resource> Inventory
        {
            get => this.inventory;
            set => this.inventory = value;
        }

        private Queue<Resource> inventoryQ;
        public Queue<Resource> InventoryQ
        {
            get => this.inventoryQ;
            set => this.inventoryQ = value;
        }

        private int currentCount;
        public int CurrentCount
        {
            get => this.currentCount;
            set => this.currentCount = value;
        }

        private int direction;
        public int Direction
        {
            get => this.direction;
            set => this.direction = value;
        }

        private Direction[] directions;
        public Direction[] Directions
        {
            get => this.directions;
            set => this.directions = value;
        }

        private SelectorDirection[] selectorDirections;
        public SelectorDirection[] SelectorDirections
        {
            get => this.selectorDirections;
            set => this.selectorDirections = value;
        }
    }

    //[Serializable]
    //public class Direction
    //{
    //    private int count;
    //    public int Count
    //    {
    //        get => this.count;
    //        set
    //        {
    //            this.count = value;
    //        }
    //    }
    //}

    //[Serializable]
    //public class SelectorDirection
    //{
    //    private Resource selectedResource;
    //    public Resource SelectedResource
    //    {
    //        get => this.selectedResource;
    //        set => this.selectedResource = value;
    //    }
    //}

    public static class MachoneModelExtensions
    {
        public static MachineModel ToMachineModel(this string str)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<MachineModel>(str);
        }

        public static GameObject ToGameObject(this string str)
        {
            MachineModel machineModel = str.ToMachineModel();

            GameObject machine = GameObject.Instantiate(PrefabDatabase.Instance.prefabs["Machine"][machineModel.MachineName]);
            machine.transform.position = new Vector3(machineModel.Position[0], machineModel.Position[1], machineModel.Position[2]);
            machine.transform.eulerAngles = new Vector3(0f, 0f, (90 * machineModel.Rotation));

            MachineController machineController = machine.GetComponent<MachineController>();

            Component[] components = machine.GetComponents(typeof(MonoBehaviour));
            IMachineController imachineController = components.FirstOrDefault(x => x != machineController) as IMachineController;

            if (imachineController is StarterController starterController)
            {
                starterController.SpawnCount = machineModel.SpawnCount;
                starterController.SetRecipe(RecipeDatabase.GetRecipe(starterController.recipeType, machineModel.ChosenRecipe));
            }
            else if (imachineController is CrafterController crafterController)
            {
                crafterController.ChosenRecipe = RecipeDatabase.GetRecipe(crafterController.recipeType, machineModel.ChosenRecipe);
                crafterController.Inventory = machineModel.Inventory;
            }
            else if (imachineController is CutterController cutterController)
            {
                cutterController.Inventory = machineModel.InventoryQ;
            }
            else if (imachineController is FurnaceController furnaceController)
            {
                furnaceController.Inventory = machineModel.InventoryQ;
            }
            else if (imachineController is HydraulicPressController hydraulicPressController)
            {
                hydraulicPressController.Inventory = machineModel.InventoryQ;
            }
            else if (imachineController is SelectorController selectorController)
            {
            }
            else if (imachineController is SellerController sellerController)
            {
            }
            else if (imachineController is SplitterController splitterController)
            {
                splitterController.CurrentCount = machineModel.CurrentCount;
                splitterController.Direction = machineModel.Direction;
                machineModel.Directions = new Direction[splitterController.Directions.Count()];
                for(int i = 0; i < splitterController.Directions.Count(); i++)
                {
                    splitterController.Directions[i].Count = machineModel.Directions[i].Count;
                    //splitterController.Directions[i].Inventory = machineModel.Directions[i].Inventory;
                }
            }
            else if (imachineController is WireDrawerController wireDrawerController)
            {
                wireDrawerController.Inventory = machineModel.InventoryQ;
            }

            return machine;
        }

        public static GameObject ToGameObject(this MachineModel machineModel)
        {
            GameObject machine = GameObject.Instantiate(PrefabDatabase.Instance.prefabs["Machine"][machineModel.MachineName]);
            MachineController machineController = machine.GetComponent<MachineController>();

            machine.transform.position = new Vector3(machineModel.Position[0], machineModel.Position[1], machineModel.Position[2]);
            machine.transform.eulerAngles = new Vector3(0f, 0f, (90 * machineModel.Rotation));

            Component[] components = machine.GetComponents(typeof(MonoBehaviour));
            IMachineController imachineController = components.FirstOrDefault(x => x != machineController) as IMachineController;

            if (imachineController is StarterController starterController)
            {
                starterController.SpawnCount = machineModel.SpawnCount;
                starterController.SetRecipe(RecipeDatabase.GetRecipe(starterController.recipeType, machineModel.ChosenRecipe));
            }
            else if (imachineController is CrafterController crafterController)
            {
                crafterController.ChosenRecipe = RecipeDatabase.GetRecipe(crafterController.recipeType, machineModel.ChosenRecipe);
                crafterController.Inventory = machineModel.Inventory;
            }
            else if (imachineController is CutterController cutterController)
            {
                cutterController.Inventory = machineModel.InventoryQ;
            }
            else if (imachineController is FurnaceController furnaceController)
            {
                furnaceController.Inventory = machineModel.InventoryQ;
            }
            else if (imachineController is HydraulicPressController hydraulicPressController)
            {
                hydraulicPressController.Inventory = machineModel.InventoryQ;
            }
            else if (imachineController is SelectorController selectorController)
            {
                for (int i = 0; i < selectorController.Directions.Count(); i++)
                {
                    selectorController.Directions[i] = new SelectorDirection();
                    selectorController.Directions[i].SelectedResource = machineModel.SelectorDirections[i].SelectedResource;
                }
            }
            else if (imachineController is SellerController sellerController)
            {
            }
            else if (imachineController is SplitterController splitterController)
            {
                splitterController.CurrentCount = machineModel.CurrentCount;
                splitterController.Direction = machineModel.Direction;

                for (int i = 0; i < splitterController.Directions.Count(); i++)
                {
                    splitterController.Directions[i].Count = machineModel.Directions[i].Count;
                    //splitterController.Directions[i].Inventory = machineModel.Directions[i].Inventory;
                }

                splitterController.UpdateDirectionSum();
            }
            else if (imachineController is WireDrawerController wireDrawerController)
            {
                wireDrawerController.Inventory = machineModel.InventoryQ;
            }

            return machine;
        }

        public static List<GameObject> ToGameObjectList(this List<MachineModel> machineModels)
        {
            List<GameObject> gameObjects = new List<GameObject>();

            machineModels.ForEach(x =>
            {
                gameObjects.Add(x.ToGameObject());
            });

            return gameObjects;
        }

        public static MachineModel ToMachineModel(this GameObject machine)
        {
            MachineModel machineModel = new MachineModel();

            MachineController machineController = machine.GetComponent<MachineController>();
            machineModel.MachineName = machineController.Machine.MachineName;

            machineModel.Position = new int[3] {
                Convert.ToInt32(machine.transform.position.x),
                Convert.ToInt32(machine.transform.position.y),
                Convert.ToInt32(machine.transform.position.z)
            };

            machineModel.Rotation = Convert.ToInt32(machine.transform.rotation.eulerAngles.z / 90);

            Component[] components = machine.GetComponents(typeof(MonoBehaviour));
            IMachineController imachineController = components.FirstOrDefault(x => x != machineController) as IMachineController;

            if (imachineController is StarterController starterController)
            {
                machineModel.SpawnCount = starterController.SpawnCount;
                machineModel.ChosenRecipe = starterController.ChosenRecipe.Name;
            }
            else if (imachineController is CrafterController crafterController)
            {
                machineModel.ChosenRecipe = crafterController.ChosenRecipe.Name;
                machineModel.Inventory = crafterController.Inventory;
            }
            else if (imachineController is CutterController cutterController)
            {
                machineModel.InventoryQ = cutterController.Inventory;
            }
            else if (imachineController is FurnaceController furnaceController)
            {
                machineModel.InventoryQ = furnaceController.Inventory;
            }
            else if (imachineController is HydraulicPressController hydraulicPressController)
            {
                machineModel.InventoryQ = hydraulicPressController.Inventory;
            }
            else if (imachineController is SelectorController selectorController)
            {
                machineModel.SelectorDirections = new SelectorDirection[selectorController.Directions.Count()];
                for (int i = 0; i < selectorController.Directions.Count(); i++)
                {
                    machineModel.SelectorDirections[i] = new SelectorDirection();
                    machineModel.SelectorDirections[i].SelectedResource = selectorController.Directions[i].SelectedResource;
                }
            }
            else if (imachineController is SellerController sellerController)
            {
            }
            else if (imachineController is SplitterController splitterController)
            {
                machineModel.CurrentCount = splitterController.CurrentCount;
                machineModel.Direction = splitterController.Direction;
                machineModel.Directions = new Direction[splitterController.Directions.Count()];
                for (int i = 0; i < splitterController.Directions.Count(); i++)
                {
                    machineModel.Directions[i] = new Direction();
                    machineModel.Directions[i].Count = splitterController.Directions[i].Count;
                    //machineModel.Directions[i].Inventory = splitterController.Directions[i].Inventory;
                }
            }
            else if (imachineController is WireDrawerController wireDrawerController)
            {
                machineModel.InventoryQ = wireDrawerController.Inventory;
            }

            return machineModel;
        }

        public static List<MachineModel> ToMachineModelList(this List<GameObject> gameObjects)
        {
            List<MachineModel> machineModels = new List<MachineModel>();

            gameObjects.ForEach(x => machineModels.Add(x.ToMachineModel()));

            return machineModels;
        }

        public static List<MachineModel> ToMachineModelList(this string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<MachineModel>>(json);
        }

        public static List<GameObject> ToGameObjectList(this string json)
        {
            List<GameObject> gameObjects = new List<GameObject>();

            List<MachineModel> machineModels = json.ToMachineModelList();

            machineModels.ForEach(x => gameObjects.Add(x.ToGameObject()));

            return gameObjects;
        }
    }
}

using Assets;
using Assets.Scripts;
using DigitalRubyShared;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public PlayerStateEnum playerStateEnum;
    public GameObject machineToPlace;

    public LayerMask layerMask;

    private Vector2 mousePos;

    [SerializeField]
    private Vector3 mouseStartPos;
    [SerializeField]
    private Vector3 mouseEndPos;
    [SerializeField]
    private GameObject selectionRectangle;

    List<GameObject> selectedObjects = new List<GameObject>();
    //List<GameObject> selectedObjectsCopy = new List<GameObject>();

    public GameObject guiCanvas;

    public GameObject starterCanvas;
    public Resource resourceToSpawn;
    public List<GameObject> placedMachines;

    [SerializeField] private Camera cam;

    [SerializeField] private GameObject gameManager;

    [SerializeField] public InputMaster controls;

    [SerializeField] public MovementScript movementScript;

    [SerializeField] private Text debugText;

    private bool settingMachine = false;

    private void Awake()
    {
        Debug.Log(string.Format("Screen resolution is: {0}x{1}", Screen.width, Screen.height));
        Debug.Log(string.Format("PersistentDataPath: {0}", Application.persistentDataPath));
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //if ((playerStateEnum != PlayerStateEnum.NONE) && (machineToPlace != null))
        //{
        //    machineToPlace.transform.position = new Vector3(
        //        Mathf.Clamp(Mathf.Round(mousePos.x), BoundsX[0], BoundsX[1]), 
        //        Mathf.Clamp(Mathf.Round(mousePos.y), BoundsY[0], BoundsY[1]),
        //        -8
        //        );
        //}

        //if (Input.touchSupported && Application.platform != RuntimePlatform.WebGLPlayer)
        //{
        //    if (playerStateEnum == PlayerStateEnum.NONE)
        //    {
        //        HandleTouch();
        //    }
        //    else
        //    {
        //        HandleBuildTouch();
        //    }
        //}
        //else
        //{
        //    HandleMouse();
        //}

        //if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        //{

        //}
        //else if (Input.touchCount == 1)
        //{
        //    Touch touch = Input.GetTouch(0);

        //    mousePos = Camera.main.ScreenToWorldPoint(touch.position);

        //    if (touch.phase == TouchPhase.Ended && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        //    {
        //        if (playerStateEnum == PlayerStateEnum.NONE)
        //        {
        //            Vector2 rayPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
        //            RaycastHit2D test = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

        //            if (test)
        //            {

        //                test.transform.gameObject.GetComponent<MachineController>().OnClick();
        //                //this.starterCanvas.GetComponent<StarterPanelScript>().Activate(test.transform.gameObject);
        //            }
        //        }
        //        else if (playerStateEnum == PlayerStateEnum.PLACE_MACHINE)
        //        {
        //            if (machineToPlace != null)
        //            {
        //                //Vector2 mouseRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //                Vector3 cursorPosition = machineToPlace.transform.position;
        //                Vector2 rayPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
        //                RaycastHit2D test = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

        //                if (test)
        //                {
        //                    Debug.Log("Machine already as this position");
        //                }
        //                else
        //                {
        //                    cursorPosition.z = 0;
        //                    this.machineToPlace.GetComponent<MachineController>().Place(cursorPosition, Quaternion.identity);
        //                }
        //            }
        //        }
        //        else if (playerStateEnum == PlayerStateEnum.PLACE_MACHINE_PASTE)
        //        {
        //            if (machineToPlace != null)
        //            {
        //                Vector3 cursorPosition = machineToPlace.transform.position;
        //                Vector2 rayPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
        //                RaycastHit2D test = Physics2D.Raycast(rayPos, Vector2.zero, 0f);
        //                cursorPosition.z = 0;

        //                List<MachineController> test1 = machineToPlace.GetComponentsInChildren<MachineController>().ToList();
        //                machineToPlace.GetComponentsInChildren<MachineController>().ToList().ForEach(x =>
        //                {
        //                    GameObject go = Instantiate(x.gameObject, x.transform.position, x.transform.rotation);
        //                    go.GetComponent<MachineController>().SetControllerValues(x.controller);
        //                });
        //            }
        //        }
        //        else if (playerStateEnum == PlayerStateEnum.ROTATE_MACHINE)
        //        {
        //            Vector2 rayPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
        //            RaycastHit2D test = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

        //            if (test && test.transform.gameObject.GetComponent<MachineController>().Machine.CanRotate)
        //            {
        //                test.transform.rotation = machineToPlace.transform.rotation;
        //            }
        //        }
        //        else if (playerStateEnum == PlayerStateEnum.SELECT)
        //        {
        //            mouseStartPos = mousePos;
        //            mouseStartPos.z = -1;
        //            selectionRectangle.transform.position = mouseStartPos;
        //            selectionRectangle.SetActive(true);
        //        }
        //        else if (playerStateEnum == PlayerStateEnum.SPAWN_RESOURCE)
        //        {
        //            Vector3 coords = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
        //            coords.z = 0;

        //            GameObject go = Instantiate(PrefabDatabase.Instance.GetPrefab("Resource", "ResourcePrefab"), coords, Quaternion.Euler(transform.eulerAngles));

        //            go.GetComponent<SpriteRenderer>().sprite = SpriteDatabase.Instance.GetSprite("Resource", this.resourceToSpawn.name);
        //            ResourceController rc = go.GetComponent<ResourceController>();
        //            rc.SetResource(this.resourceToSpawn, 1);
        //            //rc.Move(moveToPosition.position);
        //            rc.nextMoveToPosition = new Vector3(2f, 2f, 0f);
        //        }
        //    }
        //}

        //if (!EventSystem.current.IsPointerOverGameObject())
        //{
        //    if (Input.GetMouseButton(0) && playerStateEnum == PlayerStateEnum.SELECT)
        //    {
        //        mouseEndPos = mousePos;
        //        selectionRectangle.transform.localScale = mouseEndPos - mouseStartPos;
        //    }

        //    if (Input.GetMouseButtonUp(0) && playerStateEnum == PlayerStateEnum.SELECT)
        //    {
        //        selectionRectangle.SetActive(false);
        //        Vector2 lowerLeftPosition = new Vector2(Mathf.Round(Mathf.Min(mouseStartPos.x, mouseEndPos.x)), Mathf.Round(Mathf.Min(mouseStartPos.y, mouseEndPos.y)));
        //        Vector2 upperRightPosition = new Vector2(Mathf.Round(Mathf.Max(mouseStartPos.x, mouseEndPos.x)), Mathf.Round(Mathf.Max(mouseStartPos.y, mouseEndPos.y)));

        //        //this.DeselectMachines();

        //        List<GameObject> allGameObjects = UnityEngine.Object.FindObjectsOfType<GameObject>().ToList();
        //        allGameObjects.Where(x => x.layer == 8).ToList().ForEach(x =>
        //        {
        //            MachineController mc = x.GetComponent<MachineController>();
        //            if (mc != null)
        //            {
        //                if (x.transform.position.x >= lowerLeftPosition.x &&
        //                    x.transform.position.y >= lowerLeftPosition.y &&
        //                    x.transform.position.x <= upperRightPosition.x &&
        //                    x.transform.position.y <= upperRightPosition.y)
        //                {
        //                    if (this.selectedObjects.Contains(x))
        //                    {
        //                        mc.DeactivateSelected();
        //                        this.selectedObjects.Remove(x);
        //                    }
        //                    else
        //                    {
        //                        mc.ActivateSelected();
        //                        selectedObjects.Add(x);
        //                    }
        //                }
        //            }
        //        });

        //        PrefabDatabase.Instance.GetPrefab("UI", "OkCancelCanvas").GetComponent<OkCancelCanvasScript>().UpdateInstructionText($"Selected {selectedObjects.Count} machines");
        //    }
        //}

        //if (Input.GetKeyUp(KeyCode.BackQuote) || Input.touchCount == 3)
        //{
        //    PrefabDatabase.Instance.GetPrefab("UI", "Cheat").GetComponent<CheatCanvasScript>().Activate();
        //}
    }

    void DeselectMachines()
    {
        this.selectedObjects?.ForEach(x => x?.GetComponent<MachineController>()?.DeactivateSelected());
        this.selectedObjects.Clear();
    }

    public void HandleClick(GestureRecognizer gesture)
    {
        if (playerStateEnum == PlayerStateEnum.NONE)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(gesture.FocusX, gesture.FocusY));
            Vector2 rayPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
            RaycastHit2D test = Physics2D.Raycast(rayPos, Vector2.zero, 0f, 1 << 8);

            if (test)
            {
                test.transform.gameObject.GetComponent<MachineController>().OnClick();
                //this.starterCanvas.GetComponent<StarterPanelScript>().Activate(test.transform.gameObject);
            }
        }
        else if (playerStateEnum == PlayerStateEnum.PLACE_MACHINE)
        {
            if (machineToPlace != null)
            {
                if (gesture.State == GestureRecognizerState.Ended)
                {
                    //Vector2 mouseRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    //Vector3 cursorPosition = machineToPlace.transform.position;
                    Vector2 rayPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
                    RaycastHit2D test = Physics2D.Raycast(rayPos, Vector2.zero, 0f, 1 << 8);

                    if (test)
                    {
                        MachineController offender = test.transform.gameObject.GetComponent<MachineController>();

                        if (offender.Machine.MachineID == this.machineToPlace.GetComponent<MachineController>().Machine.MachineID)
                        {
                            if (this.placedMachines.Contains(test.transform.gameObject))
                            {
                                offender.Sell(true);
                                this.placedMachines.Remove(test.transform.gameObject);
                            }
                        }
                    }
                    else
                    {
                        GameObject goToAdd = this.machineToPlace.GetComponent<MachineController>().Place(rayPos, Quaternion.identity);

                        if (goToAdd != null)
                        {
                            this.placedMachines.Add(goToAdd);
                        }
                    }

                    PrefabDatabase.Instance.GetPrefab("UI", "OkCancelCanvas").GetComponent<OkCancelCanvasScript>().UpdateInstructionText($"Place {this.placedMachines.Count} {this.machineToPlace.GetComponent<MachineController>().Machine.MachineName}");
                    PrefabDatabase.Instance.GetPrefab("UI", "OkCancelCanvas").GetComponent<OkCancelCanvasScript>().UpdateInstructionText2($"${this.placedMachines.Sum(x => x.GetComponent<MachineController>().Machine.BuildCost)}");
                    PrefabDatabase.Instance.GetPrefab("UI", "OkCancelCanvas").GetComponent<OkCancelCanvasScript>().SetOkButtonActive(this.placedMachines.Count > 0);
                }
            }
        }
        else if (playerStateEnum == PlayerStateEnum.SELECT)
        {
            if (gesture.State == GestureRecognizerState.Ended)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(gesture.FocusX, gesture.FocusY));
                Vector2 rayPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
                RaycastHit2D test = Physics2D.Raycast(rayPos, Vector2.zero, 0f, 1 << 8);

                if (test)
                {
                    //test.transform.gameObject.GetComponent<MachineController>().OnClick();
                    //this.starterCanvas.GetComponent<StarterPanelScript>().Activate(test.transform.gameObject);
                    GameObject machine = test.transform.gameObject;

                    if (this.selectedObjects.Contains(machine))
                    {
                        this.RemoveFromSelection(machine);
                    }
                    else
                    {
                        this.AddToSelection(machine);
                    }
                }
            }
        }
        else if (playerStateEnum == PlayerStateEnum.PLACE_MACHINE_PASTE)
        {
            if (machineToPlace != null)
            {
                Vector3 cursorPosition = machineToPlace.transform.position;
                Vector2 rayPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
                RaycastHit2D test = Physics2D.Raycast(rayPos, Vector2.zero, 0f);
                cursorPosition.z = 0;

                List<MachineController> test1 = machineToPlace.GetComponentsInChildren<MachineController>().ToList();

                machineToPlace.GetComponentsInChildren<MachineController>().ToList().ForEach(x =>
                {
                //x.transform.position = new Vector3(x.transform.position.x, x.transform.position.y, -8);
                x.Place(x.transform.position, x.transform.rotation, x.controller);
                });
            }
        }
        else if (playerStateEnum == PlayerStateEnum.ROTATE_MACHINE)
        {
            Vector2 rayPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
            RaycastHit2D test = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

            if (test && test.transform.gameObject.GetComponent<MachineController>().Machine.CanRotate)
            {
                test.transform.rotation = machineToPlace.transform.rotation;
            }
        }
        else if (playerStateEnum == PlayerStateEnum.SPAWN_RESOURCE)
        {
            Vector3 coords = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
            coords.z = 0;

            GameObject go = Instantiate(PrefabDatabase.Instance.GetPrefab("Resource", "ResourcePrefab"), coords, Quaternion.Euler(transform.eulerAngles));

            go.GetComponent<SpriteRenderer>().sprite = SpriteDatabase.Instance.GetSprite("Resource", this.resourceToSpawn.name);
            ResourceController rc = go.GetComponent<ResourceController>();
            rc.SetResource(this.resourceToSpawn, 1);
            //rc.Move(moveToPosition.position);
            rc.nextMoveToPosition = new Vector3(2f, 2f, 0f);
        }
        else if (playerStateEnum == PlayerStateEnum.MOVE_MACHINE)
        {
            if (machineToPlace != null)
            {
                Vector2 rayPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
                this.machineToPlace.transform.position = new Vector3(rayPos.x, rayPos.y, -8);
                bool canPlace = true;
                foreach(Transform child in this.machineToPlace.transform)
                {
                    Vector2 machineRayPos = new Vector2(Mathf.Round(child.transform.position.x), Mathf.Round(child.transform.position.y));
                    RaycastHit2D test = Physics2D.Raycast(machineRayPos, Vector2.zero, 0f, 1 << 8);
                    canPlace &= !test;

                    if (!canPlace) break;
                }

                if (canPlace)
                {
                    Debug.Log("You can place here");
                }
                else
                {
                    Debug.Log("You can't place here");
                }

                //Vector2 mouseRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //Vector3 cursorPosition = machineToPlace.transform.position;
                //RaycastHit2D test = Physics2D.Raycast(rayPos, Vector2.zero, 0f, 1 << 8);



                //if (canPlace)
                //{

                //}
                //else
                //{
                //    //GameObject goToAdd = this.machineToPlace.GetComponent<MachineController>().Place(rayPos, Quaternion.identity);

                //    //if (goToAdd != null)
                //    //{
                //    //this.placedMachines.Add(goToAdd);
                //    //}

                //    this.machineToPlace.transform.position = rayPos;
                //}
            }
        }
    }

    public void HandlePan(GestureRecognizer gesture)
    {
        if (playerStateEnum == PlayerStateEnum.SELECT)
        {
            if (gesture.State == GestureRecognizerState.Began)
            {
                mouseStartPos = mousePos;
                mouseStartPos.z = -1;
                selectionRectangle.transform.position = mouseStartPos;
                selectionRectangle.SetActive(true);
            }
            else if (gesture.State == GestureRecognizerState.Executing)
            {
                Vector3 start = Camera.main.ScreenToWorldPoint(new Vector3(gesture.StartFocusX, gesture.StartFocusY));
                Vector3 end = Camera.main.ScreenToWorldPoint(new Vector3(gesture.FocusX, gesture.FocusY));

                selectionRectangle.transform.localScale = end - start;
            }
            else if (gesture.State == GestureRecognizerState.Ended)
            {
                selectionRectangle.SetActive(false);
            }
        }
        else if (playerStateEnum == PlayerStateEnum.PLACE_MACHINE)
        {
            if (gesture.State == GestureRecognizerState.Executing)
            {
                if (machineToPlace != null)
                {
                    if (this.placedMachines.Count > 0)
                    {
                        GameObject lastMachinePlaced = this.placedMachines.LastOrDefault();

                        Vector3 objPos = Camera.main.WorldToScreenPoint(lastMachinePlaced.transform.position);
                        Vector3 mousePos = new Vector3(gesture.FocusX, gesture.FocusY);

                        float distX = mousePos.x - objPos.x;
                        float distY = mousePos.y - objPos.y;

                        float angle = 90 * Mathf.Ceil((Mathf.Atan2(distY, distX) * Mathf.Rad2Deg) / 90);
                        this.debugText.text = angle.ToString();
                        //Debug.Log($"Rotating {lastMachinePlaced.name} to {angle}");

                        lastMachinePlaced.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                    }
                }
            }
        }
    }

    public void ResetPlayerState()
    {
        this.playerStateEnum = PlayerStateEnum.NONE;
        //Destroy(this.machineToPlace);
        this.machineToPlace = null;
        this.movementScript.DisableBuildMode();
    }

    public void SetMachine(GameObject machineToPlace)
    {
        this.ResetPlayerState();
        this.playerStateEnum = PlayerStateEnum.PLACE_MACHINE;
        this.machineToPlace = Instantiate(machineToPlace);
        this.machineToPlace.GetComponent<BoxCollider2D>().enabled = false;
        //this.machineToPlace.layer = 0;
    }

    public void SetRotation(Quaternion rotation)
    {
        this.ResetPlayerState();
        this.playerStateEnum = PlayerStateEnum.ROTATE_MACHINE;
        GameObject test = PrefabDatabase.Instance.GetPrefab("UI", "Arrow");
        this.machineToPlace = Instantiate(test);
        this.machineToPlace.transform.rotation = rotation;
    }

    public void StartSelectMode()
    {
        this.ResetPlayerState();
        this.playerStateEnum = PlayerStateEnum.SELECT;
        PrefabDatabase.Instance.GetPrefab("UI", "OkCancelCanvas").GetComponent<OkCancelCanvasScript>().Activate(
            "Please select the machines you wish to operate on",
            () => this.AdvanceSelectMode(),
            () => this.CancelSelectMode()
            );

        this.movementScript.EnableBuildMode();
    }

    public void AdvanceSelectMode()
    {
        PrefabDatabase.Instance.GetPrefab("UI", "SelectionActionCanvas").GetComponent<SelectionActionCanvasScript>().Activate(() => this.CancelSelectMode());
    }

    public void CancelSelectMode()
    {
        this.DeselectMachines();
        this.ResetPlayerState();
    }

    public void SaveBlueprint()
    {
        throw new NotImplementedException();
    }

    public void Copy()
    {
        if (selectedObjects.Count > 0)
        {
            List<MachineModel> machineModels = selectedObjects.ToMachineModelList();
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(machineModels, new Newtonsoft.Json.JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            //string base64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(json));

            PrefabDatabase.Instance.GetPrefab("UI", "Copy").GetComponent<CopyCanvasScript>().Activate();
            PrefabDatabase.Instance.GetPrefab("UI", "Copy").GetComponent<CopyCanvasScript>().UpdateUI(json);
        }
    }

    public void Paste(string json)
    {
        GameObject parent = new GameObject();

        //string json = Encoding.ASCII.GetString(Convert.FromBase64String(base64String));

        List<GameObject> gameObjects = json.ToGameObjectList();

        parent.transform.position = new Vector3(
            Mathf.Round(gameObjects.Sum(x => x.transform.position.x) / gameObjects.Count),
            Mathf.Round(gameObjects.Sum(x => x.transform.position.y) / gameObjects.Count),
            -8
            ); ;

        gameObjects.ForEach(x => x.transform.parent = parent.transform);

        IsPasteValid(parent.GetComponentsInChildren<MachineController>().ToList(), out bool isPasteValid, out string pasteInvalidString);

        if (isPasteValid)
        {
            this.playerStateEnum = PlayerStateEnum.PLACE_MACHINE_PASTE;
            this.machineToPlace = parent;
        }
        else
        {
            PrefabDatabase.Instance.GetPrefab("UI", "Error").GetComponent<ErrorCanvasScript>().Activate(pasteInvalidString);
        }
    }

    public void PasteUI()
    {
        PrefabDatabase.Instance.GetPrefab("UI", "Paste").GetComponent<PasteCanvasScript>().Activate(Paste);
    }

    public void IsPasteValid(List<MachineController> machinesToPlace, out bool isPasteValid, out string pasteInvalidString)
    {
        isPasteValid = true;
        pasteInvalidString = string.Empty;

        if (!CanAffordPaste(machinesToPlace))
        {
            isPasteValid = false;
            pasteInvalidString = "Not enough money for all machines in paste";
        }

        if (!HasUnlockedAllMachines(machinesToPlace))
        {
            isPasteValid = false;
            pasteInvalidString = "Not all machines in paste are unlocked";
        }


        if (!HasUnlockedAllRecipes(machinesToPlace))
        {
            isPasteValid = false;
            pasteInvalidString = "Not all recipes in paste are unlocked";
        }
    }

    public bool CanAffordPaste(List<MachineController> machinesToPlace)
    {
        return Player.playerModel.Money >= machinesToPlace.Sum(x => x.Machine.BuildCost);
    }

    public bool HasUnlockedAllMachines(List<MachineController> machinesToPlace)
    {
        return machinesToPlace.All(x => x.Machine.IsUnlocked);
    }

    public bool HasUnlockedAllRecipes(List<MachineController> machinesToPlace)
    {
        return machinesToPlace
            .Where(x => x.controller is CrafterController)
            .Select(x => x.controller)
            .Cast<CrafterController>()
            .All(x => RecipeDatabase.GetRecipe(x.recipeType, x.ChosenRecipe.Name).IsUnlocked);
    }

    public void SellSelection()
    {
        if (selectedObjects.Count > 0)
        {
            selectedObjects.ForEach(x =>
            {
                x.GetComponent<MachineController>().Sell();
            });

            this.selectedObjects.Clear();
        }
    }

    public void FlipSelectionX()
    {
        if (selectedObjects.Count > 0)
        {

            Vector3 point = new Vector3(
                Mathf.Round(selectedObjects.Sum(x => x.transform.position.x) / selectedObjects.Count),
                Mathf.Round(selectedObjects.Sum(x => x.transform.position.y) / selectedObjects.Count),
                -1
                ); ;

            selectedObjects.ForEach(x =>
            {
                x.transform.RotateAround(point, new Vector3(0, 1, 0), 180);
            });
        }
    }

    public void FlipSelectionY()
    {
        if (selectedObjects.Count > 0)
        {

            Vector3 point = new Vector3(
                Mathf.Round(selectedObjects.Sum(x => x.transform.position.x) / selectedObjects.Count),
                Mathf.Round(selectedObjects.Sum(x => x.transform.position.y) / selectedObjects.Count),
                -1
                ); ;

            selectedObjects.ForEach(x =>
            {
                x.transform.RotateAround(point, new Vector3(0, 0, 1), 180);
            });
        }
    }

    public void SpawnResource(Resource resource)
    {
        this.resourceToSpawn = new Resource(resource);
        this.playerStateEnum = PlayerStateEnum.SPAWN_RESOURCE;
    }

    public void BuildMode()
    {
        // Enable OkCancelCanvas
        PrefabDatabase.Instance.GetPrefab("UI", "OkCancelCanvas").GetComponent<OkCancelCanvasScript>().Activate(
            $"Tap to build a {this.machineToPlace.GetComponent<MachineController>().Machine.MachineName}",
            okButtonAction: () => this.AcceptBuild(),
            cancelButtonAction: () => this.CancelBuild(),
            false
            );

        this.movementScript.EnableBuildMode();
    }

    public void MoveMode()
    {
        this.playerStateEnum = PlayerStateEnum.MOVE_MACHINE;
        GameObject parent = new GameObject();

        List<GameObject> newGameObjectsList = new List<GameObject>();

        this.selectedObjects.ForEach(x =>
        {
            GameObject clone = Instantiate(x);
            clone.GetComponent<BoxCollider2D>().enabled = false;
            newGameObjectsList.Add(clone);
            x.SetActive(false);
        });

        //if (this.selectedObjects.Count > 1)
        //{
            parent.transform.position = new Vector3(
                Mathf.Round(newGameObjectsList.Sum(x => x.transform.position.x) / newGameObjectsList.Count),
                Mathf.Round(newGameObjectsList.Sum(x => x.transform.position.y) / newGameObjectsList.Count),
                -8
                );
        //}
        //else
        //{
        //    parent.transform.position = new Vector3(
        //        Mathf.Round(this.selectedObjects.FirstOrDefault().transform.position.x),
        //        Mathf.Round(this.selectedObjects.FirstOrDefault().transform.position.y),
        //        -8
        //        );
        //}

        newGameObjectsList.ForEach(x => x.transform.parent = parent.transform);
        newGameObjectsList.ForEach(x => x.transform.position = new Vector3(x.transform.position.x, x.transform.position.y, x.transform.position.z));
        this.machineToPlace = parent;
    }

    public void AcceptBuild()
    {
        // Finalize building of all machines
        this.placedMachines.Clear();
        GameObject.Destroy(this.machineToPlace);
        this.machineToPlace = null;
        this.ResetPlayerState();
        this.DeselectMachines();
    }

    public void CancelBuild()
    {
        // Cancel buildling of all machines
        this.placedMachines.ForEach(x =>
        {
            x?.GetComponent<MachineController>().Sell(true);
        });
        this.placedMachines.Clear();
        GameObject.Destroy(this.machineToPlace);
        this.machineToPlace = null;
        this.ResetPlayerState();
        this.DeselectMachines();
    }

    public void AddToSelection(GameObject machinetoAdd)
    {
        if (!this.selectedObjects.Contains(machinetoAdd))
        {
            this.selectedObjects.Add(machinetoAdd);
            machinetoAdd.GetComponent<MachineController>().ActivateSelected();
        }

        PrefabDatabase.Instance.GetPrefab("UI", "OkCancelCanvas").GetComponent<OkCancelCanvasScript>().UpdateInstructionText($"Selected {selectedObjects.Count} machines");
    }

    public void RemoveFromSelection(GameObject machineToAdd)
    {
        if (this.selectedObjects.Contains(machineToAdd))
        {
            this.selectedObjects.Remove(machineToAdd);
            machineToAdd.GetComponent<MachineController>().DeactivateSelected();
        }

        PrefabDatabase.Instance.GetPrefab("UI", "OkCancelCanvas").GetComponent<OkCancelCanvasScript>().UpdateInstructionText($"Selected {selectedObjects.Count} machines");
    }

    public void RotateSelection(Quaternion rotation)
    {
        this.selectedObjects.ForEach(x =>
        {
            x.GetComponent<MachineController>().Rotate(rotation);
        });
    }
    
    public void AcceptMoveSelection()
    {

    }

    public void CancelMoveSelection()
    {
        //this.selectedObjectsCopy.ForEach(x =>
        //{
        //    GameObject test = this.selectedObjects.FirstOrDefault(y => y.name == x.GetInstanceID().ToString());
        //    Debug.Log("Test");
        //    //.transform.position = x.transform.position;
        //});

        Destroy(this.machineToPlace);
        this.selectedObjects.ForEach(x => x.SetActive(true));
    }
}

public enum PlayerStateEnum
{
    NONE,
    PLACE_MACHINE,
    ROTATE_MACHINE,
    SELL_MACHINE,
    MOVE_MACHINE,
    SELECT,
    COPY,
    PASTE,
    PLACE_MACHINE_PASTE,
    // Cheat codes below
    SPAWN_RESOURCE,
}
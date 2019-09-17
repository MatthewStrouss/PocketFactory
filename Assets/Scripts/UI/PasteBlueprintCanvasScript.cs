using Assets.Scripts;
using DigitalRubyShared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PasteBlueprintCanvasScript : MonoBehaviour
{
    [SerializeField] private MovementScript movementScript;
    [SerializeField] private Camera cam;
    [SerializeField] private OkCancelCanvasScript OkCancelCanvas;

    private GameObject machineToPlace;
    private string JSON;
    private Action Callback;

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
        Player.playerModel.MoneyUpdated += PlayerModel_MoneyUpdated;
        this.movementScript.tapGesture.StateUpdated += this.HandleClick;
        this.movementScript.EnableBuildMode();
        this.UpdateUI();
        this.gameObject.SetActive(true);
    }

    public void Activate(string blueprintString, Action callback)
    {
        this.Callback = callback;
        this.SetupPaste(blueprintString);
        this.Activate();
    }

    public void Deactivate()
    {
        Player.playerModel.MoneyUpdated -= PlayerModel_MoneyUpdated;
        this.movementScript.tapGesture.StateUpdated -= this.HandleClick;
        this.movementScript.DisableBuildMode();

        Destroy(this.machineToPlace);

        this.gameObject.SetActive(false);
        this.Callback?.DynamicInvoke();
    }

    private void PlayerModel_MoneyUpdated(object sender, System.EventArgs e)
    {
        this.UpdateUI();
    }

    public void UpdateUI()
    {
        this.OkCancelCanvas.Activate(
            "Place the paste until you are satisfied with position. Press Ok to place and press Cancel when you're ready to leave paste mode.",
            this.AcceptPaste,
            this.CancelPaste,
            false
            );
    }

    private void AcceptPaste()
    {
        if (machineToPlace != null)
        {
            machineToPlace.GetComponentsInChildren<MachineController>().ToList().ForEach(x =>
            {
                GameObject go = Instantiate(x.gameObject, x.transform.position, x.transform.rotation);
                go.GetComponent<MachineController>().SetControllerValues(x.controller);
            });
        }

        this.UpdateUI();
    }

    private void CancelPaste()
    {
        this.Deactivate();
    }

    public void SetupPaste(string json)
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

        //IsPasteValid(parent.GetComponentsInChildren<MachineController>().ToList(), out bool isPasteValid, out string pasteInvalidString);

        this.machineToPlace = parent;
    }

    //public void IsPasteValid(List<MachineController> machinesToPlace, out bool isPasteValid, out string pasteInvalidString)
    //{
    //    isPasteValid = true;
    //    pasteInvalidString = string.Empty;

    //    if (!CanAffordPaste(machinesToPlace))
    //    {
    //        isPasteValid = false;
    //        pasteInvalidString = "Not enough money for all machines in paste";
    //    }

    //    if (!HasUnlockedAllMachines(machinesToPlace))
    //    {
    //        isPasteValid = false;
    //        pasteInvalidString = "Not all machines in paste are unlocked";
    //    }


    //    if (!HasUnlockedAllRecipes(machinesToPlace))
    //    {
    //        isPasteValid = false;
    //        pasteInvalidString = "Not all recipes in paste are unlocked";
    //    }
    //}

    //public bool CanAffordPaste(List<MachineController> machinesToPlace)
    //{
    //    return Player.playerModel.Money >= machinesToPlace.Sum(x => x.Machine.BuildCost);
    //}

    //public bool HasUnlockedAllMachines(List<MachineController> machinesToPlace)
    //{
    //    return machinesToPlace.All(x => x.Machine.IsUnlocked);
    //}

    //public bool HasUnlockedAllRecipes(List<MachineController> machinesToPlace)
    //{
    //    return machinesToPlace
    //        .Where(x => x.controller is CrafterController)
    //        .Select(x => x.controller)
    //        .Cast<CrafterController>()
    //        .All(x => RecipeDatabase.GetRecipe(x.recipeType, x.ChosenRecipe.Name).IsUnlocked);
    //}

    public void HandleClick(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended)
        {
            if (machineToPlace != null)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(gesture.FocusX, gesture.FocusY));
                Vector2 rayPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
                this.machineToPlace.transform.position = new Vector3(rayPos.x, rayPos.y, -8);
                bool canPlace = true;
                foreach (Transform child in this.machineToPlace.transform)
                {
                    Vector2 machineRayPos = new Vector2(Mathf.Round(child.transform.position.x), Mathf.Round(child.transform.position.y));
                    RaycastHit2D test = Physics2D.Raycast(machineRayPos, Vector2.zero, 0f, 1 << 8);
                    canPlace &= !test;

                    if (!canPlace) break;
                }

                this.OkCancelCanvas.SetOkButtonActive(canPlace);
            }
        }
    }
}

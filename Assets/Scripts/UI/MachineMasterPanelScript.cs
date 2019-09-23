using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineMasterPanelScript : MonoBehaviour
{
    [SerializeField] private Text HeaderText;
    [SerializeField] private Button SellButton;
    [SerializeField] private Button RotateButton;
    [SerializeField] private Button MoveButton;
    [SerializeField] private CancelCanvasScript XButton;
    [SerializeField] private Text TimerText;
    [SerializeField] private Image RadialTimer;
    [SerializeField] private Text SellPriceText;
    [SerializeField] private MoveModeCanvasScript MoveModeCanvasScript;


    [Header("Individual Machine Canvases")]
    [SerializeField] private CrafterCanvasScriptNewNew CrafterCanvas;
    [SerializeField] private CutterCanvasScriptNewNew CutterCanvas;
    [SerializeField] private FurnaceCanvasScriptNewNew FurnaceCanvas;
    [SerializeField] private HydraulicPressCanvasScriptNewNew HydraulicPressCanvas;
    [SerializeField] private SelectorCanvasScriptNewNew SelectorCanvas;
    [SerializeField] private SellerCanvasScriptNewNew SellerCanvas;
    [SerializeField] private SplitterCanvasScriptNewNew SplitterCanvas;
    [SerializeField] private StarterCanvasNewNew StarterCanvas;
    [SerializeField] private WireDrawerCanvasScriptNewNew WireDrawerCanvas;

    private MachineController MachineController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.RadialTimer.fillAmount = 1 - (this.MachineController.nextActionTime - Time.time) / this.MachineController.Machine.ActionTime;
    }

    private void Activate()
    {
        this.XButton.Activate(this.Deactivate);
        this.UpdateUI();
    }

    public void Activate(MachineController machineController)
    {
        this.MachineController = machineController;
        this.Activate();
    }

    public void Deactivate()
    {
        this.CrafterCanvas.gameObject.SetActive(false);
        this.CutterCanvas.gameObject.SetActive(false);
        this.FurnaceCanvas.gameObject.SetActive(false);
        this.HydraulicPressCanvas.gameObject.SetActive(false);
        this.SelectorCanvas.gameObject.SetActive(false);
        this.SellerCanvas.gameObject.SetActive(false);
        this.SplitterCanvas.gameObject.SetActive(false);
        this.StarterCanvas.gameObject.SetActive(false);
        this.WireDrawerCanvas.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        this.HeaderText.text = this.MachineController.Machine.MachineName;

        this.TimerText.text = $"{this.MachineController.Machine.ActionTime} sec";
        this.SellPriceText.text = $"${Assets.Scripts.Extensions.LongExtensions.FormatNumber(Mathf.RoundToInt(0.9f * this.MachineController.Machine.BuildCost))}";

        //this.SellButton.interactable = this.MachineController.Machine
        this.RotateButton.interactable = this.MachineController.Machine.CanRotate;

        this.ToggleCanvas();
        this.gameObject.SetActive(true);
    }

    private void ToggleCanvas()
    {
        if (this.MachineController.MachineType == MachineTypeEnum.CRAFTER)
        {
            this.CrafterCanvas.Activate(this.MachineController.controller as CrafterController);
        }
        else if (this.MachineController.MachineType == MachineTypeEnum.CUTTER)
        {
            this.CutterCanvas.Activate(this.MachineController.controller as CutterController);
        }
        else if (this.MachineController.MachineType == MachineTypeEnum.FURNACE)
        {
            this.FurnaceCanvas.Activate(this.MachineController.controller as FurnaceController);
        }
        else if (this.MachineController.MachineType == MachineTypeEnum.HYDRAULICPRESS)
        {
            this.HydraulicPressCanvas.Activate(this.MachineController.controller as HydraulicPressController);
        }
        else if (this.MachineController.MachineType == MachineTypeEnum.SELECTOR)
        {
            this.SelectorCanvas.Activate(this.MachineController.controller as SelectorController);
        }
        else if (this.MachineController.MachineType == MachineTypeEnum.SELLER)
        {
            this.SellerCanvas.Activate(this.MachineController.controller as SellerController);
        }
        else if (this.MachineController.MachineType == MachineTypeEnum.SPLITTER)
        {
            this.SplitterCanvas.Activate(this.MachineController.controller as SplitterController);
        }
        else if (this.MachineController.MachineType == MachineTypeEnum.STARTER)
        {
            this.StarterCanvas.Activate(this.MachineController.controller as StarterController);
        }
        else if (this.MachineController.MachineType == MachineTypeEnum.WIREDRAWER)
        {
            this.WireDrawerCanvas.Activate(this.MachineController.controller as WireDrawerController);
        }
    }

    public void SellButton_Clicked()
    {
        this.MachineController.Sell();
        this.Deactivate();
    }

    public void RotateButton_Clicked()
    {
        this.MachineController.RotateBy(Quaternion.Euler(0f, 0f, -90f));
    }

    public void MoveButton_Clicked()
    {
        List<GameObject> items = new List<GameObject>();
        items.Add(this.MachineController.gameObject);
        this.gameObject.SetActive(false);

        this.MoveModeCanvasScript.Activate(
            items,
            this.UpdateUI,
            this.UpdateUI
            );
    }
}

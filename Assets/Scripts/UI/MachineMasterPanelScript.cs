using Assets.Scripts.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineMasterPanelScript : MonoBehaviour
{
    [SerializeField] private Text HeaderText;
    [SerializeField] private CancelCanvasScript XButton;
    [SerializeField] private OkCancelCanvasScript OkCancel;
    [SerializeField] private Text TimerText;
    [SerializeField] private Image RadialTimer;
    [SerializeField] private Text ElectricityCostText;
    [SerializeField] private MoveModeCanvasScript MoveModeCanvasScript;

    [Header("Buttons")]
    [SerializeField] private Button SellButton;
    [SerializeField] private Button RotateButton;
    [SerializeField] private Button FlipHorizontalButton;
    [SerializeField] private Button FlipVerticalButton;
    [SerializeField] private Button MoveButton;

    [Header("Individual Machine Canvases")]
    [SerializeField] private CrafterCanvasScriptNewNew crafterCanvas;
    public CrafterCanvasScriptNewNew CrafterCanvas
    {
        get => this.crafterCanvas;
    }

    [SerializeField] private CutterCanvasScriptNewNew cutterCanvas;
    public CutterCanvasScriptNewNew CutterCanvas
    {
        get => this.cutterCanvas;
    }

    [SerializeField] private FurnaceCanvasScriptNewNew furnaceCanvas;
    public FurnaceCanvasScriptNewNew FurnaceCanvas
    {
        get => this.furnaceCanvas;
    }

    [SerializeField] private HydraulicPressCanvasScriptNewNew hydraulicPressCanvas;
    public HydraulicPressCanvasScriptNewNew HydraulicPressCanvas
    {
        get => this.hydraulicPressCanvas;
    }

    [SerializeField] private RollerCanvasScriptNewNew rollerCanvas;
    public RollerCanvasScriptNewNew RollerCanvas
    {
        get => this.rollerCanvas;
    }

    [SerializeField] private SelectorCanvasScriptNewNew selectorCanvas;
    public SelectorCanvasScriptNewNew SelectorCanvas
    {
        get => this.selectorCanvas;
    }

    [SerializeField] private SellerCanvasScriptNewNew sellerCanvas;
    public SellerCanvasScriptNewNew SellerCanvas
    {
        get => this.sellerCanvas;
    }

    [SerializeField] private SplitterCanvasScriptNewNew splitterCanvas;
    public SplitterCanvasScriptNewNew SplitterCanvas
    {
        get => this.splitterCanvas;
    }

    [SerializeField] private StarterCanvasNewNew starterCanvas;
    public StarterCanvasNewNew StarterCanvas
    {
        get => this.starterCanvas;
    }

    [SerializeField] private WireDrawerCanvasScriptNewNew wireDrawerCanvas;
    public WireDrawerCanvasScriptNewNew WireDrawerCanvas
    {
        get => this.wireDrawerCanvas;
    }

    private MachineController MachineController;
    private Action Callback;

    [SerializeField] private Text DebugText;

    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.RadialTimer.fillAmount = 1 - (this.MachineController.nextActionTime - Time.time) / this.MachineController.Machine.ActionTime;
    }

    private void LateUpdate()
    {
        if (TouchScreenKeyboard.visible)
        {
            this.transform.position = new Vector3(this.originalPosition.x, this.originalPosition.y + GetKeyboardSize());
        }
        else
        {
            this.transform.position = this.originalPosition;
        }
    }

    public int GetKeyboardSize()
    {
        using (AndroidJavaClass UnityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject View = UnityClass.GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer").Call<AndroidJavaObject>("getView");

            using (AndroidJavaObject Rct = new AndroidJavaObject("android.graphics.Rect"))
            {
                View.Call("getWindowVisibleDisplayFrame", Rct);

                return Screen.height - Rct.Call<int>("height");
            }
        }
    }

    private void Activate()
    {
        this.XButton.Activate(this.Deactivate);
        this.UpdateUI();
        this.originalPosition = this.transform.position;
    }

    public void Activate(MachineController machineController, Action callback)
    {
        this.MachineController = machineController;
        this.Callback = callback;
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
        this.Callback?.DynamicInvoke();
        this.XButton.Deactivate();
    }

    public void UpdateUI()
    {
        this.HeaderText.text = this.MachineController.Machine.MachineName;

        this.TimerText.text = $"{this.MachineController.Machine.ActionTime} sec";
        this.ElectricityCostText.text = $"${Assets.Scripts.Extensions.LongExtensions.FormatNumber(Mathf.RoundToInt(0.9f * this.MachineController.Machine.ElectricityCost))}";

        //this.SellButton.interactable = this.MachineController.Machine
        this.RotateButton.gameObject.SetActive(this.MachineController.Machine.CanRotate);
        this.FlipHorizontalButton.gameObject.SetActive(this.MachineController.Machine.CanRotate);
        this.FlipVerticalButton.gameObject.SetActive(this.MachineController.Machine.CanRotate);

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
        this.gameObject.SetActive(false);

        this.OkCancel.Activate(
            $"Are you sure you want to sell {this.MachineController.Machine.MachineName} for {Mathf.RoundToInt(0.9f * this.MachineController.Machine.BuildCost)}?",
            this.AcceptSell,
            this.CancelSell
            );
    }

    public void AcceptSell()
    {
        this.MachineController.Sell();
        this.Callback = null;
        this.Deactivate();
    }

    public void CancelSell()
    {
        this.UpdateUI();
    }

    public void RotateButton_Clicked()
    {
        this.MachineController.RotateBy(Quaternion.Euler(0f, 0f, -90f));
    }

    public void FlipHorizontalButton_Clicked()
    {
        this.MachineController.transform.RotateAround(
            this.MachineController.transform.position,
            new Vector3(0, 1, 0),
            180
            );
    }

    public void FlipVerticalButton_Clicked()
    {
        this.MachineController.transform.RotateAround(
            this.MachineController.transform.position,
            new Vector3(0, 0, 1),
            180
            );
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

using Assets;
using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
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

    public GameObject guiCanvas;

    public GameObject starterCanvas;

    public void AddMoney(long moneyToAdd, bool addToAverage = true)
    {
        this.Money += moneyToAdd;

        if (addToAverage)
        {

        }
    }

    public void SubMoney(long moneyToSub, bool addToAverage = true)
    {
        this.Money -= moneyToSub;

        if (addToAverage)
        {

        }
    }

    private long money;
    public long Money
    {
        get => this.money;
        set
        {
            this.money = value;
            PrefabDatabase.Instance.GetPrefab("UI", "Money").GetComponent<MoneyCanvasScript>().UpdateUI(value);
        }
    }

    public Resource resourceToSpawn;
    public List<GameObject> placedMachines;
    private static readonly float PanSpeed = 20f;
    private static readonly float ZoomSpeedTouch = 0.1f;
    private static readonly float ZoomSpeedMouse = 0.5f;

    private static readonly float[] BoundsX = new float[] { -7f, 8f };
    private static readonly float[] BoundsY = new float[] { -7f, -8f };
    private static readonly float[] ZoomBounds = new float[] { 10f, 85f };

    private Camera cam;

    private Vector3 lastPanPosition;
    private int panFingerId; // Touch mode only

    private bool wasZoomingLastFrame; // Touch mode only
    private Vector2[] lastZoomPositions; // Touch mode only

    private void Awake()
    {
        this.Money = 0;
        Debug.Log(string.Format("Screen resolution is: {0}x{1}", Screen.width, Screen.height));
        Debug.Log(string.Format("PersistentDataPath: {0}", Application.persistentDataPath));
        cam = GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if ((playerStateEnum != PlayerStateEnum.NONE) && (machineToPlace != null))
        {
            machineToPlace.transform.position = new Vector3(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y), -9);
        }

        if (playerStateEnum == PlayerStateEnum.NONE)
        {
            if (Input.touchSupported && Application.platform != RuntimePlatform.WebGLPlayer)
            {
                HandleTouch();
            }
            else
            {
                HandleMouse();
            }
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (playerStateEnum == PlayerStateEnum.NONE)
            {
                Vector2 rayPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
                RaycastHit2D test = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

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
                    //Vector2 mouseRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3 cursorPosition = machineToPlace.transform.position;
                    Vector2 rayPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
                    RaycastHit2D test = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

                    if (test)
                    {
                        Debug.Log("Machine already as this position");
                    }
                    else
                    {
                        cursorPosition.z = 0;
                        this.placedMachines.Add(this.machineToPlace.GetComponent<MachineController>().Place(cursorPosition, Quaternion.identity));
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
                        GameObject go = Instantiate(x.gameObject, x.transform.position, x.transform.rotation);
                        go.GetComponent<MachineController>().SetControllerValues(x.controller);
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
            else if (playerStateEnum == PlayerStateEnum.SELECT)
            {
                mouseStartPos = mousePos;
                mouseStartPos.z = -1;
                selectionRectangle.transform.position = mouseStartPos;
                selectionRectangle.SetActive(true);
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
        }
        else if (Input.touchCount == 1 && !EventSystem.current.IsPointerOverGameObject())
        {
            Touch touch = Input.GetTouch(0);

            mousePos = Camera.main.ScreenToWorldPoint(touch.position);

            

            if (playerStateEnum == PlayerStateEnum.NONE)
            {
                Vector2 rayPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
                RaycastHit2D test = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

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
                    //Vector2 mouseRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3 cursorPosition = machineToPlace.transform.position;
                    Vector2 rayPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
                    RaycastHit2D test = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

                    if (test)
                    {
                        Debug.Log("Machine already as this position");
                    }
                    else
                    {
                        cursorPosition.z = 0;
                        this.machineToPlace.GetComponent<MachineController>().Place(cursorPosition, Quaternion.identity);
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
                        GameObject go = Instantiate(x.gameObject, x.transform.position, x.transform.rotation);
                        go.GetComponent<MachineController>().SetControllerValues(x.controller);
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
            else if (playerStateEnum == PlayerStateEnum.SELECT)
            {
                mouseStartPos = mousePos;
                mouseStartPos.z = -1;
                selectionRectangle.transform.position = mouseStartPos;
                selectionRectangle.SetActive(true);
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
        }
        else if (Input.GetMouseButtonUp(1) || Input.touchCount == 2)
        {
            ResetPlayerState();
        }

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButton(0) && playerStateEnum == PlayerStateEnum.SELECT)
            {
                mouseEndPos = mousePos;
                selectionRectangle.transform.localScale = mouseEndPos - mouseStartPos;
            }

            if (Input.GetMouseButtonUp(0) && playerStateEnum == PlayerStateEnum.SELECT)
            {
                selectionRectangle.SetActive(false);
                Vector2 lowerLeftPosition = new Vector2(Mathf.Min(mouseStartPos.x, mouseEndPos.x), Mathf.Min(mouseStartPos.y, mouseEndPos.y));
                Vector2 upperRightPosition = new Vector2(Mathf.Max(mouseStartPos.x, mouseEndPos.x), Mathf.Max(mouseStartPos.y, mouseEndPos.y));

                if (this.selectedObjects.Count > 0)
                {
                    this.selectedObjects?.ForEach(x => x?.GetComponent<MachineController>()?.DeactivateSelected());
                }

                this.selectedObjects.Clear();

                List<GameObject> allGameObjects = UnityEngine.Object.FindObjectsOfType<GameObject>().ToList();
                allGameObjects.Where(x => x.layer == 8).ToList().ForEach(x =>
                {
                    MachineController mc = x.GetComponent<MachineController>();
                    if (mc != null)
                    {
                        if (x.transform.position.x >= lowerLeftPosition.x &&
                            x.transform.position.y >= lowerLeftPosition.y &&
                            x.transform.position.x <= upperRightPosition.x &&
                            x.transform.position.y <= upperRightPosition.y)
                        {
                            mc.ActivateSelected();
                            selectedObjects.Add(x);
                        }
                    }
                });
            }
        }

        if (Input.GetKeyUp(KeyCode.BackQuote) || Input.touchCount == 3)
        {
            PrefabDatabase.Instance.GetPrefab("UI", "Cheat").GetComponent<CheatCanvasScript>().Activate();
        }
    }

    void HandleTouch()
    {
        switch (Input.touchCount)
        {

            case 1: // Panning
                wasZoomingLastFrame = false;

                // If the touch began, capture its position and its finger ID.
                // Otherwise, if the finger ID of the touch doesn't match, skip it.
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    lastPanPosition = touch.position;
                    panFingerId = touch.fingerId;
                }
                else if (touch.fingerId == panFingerId && touch.phase == TouchPhase.Moved)
                {
                    PanCamera(touch.position);
                }
                break;

            case 2: // Zooming
                Vector2[] newPositions = new Vector2[] { Input.GetTouch(0).position, Input.GetTouch(1).position };
                if (!wasZoomingLastFrame)
                {
                    lastZoomPositions = newPositions;
                    wasZoomingLastFrame = true;
                }
                else
                {
                    // Zoom based on the distance between the new positions compared to the 
                    // distance between the previous positions.
                    float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
                    float oldDistance = Vector2.Distance(lastZoomPositions[0], lastZoomPositions[1]);
                    float offset = newDistance - oldDistance;

                    ZoomCamera(offset, ZoomSpeedTouch);

                    lastZoomPositions = newPositions;
                }
                break;

            default:
                wasZoomingLastFrame = false;
                break;
        }
    }

    void HandleMouse()
    {
        // On mouse down, capture it's position.
        // Otherwise, if the mouse is still down, pan the camera.
        if (Input.GetMouseButtonDown(0))
        {
            lastPanPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            PanCamera(Input.mousePosition);
        }

        // Check for scrolling to zoom the camera
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scroll, ZoomSpeedMouse);
    }

    void PanCamera(Vector3 newPanPosition)
    {
        // Determine how much to move the camera
        Vector3 offset = cam.ScreenToViewportPoint(lastPanPosition - newPanPosition);
        Vector3 move = new Vector3(offset.x * PanSpeed, offset.y * PanSpeed, 0);

        // Perform the movement
        transform.Translate(move, Space.World);

        // Ensure the camera remains within bounds.
        //Vector3 pos = transform.position;
        //pos.x = Mathf.Clamp(transform.position.x, BoundsX[0], BoundsX[1]);
        //pos.y = Mathf.Clamp(transform.position.y, BoundsY[0], BoundsY[1]);
        //transform.position = pos;

        // Cache the position
        lastPanPosition = newPanPosition;
    }

    void ZoomCamera(float offset, float speed)
    {
        if (offset == 0)
        {
            return;
        }

        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - (offset * speed), ZoomBounds[0], ZoomBounds[1]);
    }

    public void ResetPlayerState()
    {
        this.playerStateEnum = PlayerStateEnum.NONE;
        Destroy(this.machineToPlace);
        //this.machineToPlace = null;
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
    }

    public void Copy()
    {
        if (selectedObjects.Count > 0)
        {
            List<MachineModel> machineModels = selectedObjects.ToMachineModelList();
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(machineModels);
            string base64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(json));

            PrefabDatabase.Instance.GetPrefab("UI", "Copy").GetComponent<CopyCanvasScript>().Activate();
            PrefabDatabase.Instance.GetPrefab("UI", "Copy").GetComponent<CopyCanvasScript>().UpdateUI(base64);
        }
    }

    public void PasteUI()
    {
        PrefabDatabase.Instance.GetPrefab("UI", "Paste").GetComponent<PasteCanvasScript>().Activate(Paste);
    }

    public void Paste(string base64String)
    {
        GameObject parent = new GameObject();

        string json = Encoding.ASCII.GetString(Convert.FromBase64String(base64String));

        List<GameObject> gameObjects = json.ToGameObjectList();

        parent.transform.position = new Vector3(
            Mathf.Round(gameObjects.Sum(x => x.transform.position.x) / gameObjects.Count),
            Mathf.Round(gameObjects.Sum(x => x.transform.position.y) / gameObjects.Count),
            -1
            ); ;

        gameObjects.ForEach(x => x.transform.parent = parent.transform);

        this.playerStateEnum = PlayerStateEnum.PLACE_MACHINE_PASTE;
        this.machineToPlace = parent;
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
        // Disable MainUICanvas
        PrefabDatabase.Instance.GetPrefab("UI", "MainUI").SetActive(false);

        // Enable BuildUICanvas
        PrefabDatabase.Instance.GetPrefab("UI", "BuildUI").SetActive(true);

        // Enable OkCancelCanvas
        PrefabDatabase.Instance.GetPrefab("UI", "OkCancelUI").GetComponent<OkCancelCanvasScript>().Activate(
            okButtonAction: () => this.AcceptBuild(),
            cancelButtonAction: () => this.CancelBuild()
            );
    }

    public void AcceptBuild()
    {
        // Finalize building of all machines
        this.placedMachines.Clear();

        // Enable MainUICanvas
        PrefabDatabase.Instance.GetPrefab("UI", "MainUI").SetActive(true);

        // Disable BuildUICanvas
        PrefabDatabase.Instance.GetPrefab("UI", "BuildUI").SetActive(false);
    }

    public void CancelBuild()
    {
        // Cancel buildling of all machines
        this.placedMachines.ForEach(x =>
        {
            x.GetComponent<MachineController>().Sell(true);
        });
        this.placedMachines.Clear();

        // Enable MainUICanvas
        PrefabDatabase.Instance.GetPrefab("UI", "MainUI").SetActive(true);

        // Disable BuildUICanvas
        PrefabDatabase.Instance.GetPrefab("UI", "BuildUI").SetActive(false);
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

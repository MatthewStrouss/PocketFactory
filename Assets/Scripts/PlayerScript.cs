using Assets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerScript : MonoBehaviour
{
    public PlayerStateEnum playerStateEnum;
    public GameObject machineToPlace;

    public LayerMask layerMask;

    private Vector2 mousePos;

    [SerializeField]
    private Vector2 mouseStartPos;
    [SerializeField]
    private Vector2 mouseCurrentPos;
    [SerializeField]
    private GameObject selectionRectangle;
    private GameObject selectionRectangle2;

    public GameObject guiCanvas;

    public GameObject starterCanvas;

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

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (playerStateEnum == PlayerStateEnum.NONE)
            {
                Vector2 rayPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
                RaycastHit2D test = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

                if (test)
                {
                    test.transform.gameObject.GetComponent<StarterController>().OnPointerClick(null);
                    //this.starterCanvas.GetComponent<StarterPanelScript>().Activate(test.transform.gameObject);
                }
            }
            else 
            if (playerStateEnum == PlayerStateEnum.PLACE_MACHINE)
            {
                if (machineToPlace != null)
                {
                    //Vector2 mouseRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3 cursorPosition = machineToPlace.transform.position;
                    RaycastHit2D rayHit = Physics2D.Raycast(cursorPosition, Vector2.zero, Mathf.Infinity, layerMask);

                    Debug.Log(rayHit.collider);

                    if (rayHit.collider == null)
                    {
                        Instantiate(machineToPlace, cursorPosition, Quaternion.identity);
                    }
                }
            }
            else if (playerStateEnum == PlayerStateEnum.ROTATE_MACHINE)
            {
                Vector2 rayPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D test = Physics2D.Raycast(machineToPlace.transform.position, Vector2.zero, 0f);

                if (test)
                {
                    test.transform.rotation = machineToPlace.transform.rotation;
                }
            }
            else if (playerStateEnum == PlayerStateEnum.SELECT)
            {
                mouseStartPos = Camera.main.ScreenToWorldPoint(new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y)));
                //selectionRectangle = new Rect(mouseStartPos, new Vector2(0f, 0f));
                //selectionRectangle.SetActive(true);
                selectionRectangle2 = Instantiate(selectionRectangle, guiCanvas.transform);
                selectionRectangle2.SetActive(true);
                selectionRectangle2.transform.position = mouseStartPos;
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            ResetPlayerState();
        }

        if (Input.GetMouseButton(0) && playerStateEnum == PlayerStateEnum.SELECT)
        {
            mouseCurrentPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
            //selectionRectangle.size = new Vector2(
            //    Mathf.Abs(mouseCurrentPos.x - mouseStartPos.x),
            //    Mathf.Abs(mouseCurrentPos.y - mouseStartPos.y)
            //    );
            (selectionRectangle2.transform as RectTransform).sizeDelta = Camera.main.ScreenToWorldPoint(new Vector2(
                Mathf.Abs(mouseCurrentPos.x - mouseStartPos.x),
                Mathf.Abs(mouseCurrentPos.y - mouseStartPos.y)
                ));
        }
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
}

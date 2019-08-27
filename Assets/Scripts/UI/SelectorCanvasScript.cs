using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorCanvasScript : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private Canvas leftCanvas;
    [SerializeField] private Canvas forwardCanvas;
    [SerializeField] private Canvas rightanvas;
    [SerializeField] private Canvas XButton;
    [SerializeField] private GameObject leftButton;
    //[SerializeField] private GameObject forwardButton;
    [SerializeField] private GameObject rightButton;
    [SerializeField] private GameObject recipePanel;
    private GameObject selectorGameObject;
    private SelectorController selectorController;

    private long leftIndex;
    private long forwardIndex;
    private long rightIndex;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
        this.UpdateUI();
        this.XButton.GetComponent<CancelCanvasScript>().Activate(this.gameObject);
    }

    public void Activate(GameObject selectorGameObject)
    {
        this.selectorGameObject = selectorGameObject;
        this.selectorController = this.selectorGameObject.GetComponent<SelectorController>();

        this.mainPanel.transform.rotation = Quaternion.Euler(this.mainPanel.transform.rotation.eulerAngles.x, this.mainPanel.transform.rotation.eulerAngles.y, this.selectorGameObject.transform.rotation.eulerAngles.z);
        this.leftButton.transform.rotation = Quaternion.Euler(this.leftButton.transform.rotation.eulerAngles.x, this.leftButton.transform.rotation.eulerAngles.y, 0);
        this.rightButton.transform.rotation = Quaternion.Euler(this.rightButton.transform.rotation.eulerAngles.x, this.rightButton.transform.rotation.eulerAngles.y, 0);

        if (this.selectorController.selectorDirectionEnum == SelectorDirectionEnum.LEFT)
        {
            this.leftCanvas.gameObject.SetActive(true);
            this.leftIndex = 0;
            this.rightanvas.gameObject.SetActive(false);
            this.rightIndex = 0;

            this.leftButton.GetComponentInChildren<ResourceCanvasScript>().SetResource(this.selectorController.Directions[this.leftIndex].SelectedResource);
            this.leftButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                this.gameObject.SetActive(false);

                this.recipePanel.GetComponent<SelectResourceCanvasScript>().Activate((r) =>
                {
                    this.selectorController.Directions[this.leftIndex].SelectedResource = r;
                    this.leftButton.GetComponentInChildren<ResourceCanvasScript>().SetResource(this.selectorController.Directions[this.leftIndex].SelectedResource);
                    this.Activate(this.selectorGameObject);
                });
            });
        }
        else if (this.selectorController.selectorDirectionEnum == SelectorDirectionEnum.RIGHT)
        {
            this.leftCanvas.gameObject.SetActive(false);
            this.leftIndex = 0;
            this.rightanvas.gameObject.SetActive(true);
            this.rightIndex = 0;

            this.rightButton.GetComponentInChildren<ResourceCanvasScript>().SetResource(this.selectorController.Directions[this.rightIndex].SelectedResource);
            this.rightButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                this.gameObject.SetActive(false);

                this.recipePanel.GetComponent<SelectResourceCanvasScript>().Activate((r) =>
                {
                    this.selectorController.Directions[this.rightIndex].SelectedResource = r;
                    this.rightButton.GetComponentInChildren<ResourceCanvasScript>().SetResource(this.selectorController.Directions[this.rightIndex].SelectedResource);
                    this.Activate(this.selectorGameObject);
                });
            });
        }
        else if (this.selectorController.selectorDirectionEnum == SelectorDirectionEnum.MULTI)
        {
            this.leftCanvas.gameObject.SetActive(true);
            this.leftIndex = 0;
            this.rightanvas.gameObject.SetActive(true);
            this.rightIndex = 2;

            this.leftButton.GetComponentInChildren<ResourceCanvasScript>().SetResource(this.selectorController.Directions[this.leftIndex].SelectedResource);
            this.leftButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                this.gameObject.SetActive(false);

                this.recipePanel.GetComponent<SelectResourceCanvasScript>().Activate((r) =>
                {
                    this.selectorController.Directions[this.leftIndex].SelectedResource = r;
                    this.leftButton.GetComponentInChildren<ResourceCanvasScript>().SetResource(this.selectorController.Directions[this.leftIndex].SelectedResource);
                    this.Activate(this.selectorGameObject);
                });
            });

            this.rightButton.GetComponentInChildren<ResourceCanvasScript>().SetResource(this.selectorController.Directions[this.rightIndex].SelectedResource);
            this.rightButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                this.gameObject.SetActive(false);

                this.recipePanel.GetComponent<SelectResourceCanvasScript>().Activate((r) =>
                {
                    this.selectorController.Directions[this.rightIndex].SelectedResource = r;
                    this.rightButton.GetComponentInChildren<ResourceCanvasScript>().SetResource(this.selectorController.Directions[this.rightIndex].SelectedResource);
                    this.Activate(this.selectorGameObject);
                });
            });
        }

        this.forwardCanvas.gameObject.SetActive(true);
        this.forwardIndex = 1;
        //this.forwardButton.GetComponentInChildren<ResourceCanvasScript>().SetResource(this.selectorController.Directions[this.forwardIndex].SelectedResource);

        this.Activate();
    }

    public void Deactivate()
    {
        this.XButton.GetComponent<CancelCanvasScript>().Deactivate();
    }

    public void UpdateUI()
    {

    }
}

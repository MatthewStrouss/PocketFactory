using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ArrowToggleCanvasScript : MonoBehaviour
{
    private bool showArrows = false;
    [SerializeField] private Image showImage;
    [SerializeField] private Image hideImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ArrowToggleButton_Clicked()
    {
        showArrows = !showArrows;

        showImage.gameObject.SetActive(!showArrows);
        hideImage.gameObject.SetActive(showArrows);

        UnityEngine.Object.FindObjectsOfType<GameObject>().ToList().Where(x => x.layer.Equals(8)).ToList().ForEach(x =>
        {
            x.GetComponent<MachineController>()?.ToggleArrow();
        });
    }
}

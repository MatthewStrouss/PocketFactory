using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ArrowToggleCanvasScript : MonoBehaviour
{
    private bool showArrows = false;
    [SerializeField] private Text showText;
    [SerializeField] private Text hideText;

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

        showText.gameObject.SetActive(!showArrows);
        hideText.gameObject.SetActive(showArrows);

        UnityEngine.Object.FindObjectsOfType<GameObject>().ToList().Where(x => x.layer.Equals(8)).ToList().ForEach(x =>
        {
            x.GetComponent<MachineController>()?.ToggleArrow();
        });
    }
}

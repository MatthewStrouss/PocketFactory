using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionObjectScript : MonoBehaviour
{
    private Action<GameObject> SelectionAction;

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
        this.gameObject.SetActive(true);
    }

    public void Activate(Action<GameObject> selectionAction)
    {
        this.SelectionAction = selectionAction;
        this.Activate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.SelectionAction?.DynamicInvoke(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }

    private void OnDisable()
    {
        this.transform.localScale = new Vector3(0f, 0f);
    }
}

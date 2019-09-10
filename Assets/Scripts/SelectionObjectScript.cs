using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionObjectScript : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            this.playerScript.AddToSelection(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }

    private void OnDisable()
    {
        this.transform.localScale = new Vector3(0f, 0f);
    }
}

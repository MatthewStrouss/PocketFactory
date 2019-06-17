using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateScript : MonoBehaviour
{
    [SerializeField]
    private GameObject finalObject;

    private Vector2 mousePos;

    [SerializeField]
    private LayerMask allTilesLayer;
    
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
        if (transform.position.x < -8)
        {
            transform.position = new Vector2(-8, transform.position.y);
        }
        else if (transform.position.x > 9)
        {
            transform.position = new Vector2(9, transform.position.y);
        }
        else if (transform.position.y < -13)
        {
            transform.position = new Vector2(transform.position.x, -13);
        }
        else if (transform.position.y > 4)
        {
            transform.position = new Vector2(transform.position.x, 4);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseRay = Camera.main.ScreenToWorldPoint(transform.position);
            RaycastHit2D rayHit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, allTilesLayer);

            if(rayHit.collider == null)
            {
                Instantiate(finalObject, transform.position, Quaternion.identity);
            }
        }
    }
}

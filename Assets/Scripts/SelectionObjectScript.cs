using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionObjectScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<GameObject> collidedMachines = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collidedMachines.Add(collision.gameObject);
        collision.gameObject.GetComponent<MachineController>().ActivateSelected();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collidedMachines.Remove(collision.gameObject);
        collision.gameObject.GetComponent<MachineController>().DeactivateSelected();
    }
}

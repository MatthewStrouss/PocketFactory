using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterController : MonoBehaviour
{
    public GameObject Gold;
    public GameObject Iron;
    public GameObject Aluminum;
    public GameObject Copper;
    public GameObject Diamond;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnItems", 1.0f, 1.0f);
    }
    
    void SpawnItems()
    {
        
        Instantiate(Gold, transform.position + new Vector3(0,-0.5f,0), Quaternion.identity);
    }

    private void OnMouseDown()
    {
        print("Clicked on starter");
    }
}

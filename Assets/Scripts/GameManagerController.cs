using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    public GUIManagerController gUIManagerController;

    private static GameManagerController instance;
    public static GameManagerController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManagerController();
            }

            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

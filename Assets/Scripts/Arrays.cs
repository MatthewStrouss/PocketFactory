using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrays : MonoBehaviour
{
    public GameObject Starter;

    private void Start()
    {
        GameObject[,] grid = new GameObject[7, 3];
        for (int i = 0; i < 7; i++)
        {
            for (int y = 0; y < 3; y++)
            {
                grid[i, y] = (GameObject)Instantiate(Starter, new Vector3(i, y, -0.1f), Quaternion.identity);
                string pos = (grid[i, y].transform.position.x + "," + grid[i, y].transform.position.y);
                print(pos);
            }
        }
    }
}

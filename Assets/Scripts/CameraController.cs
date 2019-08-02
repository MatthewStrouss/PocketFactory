using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // VARIABLES
    public float panSpeed = 0.4f;

    private Vector3 mouseOrigin;
    private bool isPanning;


    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            //right click was pressed
            mouseOrigin = Input.mousePosition;
            isPanning = true;
        }


        // cancel on button release
        if (!Input.GetMouseButton(1))
        {
            isPanning = false;
        }

        //move camera on X & Y
        if (isPanning)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            // update x and y but not z
            Vector3 move = new Vector3(pos.x * panSpeed, pos.y * panSpeed, 0);

            Camera.main.transform.Translate(move, Space.Self);
        }

        // lock camera to edges
        if (Camera.main.transform.position.x < 0)
        {
            Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
        else if (Camera.main.transform.position.x > 3)
        {
            Camera.main.transform.position = new Vector3(3, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
        if (Camera.main.transform.position.y > 0)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z);
        }
        else if (Camera.main.transform.position.y < -7)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, -7, Camera.main.transform.position.z);
        }
    }
}

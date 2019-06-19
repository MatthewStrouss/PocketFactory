using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerController : MonoBehaviour
{

    public float speed = 1.0f;
    private float startTime;
    private float journeyLength;
    private bool started = false;

    public Transform startMarker;
    public Transform endMarker;

    public int rotation = 0;

    void Start()
    {
        
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(rotation == 0 || rotation == 3)
        {
            target.transform.position = new Vector2(transform.position.x, target.transform.position.y);
        }
        else if (rotation == 1|| rotation == 4)
        {
            target.transform.position = new Vector2(target.transform.position.x, transform.position.y);
        }
    }
    private void OnTriggerStay2D(Collider2D item)
    {
        if (started == false)
        {
            startTime = Time.time;
            started = true;
        }
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        item.transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
    }
}

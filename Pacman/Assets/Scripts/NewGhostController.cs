using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NewGhostController : MonoBehaviour
{
    public float speed = 3f;
    
    private Vector3 currentDirection = Vector2.up;

    private bool performWaypointCheck = true;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos += currentDirection * speed * Time.deltaTime;
        transform.position = pos;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Waypoint"))
        {
            if (performWaypointCheck == false)
                return;
            
            // Spitze - Schaft ---> Vector
            Vector3 vonMirZumWaypoint = other.transform.position - transform.position;
            float entfernung = vonMirZumWaypoint.magnitude;
            if (entfernung < 0.1f)
            {
                transform.position = other.transform.position;
                
                Waypoint waypoint = other.gameObject.GetComponent<Waypoint>();
                int numberOfPossibleDirections = waypoint.directions.Length;
                int index = Random.Range(0, numberOfPossibleDirections);
                switch (waypoint.directions[index])
                {
                    case Waypoint.Direction.Left:
                        currentDirection = Vector3.left;
                        break;
                    case Waypoint.Direction.Right:
                        currentDirection = Vector3.right;
                        break;
                    case Waypoint.Direction.Top:
                        currentDirection = Vector3.up;
                        break;
                    case Waypoint.Direction.Bottom:
                        currentDirection = Vector3.down;
                        break;
                }

                performWaypointCheck = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        performWaypointCheck = true;
    }
}

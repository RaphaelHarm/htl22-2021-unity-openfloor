using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhostController : MonoBehaviour
{
    public float speed = 3f;
    
    private Vector3 currentDirection = Vector2.up;

    private bool hasNewDirection = false;
    private Vector3 newDirection;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos += currentDirection * speed * Time.deltaTime;
        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Waypoint"))
        {
            Waypoint waypoint = other.gameObject.GetComponent<Waypoint>();
            int numberOfPossibleDirections = waypoint.directions.Length;
            int index = Random.Range(0, numberOfPossibleDirections);
            switch (waypoint.directions[index])
            {
                case Waypoint.Direction.Left:
                    newDirection = Vector3.left;
                    break;
                case Waypoint.Direction.Right:
                    newDirection = Vector3.right;
                    break;
                case Waypoint.Direction.Top:
                    newDirection = Vector3.up;
                    break;
                case Waypoint.Direction.Bottom:
                    newDirection = Vector3.down;
                    break;
            }

            hasNewDirection = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Ghost"))
        {
            newDirection = currentDirection * -1;
            hasNewDirection = true;
        }
        
        if (hasNewDirection == true)
        {
            currentDirection = newDirection;
            hasNewDirection = false;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class GhostRaycastController : MonoBehaviour
{
    public float speed = 0.3f;
    
    // Richtungsvektoren IMMER mit Länge 1!
    // Länge von einem Vektor: a*a + b*b = c*c -> c = Math.Sqrt(a*a + b*b)
    //                         x*x + y*y + z*z = c*c -> c = Math.Sqrt(x*x + y*y + z*z)
    public Vector3 direction;

    private enum State
    {
        Idle,
        Attack
    }

    private State state;
    
    private Vector3[] possibleDirections = { Vector3.right, Vector3.forward, Vector3.left, Vector3.back };
    private int directionIndex;
    
    private Color gizmoColor = Color.green;

    private const float RAYCAST_DISTANCE = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
        directionIndex = 0;
        direction = possibleDirections[directionIndex];
    }

    // Update is called once per frame
    void Update()
    {
        bool hitSomething = Physics.Raycast(transform.position, direction, RAYCAST_DISTANCE);

        if (hitSomething)
        {
            gizmoColor = Color.red;

            do
            {
                ChangeToRandomDirection();
            } while (Physics.Raycast(transform.position, direction, RAYCAST_DISTANCE));
        }
        else
        {
            gizmoColor = Color.green;
        }
        transform.position += direction * speed * Time.deltaTime;
    }

    private void ChangeDirection()
    {
        directionIndex = directionIndex + 1;
        if (directionIndex >= possibleDirections.Length)
            directionIndex = 0;

        direction = possibleDirections[directionIndex];
    }

    private void ChangeToRandomDirection()
    {
        int newDirectionIndex;

        do
        {
            newDirectionIndex = Random.Range(0, possibleDirections.Length);
        } while (directionIndex == newDirectionIndex);

        directionIndex = newDirectionIndex;
        
        direction = possibleDirections[directionIndex];
    }
    
    // o --> 
    //   (1)
    private void OnDrawGizmos()
    {
        Color oldColor = Gizmos.color;
        
        Gizmos.color = gizmoColor;
        Gizmos.DrawLine(transform.position, transform.position + direction * RAYCAST_DISTANCE);
        
        if (state == State.Attack)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.gray;

        SphereCollider collider = GetComponent<SphereCollider>();
        Gizmos.DrawWireSphere(transform.position, collider.radius);
        
        Gizmos.color = oldColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        state = State.Attack;
    }

    private void OnTriggerExit(Collider other)
    {
        state = State.Idle;
    }
}

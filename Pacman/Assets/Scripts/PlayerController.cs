using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 direction;

    public float speed = 1.5f;
    public Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.zero;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = 0f;
        float z = 0f;
        
        if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            // direction = new Vector3(1, 0, 0);
            x = 1f;
        }
        else if (Input.GetKey(KeyCode.DownArrow) == true)
        {
            // direction = new Vector3(-1, 0, 0);
            x = -1f;
        }

        if (Input.GetKey(KeyCode.LeftArrow) == true)
        {
            // direction = new Vector3(0, 0, 1);
            z = 1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            // direction = new Vector3(0, 0, -1);
            z = -1f;
        }

        direction = new Vector3(x, 0, z).normalized;
        
        // transform.position += direction * speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        // rb.AddForce(direction, ForceMode.Acceleration);
        rb.velocity = direction * speed;
    }
}

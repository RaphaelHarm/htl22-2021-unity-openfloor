using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySpriteAnimator : MonoBehaviour
{
    public Sprite[] sprites;
    public float speed = 0.25f;
    
    private int index;
    private float timePassed = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > speed)
        {
            index++;
            if (index >= sprites.Length)
                index = 0;

            GetComponent<SpriteRenderer>().sprite = sprites[index];
            timePassed = 0;
        }
    }
}

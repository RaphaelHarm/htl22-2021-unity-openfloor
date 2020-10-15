using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Texture2D cardBackground;
    private Texture2D cardForeground;

    private Texture2D currentTexture;
    
    private bool showsForeground;
    private int index;
    
    // Start is called before the first frame update
    void Start()
    {
        showsForeground = false;
        currentTexture = cardBackground;
        // cardForeground = (Texture2D)GetComponent<MeshRenderer>().material.mainTexture;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TurnCard(false);
        }
        GetComponent<MeshRenderer>().material.mainTexture = currentTexture;
    }

    public void SetForeground(Texture2D texture)
    {
        cardForeground = texture;
    }
    
    public bool TurnCard(bool forceForeground)
    {
        bool faceChanged = false;
        
        if (showsForeground && !forceForeground)
        {
            // GetComponent<MeshRenderer>().material.mainTexture = cardBackground;
            currentTexture = cardBackground;
            showsForeground = false;
            faceChanged = true;
        }
        else
        {
            if (showsForeground == false)
                faceChanged = true;
            
            // GetComponent<MeshRenderer>().material.mainTexture = cardForeground;
            currentTexture = cardForeground;
            showsForeground = true;
        }

        return faceChanged;
    }

    public void ForceBackground()
    {
        currentTexture = cardBackground;
        showsForeground = false;
    }

    public void SetTextureIndex(int textureIndex)
    {
        index = textureIndex;
    }

    public int GetTextureIndex()
    {
        return index;
    }
}

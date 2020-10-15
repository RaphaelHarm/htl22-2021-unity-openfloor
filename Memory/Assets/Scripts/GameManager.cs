using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Card[] cards;
    public Texture2D[] textures;
    public Text scoreText;
    public GameObject gameOverGO;

    private int numberOfOpenCards = 0;
    private int numberOfRemainingCards;
    
    private Card firstCard;
    private Card secondCard;

    private int score = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        SelectPicturesForCards();
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfOpenCards == 2)
            return;

        if (numberOfRemainingCards == 0)
        {
            Debug.Log("Game Over.");
            gameOverGO.SetActive(true);
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed.");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            bool didHitSomething = Physics.Raycast(ray, out hit);
            if (didHitSomething)
            {
                Card card = hit.transform.GetComponent<Card>();
                bool faceChanged = card.TurnCard(true);
                if (faceChanged)
                {
                    numberOfOpenCards++;
                    if (numberOfOpenCards == 1)
                        firstCard = card;
                    else // if (numberOfCards == 2)
                        secondCard = card;
                }

                if (numberOfOpenCards == 2)
                {
                    if (firstCard.GetTextureIndex() == secondCard.GetTextureIndex())
                    {
                        score++;
                        scoreText.text = score.ToString();
                        
                        // 2 gleiche wurden aufgedeckt
                        Invoke("RemoveCardsAndIncreaseScore", 1f);
                    }
                    else
                    {
                        // 2 unterschiedliche aufgedeckt
                        Invoke("HideAllFrontFaces", 1f);
                    }
                }
                
                Debug.Log("Mouse Pressed - Number Of Open Cards: " + numberOfOpenCards);
            }
        }
    }

    private void RemoveCardsAndIncreaseScore()
    {
        Destroy(firstCard.gameObject);
        Destroy(secondCard.gameObject);
        numberOfOpenCards = 0;
        numberOfRemainingCards -= 2;
    }
    
    private void HideAllFrontFaces()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].ForceBackground();
        }
        numberOfOpenCards = 0;
    }

    private void SelectPicturesForCards()
    {
        List<int> indices = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3 };
        numberOfRemainingCards = indices.Count;
        
        for (int i = 0; i < cards.Length; i++)
        {
            int x = Random.Range(0, indices.Count);
            int textureIndex = indices[x];
            indices.RemoveAt(x);
            
            Debug.Log($"x: {x} --> index: {textureIndex}");
            Debug.Log("Indices.Count: " + indices.Count);
            
            
            Texture2D currentTexture = textures[textureIndex];
            cards[i].SetForeground(currentTexture);
            cards[i].SetTextureIndex(textureIndex);
        }
    }
}

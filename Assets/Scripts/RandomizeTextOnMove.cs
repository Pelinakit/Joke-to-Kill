using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.UI;

public class RandomizeTextOnMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public string[] words = {"potato", "irrigation", "farmer", "tractor", "farm", "seedling", "harvest", "pesticides", "crop", "russet", "field", "sowing", "fertilizer", "mulch", "market", "barn", "fries", "fungicides", "plowing", "sustainability"};

    private Rigidbody2D rb2D;
    private Text textComponent;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        textComponent = GetComponentInChildren<Text>();

        if (textComponent == null)
        {
            Debug.LogError("Text component not found!");
        }
        else
        {
            Debug.Log("Text component found!");
        }
        RandomizeWord();
    }

    void Update()
    {
        // Move the object
        rb2D.linearVelocity = new Vector2(moveSpeed, 0f);
    }

    void RandomizeWord()
    {
        Debug.Log("RandomizeWord called.");
        if (textComponent != null && words.Length > 0)
        {
            Debug.Log("Picking random word for textComponent.");
            // Pick a random word from the array and set it as the text
            string randomWord = words[Random.Range(0, words.Length)];
            textComponent.text = randomWord;
        }
    }
}

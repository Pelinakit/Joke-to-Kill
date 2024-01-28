using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProjectileCollisionController : MonoBehaviour
{
    // Start is called before the first frame update
    private List<string> collectedWords = new List<string>();

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Projectile has hit: " + collision.gameObject.name);
        if (collision.gameObject.name == "sanakupla(Clone)")
        {
            TextMeshProUGUI textMeshPro = collision.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            if (textMeshPro != null)
            {
                collectedWords.Add(textMeshPro.text);
                Debug.Log("Collected word: " + textMeshPro.text);
                UpdateJokebox(collectedWords);
            }
            else
            {
                Debug.LogWarning("TextMeshPro component not found in the collided object.");
            }
        }
        Destroy(collision.gameObject); // This line destroys the object that was hit
        Destroy(gameObject); // This line destroys the projectile
    }
    void UpdateJokebox(List<string> words)
    {
        GameObject mainCanvas = GameObject.Find("MainCanvas");
        TextMeshProUGUI jokeText = mainCanvas.transform.Find("JokeContainer/Imagebg/JokeText").GetComponent<TextMeshProUGUI>();
        if (jokeText != null)
        {
            string joke = string.Join(" ", words.ToArray());
            jokeText.text += " " + joke;
        }
        else
        {
            Debug.LogWarning("JokeText component not found in the MainCanvas.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

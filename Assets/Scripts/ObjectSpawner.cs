using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform spawnPoint;
    public float spawnRate = 2;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpawnObject();
    }

    void Update()
    {
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            SpawnObject();
            timer = Random.Range(0f, 1.5f);
        }
    }

    void SpawnObject()
    {
        GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
        float randomY = Random.Range(0f, 4f); // Randomize y within a range, for example -5 to 5
        Vector2 randomizedSpawnPoint = new Vector2(spawnPoint.position.x, spawnPoint.position.y + randomY);
        spawnedObject.transform.position = randomizedSpawnPoint;
        // Set a random color for the spawned object
        Renderer objectRenderer = spawnedObject.GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            // Generate a random color
            Color randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f); // Ensure opacity by setting alpha to 1
            // Add white to the random color to get a pastel color
            Color pastelColor = Color.Lerp(Color.white, randomColor, 0.5f);
            // Apply the pastel color to the object
            objectRenderer.material.color = pastelColor;
        }
        else
        {
            Debug.LogWarning("Renderer component not found in the spawned object.");
        }
        // Find the TMP component within the spawned object
        Canvas canvas = spawnedObject.GetComponentInChildren<Canvas>();
        TextMeshProUGUI textMeshPro = canvas.GetComponentInChildren<TextMeshProUGUI>();

        if (textMeshPro != null)
        {
            // Change the text of the TMP component
            string[] nouns = { "potato", "irrigation", "farmer", "tractor", "farm", "seedling", "harvest", "pesticides", "crop", "russet", "field", "sowing", "fertilizer", "mulch", "market", "barn", "fries", "fungicides", "plowing", "sustainability" };
            string[] prepositions = { "above", "across", "against", "along", "among", "around", "at", "before", "behind", "below", "beneath", "beside", "between", "by", "down", "during", "for", "from", "in", "inside", "into", "near", "of", "off", "on", "out", "over", "through", "to", "under", "up", "with", "within", "without" };
            string[] adjectives = { "happy", "sad", "angry", "excited", "bored", "tired", "hungry", "thirsty", "scared", "nervous", "confused", "surprised", "cold", "hot", "sick", "healthy", "strong", "weak", "lazy", "busy" };
            string[] verbs = { "run", "jump", "swim", "fly", "eat", "drink", "sleep", "read", "write", "play", "work", "study", "sing", "dance", "laugh", "cry", "talk", "listen", "watch", "think" };
            List<string> wordsList = new List<string>();
            wordsList.AddRange(nouns);
            wordsList.AddRange(prepositions);
            wordsList.AddRange(adjectives);
            wordsList.AddRange(verbs);
            string[] words = wordsList.ToArray();
            textMeshPro.text = words[Random.Range(0, words.Length)];
        }
        else
        {
            Debug.LogWarning("TextMeshPro component not found in the spawned object.");
        }
    }



}

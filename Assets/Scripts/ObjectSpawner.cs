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
            timer = 0;
        }
    }

    void SpawnObject()
    {
        GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
        float randomY = Random.Range(0f, 4f); // Randomize y within a range, for example -5 to 5
        Vector2 randomizedSpawnPoint = new Vector2(spawnPoint.position.x, spawnPoint.position.y + randomY);
        spawnedObject.transform.position = randomizedSpawnPoint;

        // Find the TMP component within the spawned object
        Canvas canvas = spawnedObject.GetComponentInChildren<Canvas>();
        TextMeshProUGUI textMeshPro = canvas.GetComponentInChildren<TextMeshProUGUI>();

        if (textMeshPro != null)
        {
            // Change the text of the TMP component
            string[] words = { "potato", "irrigation", "farmer", "tractor", "farm", "seedling", "harvest", "pesticides", "crop", "russet", "field", "sowing", "fertilizer", "mulch", "market", "barn", "fries", "fungicides", "plowing", "sustainability" };
            textMeshPro.text = words[Random.Range(0, words.Length)];
        }
        else
        {
            Debug.LogWarning("TextMeshPro component not found in the spawned object.");
        }
    }



}

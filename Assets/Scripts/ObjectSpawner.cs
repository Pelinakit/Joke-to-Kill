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
        if (prefabToSpawn != null && spawnPoint != null)
        {
            GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);

            // Output information about all children to the debug log
            Debug.Log("Children of the spawned object:");

            for (int i = 0; i < spawnedObject.transform.childCount; i++)
            {
                Transform child = spawnedObject.transform.GetChild(i);
                Debug.Log("Child " + i + ": " + child.name);
            }

            // Optionally, find and log specific components of the children
            TextMeshProUGUI textMeshProComponent = spawnedObject.GetComponentInChildren<TextMeshProUGUI>();
            if (textMeshProComponent != null)
            {
                Debug.Log("TextMeshPro Component found in children: " + textMeshProComponent.text);
            }
            else
            {
                Debug.LogError("TextMeshPro component not found in the children of the spawned object.");
            }
        }
        else
        {
            Debug.LogError("Prefab or spawn point not set in Object Spawner script.");
        }
    }



}

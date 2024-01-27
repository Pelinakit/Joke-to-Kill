using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogError("Prefab or spawn point not set in Object Spawner script. ");
        }
    }
}

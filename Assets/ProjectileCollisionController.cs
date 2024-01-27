using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionController : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Projectile has hit: " + collision.gameObject.name);
        Destroy(collision.gameObject); // This line destroys the object that was hit
        Destroy(gameObject); // This line destroys the projectile
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

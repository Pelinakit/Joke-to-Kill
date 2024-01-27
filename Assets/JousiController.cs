using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JousiController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    // Update is called once per frame
    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        
        if (projectileRb != null)
        {
            projectileRb.AddForce(Vector2.up * projectileSpeed, ForceMode2D.Impulse);
        }
    }
}

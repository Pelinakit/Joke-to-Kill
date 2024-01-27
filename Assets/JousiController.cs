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

        // Get the mouse position in screen space
        Vector3 mousePos = Input.mousePosition;

        // Convert the mouse position to a point in the game world
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f)); // Assuming the camera is at a Z distance of 10

        // Calculate the direction from the crossbow to the mouse cursor
        Vector3 direction = worldPos - transform.position;

        // Calculate the rotation angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the crossbow to face the mouse cursor
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Optionally, rotate the tip of the crossbow independently
        // tip.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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

using UnityEngine;

public class JousiController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float offScreenBoundary = 1.0f;

    void Update()
    {
        // Get the mouse position in screen space
        Vector2 mousePos = Input.mousePosition;

        // Convert the mouse position to a point in the game world
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));

        // Calculate the direction from the crossbow to the mouse cursor
        Vector2 direction = worldPos - (Vector2)transform.position;

        // Calculate the rotation angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        // Rotate the crossbow to face the mouse cursor
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        DestroyOffScreenProjectiles();
    }

    void Shoot()
    {
        // Use the same rotation as the firePoint
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

        if (projectileRb != null)
        {
            // Calculate the shooting direction based on the rotated firePoint
            Vector2 shootDirection = new Vector2(-Mathf.Sin(Mathf.Deg2Rad * firePoint.eulerAngles.z), Mathf.Cos(Mathf.Deg2Rad * firePoint.eulerAngles.z));

            // Use the calculated direction to determine the shooting direction
            projectileRb.linearVelocity = shootDirection * projectileSpeed;
        }

        // Attach the ArrowProjectile script to the instantiated arrow
        projectile.AddComponent<ArrowProjectile>();
    }

    void DestroyOffScreenProjectiles()
    {
        // Find all projectiles currently in the scene
        foreach (var projectile in GameObject.FindGameObjectsWithTag("Projectile"))
        {
            Vector2 viewportPosition = Camera.main.WorldToViewportPoint(projectile.transform.position);

            // Check if the projectile is outside the viewport bounds
            if (viewportPosition.x < -offScreenBoundary || viewportPosition.x > 1 + offScreenBoundary ||
                viewportPosition.y < -offScreenBoundary || viewportPosition.y > 1 + offScreenBoundary)
            {
                Destroy(projectile);
            }
        }
    }

}

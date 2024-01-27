using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float deadZone = -25;

    // Start is called before the first frame update
    void Update()
    {
        MoveObject();
    }

    // Update is called once per frame
    void MoveObject()
    {
        transform.position = transform.position + Vector3.left * moveSpeed * Time.deltaTime;
         
         if (transform.position.x < deadZone)
        {
            Debug.Log("Bubble Deleted");
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionHandler : MonoBehaviour
{
    // Dead zones
    public float deadZoneTop = 7f;
    public float deadZoneBottom = -7f;
    public float deadZoneRight = 13f;
    public float deadZoneLeft = -13f;

    public float bulletSpeed = 5f;

    private float rotationAngle;

    void Start()
    {
        rotationAngle = transform.rotation.eulerAngles.z;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        switch (rotationAngle)
        {
            case 0f:
                transform.position = transform.position + (Vector3.up * bulletSpeed) * Time.deltaTime;
                break;

            case 90f:
                transform.position = transform.position + (Vector3.left * bulletSpeed) * Time.deltaTime;
                break;

            case 180f:
                transform.position = transform.position + (Vector3.down * bulletSpeed) * Time.deltaTime;
                break;

            case 270f:
                transform.position = transform.position + (Vector3.right * bulletSpeed) * Time.deltaTime;
                break;

            default:
                break;
        }

        // Destroy bullet when in dead zone
        if (gameObject.transform.position.y < deadZoneBottom ||
        gameObject.transform.position.y > deadZoneTop ||
        gameObject.transform.position.x < deadZoneLeft ||
        gameObject.transform.position.x > deadZoneRight)
        {
            Destroy(gameObject);
        }
    }
}

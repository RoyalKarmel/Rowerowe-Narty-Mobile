using UnityEngine;

public class BulletCollisionHandler : MonoBehaviour
{
    // Dead zones
    public float deadZoneVertical = 7f;
    public float deadZoneHorizontal = 13f;

    public float bulletSpeed = 7f;

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
        Vector3 moveDirection = Quaternion.Euler(0, 0, rotationAngle) * Vector3.up;
        transform.position += moveDirection * bulletSpeed * Time.deltaTime;

        // Destroy bullet when in dead zone
        if (transform.position.y < -deadZoneVertical ||
            transform.position.y > deadZoneVertical ||
            transform.position.x < -deadZoneHorizontal ||
            transform.position.x > deadZoneHorizontal
        )
        {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class PoliceMove : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float deadZoneRight = 13f;
    public float deadZoneLeft = -13f;
    private float moveSpeed;

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (spriteRenderer.flipX)
        {
            transform.position = transform.position + (Vector3.right * moveSpeed) * Time.deltaTime;
            if (transform.position.x > deadZoneRight) Destroy(gameObject);
        }
        else
        {
            transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

            if (transform.position.x < deadZoneLeft) Destroy(gameObject);
        }
    }
}

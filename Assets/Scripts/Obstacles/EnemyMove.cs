using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float deadZone = -7f;
    private float moveSpeed;

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.down * moveSpeed) * Time.deltaTime;

        if (transform.position.y < deadZone)
            Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMove : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float deadZoneRight = 13f;
    public float deadZoneLeft = -13f;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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

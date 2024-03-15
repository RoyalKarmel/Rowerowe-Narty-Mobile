using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7f;
    private SpriteRenderer spriteRenderer;
    private Vector2 touchStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                touchStartPosition = touch.position;
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = touch.deltaPosition;
                Vector2 normalizedTouchDelta = touchDeltaPosition.normalized;

                Vector3 movement = new Vector3(normalizedTouchDelta.x, normalizedTouchDelta.y, 0f) * moveSpeed * Time.deltaTime;
                transform.Translate(movement);

                if (normalizedTouchDelta.x > 0) spriteRenderer.flipX = true;
                else if (normalizedTouchDelta.x < 0) spriteRenderer.flipX = false;
            }
        }
    }
}

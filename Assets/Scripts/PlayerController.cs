using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float normalSpeed = 7f;
    public float slowDown = 4f;
    public Joystick joystick;
    public GameManager gameManager;

    private float moveSpeed = 0f;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveSpeed = normalSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource = collision.gameObject.GetComponent<AudioSource>();
        audioSource.Play();

        gameManager.gameOver();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        audioSource = other.gameObject.GetComponent<AudioSource>();
        audioSource.Play();

        if (other.gameObject.CompareTag("Puddle"))
            moveSpeed = slowDown;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Puddle"))
            moveSpeed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        Vector2 direction = new Vector2(horizontalInput, verticalInput).normalized;

        Vector3 movement = new Vector3(direction.x, direction.y, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        if (horizontalInput > 0) spriteRenderer.flipX = true;
        else if (horizontalInput < 0) spriteRenderer.flipX = false;
    }
}

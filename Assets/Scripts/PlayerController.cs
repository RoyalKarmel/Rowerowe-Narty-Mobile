using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float normalSpeed = 7f;
    public Joystick joystick;
    public GameManager gameManager;
    public BoostManager boostManager;

    private float moveSpeed = 0f;
    private bool hasShield = false;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveSpeed = normalSpeed;
    }

    // Collision with obstacles
    void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource = collision.gameObject.GetComponent<AudioSource>();
        audioSource.Play();

        if (!hasShield)
            gameManager.gameOver();
        else
        {
            Destroy(collision.gameObject);
            ToggleShield(false);
            boostManager.hideBoostUI("Shield");
        }
    }

    // Collision with triggers (boosts & puddles)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Puddle"))
        {
            audioSource = other.gameObject.GetComponent<AudioSource>();
            audioSource.Play();
            SlowDown();
        }
        else
        {
            if (other.gameObject.CompareTag("Coin"))
                gameManager.setCoins();
            else
                boostManager.boostEffect(other.gameObject.tag);

            Destroy(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Puddle"))
            SetMoveSpeed();
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

    // Movement speed
    void SlowDown()
    {
        moveSpeed /= 2;
    }
    public void SetMoveSpeed(float speed = -1)
    {
        if (speed == -1) moveSpeed = normalSpeed;
        else moveSpeed = speed;
    }

    // Toggle shield
    public void ToggleShield(bool isActive)
    {
        Debug.Log(isActive);
        hasShield = isActive;
    }
}

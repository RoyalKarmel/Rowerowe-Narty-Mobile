using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float normalSpeed = 7f;
    public Joystick joystick;
    public GameManager gameManager;
    public BoostManager boostManager;
    public Sprite[] skins;
    public SpriteRenderer spriteRenderer;

    private float moveSpeed = 0f;
    private bool hasShield = false;

    private AudioSource audioSource;

    private string selectedSkinKey = "SelectedSkinID";

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = normalSpeed;

        int selectedSkinID = PlayerPrefs.GetInt(selectedSkinKey, 0);

        if (skins != null && selectedSkinID >= 0 && selectedSkinID < skins.Length)
            spriteRenderer.sprite = skins[selectedSkinID];
        else
            Debug.LogError("Invalid skin configuration or selected skin ID: " + selectedSkinID);
    }

    // Check collision
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Puddle"))
        {
            audioSource = other.gameObject.GetComponent<AudioSource>();
            audioSource.Play();

            if (other.gameObject.CompareTag("Obstacle"))
                HandleObstacleCollision(other.gameObject);
            else
                SlowDown();
        }
        else if (other.gameObject.CompareTag("Bullet"))
            return;
        else
            HandleBoostCollision(other.gameObject);
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

    // Handle collision
    void HandleObstacleCollision(GameObject obstacle)
    {
        if (!hasShield)
            gameManager.GameOver();
        else
        {
            Destroy(obstacle);
            ToggleShield(false);
            boostManager.DisableBoost("Shield");
        }
    }

    void HandleBoostCollision(GameObject boost)
    {
        boostManager.BoostEffect(boost.tag);

        Destroy(boost);
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
        hasShield = isActive;
    }
}

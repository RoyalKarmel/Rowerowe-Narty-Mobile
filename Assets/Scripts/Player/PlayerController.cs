using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float normalSpeed = 6f;
    public float acceleration = 9f;
    public Joystick joystick;
    public Sprite[] skins;
    public SpriteRenderer spriteRenderer;

    private float moveSpeed = 0f;
    private bool hasShield = false;

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

    #region Effects
    // Movement speed
    public void SlowDown()
    {
        moveSpeed /= 2;
    }
    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    // Toggle shield
    public void SetShield(bool isActive)
    {
        hasShield = isActive;
    }

    public bool GetShield()
    {
        return hasShield;
    }
    #endregion
}

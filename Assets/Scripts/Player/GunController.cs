using UnityEngine;

public class GunController : MonoBehaviour
{
    public SpriteRenderer player;
    private SpriteRenderer spriteRenderer;
    private float initialXPosition;
    private Quaternion initialRotation;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialXPosition = transform.localPosition.x;
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        bool facingRight = !player.flipX;
        spriteRenderer.flipX = !facingRight;

        float currentXPosition = facingRight ? initialXPosition : -initialXPosition;
        float currentRotation = facingRight ? initialRotation.eulerAngles.z : -initialRotation.eulerAngles.z;

        transform.localPosition = new Vector3(currentXPosition, transform.localPosition.y, transform.localPosition.z);
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, currentRotation);
    }
}

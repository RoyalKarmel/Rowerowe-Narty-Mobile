using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public PlayerController playerController;
    public GameManager gameManager;
    public BoostManager boostManager;

    private AudioSource audioSource;

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
                playerController.SlowDown();
        }
        else if (other.gameObject.CompareTag("Bullet"))
            return;
        else
            HandleBoostCollision(other.gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Puddle"))
            playerController.SetMoveSpeed();
    }

    // Handle collision
    void HandleObstacleCollision(GameObject obstacle)
    {
        if (!playerController.GetShield())
            gameManager.GameOver();
        else
        {
            Destroy(obstacle);
            playerController.SetShield(false);
            boostManager.DisableBoost("Shield");
        }
    }

    void HandleBoostCollision(GameObject boost)
    {
        boostManager.BoostEffect(boost);

        Destroy(boost);
    }
}

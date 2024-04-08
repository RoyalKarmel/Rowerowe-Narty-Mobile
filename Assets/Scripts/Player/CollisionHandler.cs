using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public PlayerController playerController;
    public GameOver gameOver;
    public BoostManager boostManager;

    private AudioSource audioSource;

    // Check collision
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Puddle"))
        {
            audioSource = other.gameObject.GetComponent<AudioSource>();
            audioSource.Play();

            if (other.gameObject.CompareTag("Enemy"))
                HandleEnemyCollision(other.gameObject);
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
            playerController.SetMoveSpeed(playerController.normalSpeed);
    }

    // Handle collision
    void HandleEnemyCollision(GameObject enemy)
    {
        if (!playerController.GetShield())
            gameOver.GameOverScreen();
        else
        {
            Destroy(enemy);
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

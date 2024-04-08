using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            bool isPlayerOnLeft = player.transform.position.x < transform.position.x;

            GetComponent<SpriteRenderer>().flipX = isPlayerOnLeft;
        }
    }
}

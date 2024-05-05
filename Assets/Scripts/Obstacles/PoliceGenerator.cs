using UnityEngine;

public class PoliceGenerator : MonoBehaviour
{
    public Transform player;
    public GameObject[] policeGenerators;
    public GameObject policePrefab;
    public float spawnInterval = 10f;
    private float timer = 0;

    [Header("Movement variables")]
    public float policeSpeed = 3f;
    public float maxPoliceSpeed = 6f;

    void Start()
    {
        InvokeRepeating("ChangeSpeed", 15f, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnInterval) timer += Time.deltaTime;
        else
        {
            GeneratePolice();
            timer = 0;
        }
    }

    void GeneratePolice()
    {
        int randomIndex = Random.Range(0, policeGenerators.Length);
        GameObject selectedGenerator = policeGenerators[randomIndex];

        GameObject newPolice =
            Instantiate(policePrefab, new Vector3(selectedGenerator.transform.position.x, player.position.y, 0f), selectedGenerator.transform.rotation);

        SpriteRenderer policeRenderer = newPolice.GetComponent<SpriteRenderer>();
        policeRenderer.flipX = randomIndex == 0;

        PoliceMove policeMove = newPolice.GetComponent<PoliceMove>();
        if (policeMove != null)
            policeMove.SetSpeed(policeSpeed);
    }

    void ChangeSpeed()
    {
        if (policeSpeed < maxPoliceSpeed)
        {
            policeSpeed += 0.25f;
            CancelInvoke("ChangeSpeed");
        }
    }
}

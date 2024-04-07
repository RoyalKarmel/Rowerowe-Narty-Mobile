using UnityEngine;

public class PoliceGenerator : MonoBehaviour
{
    public GameObject[] policeGenerators;
    public GameObject policePrefab;
    public float spawnInterval = 10f;
    private float timer = 0;
    public float heightOffset = 3f;

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

        float minY = selectedGenerator.transform.position.y - heightOffset;
        float maxY = selectedGenerator.transform.position.y + heightOffset;

        GameObject newPolice =
            Instantiate(policePrefab, new Vector3(selectedGenerator.transform.position.x, Random.Range(minY, maxY), 0f), selectedGenerator.transform.rotation);

        SpriteRenderer policeRenderer = newPolice.GetComponent<SpriteRenderer>();
        policeRenderer.flipX = randomIndex == 0;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public List<GameObject> prefabs;
    public float spawnInterval = 5f;
    private float timer = 0;
    public float widthOffset = 10f;

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnInterval) timer += Time.deltaTime;
        else
        {
            Generate();
            timer = 0;
        }
    }

    void Generate()
    {
        int randomIndex = Random.Range(0, prefabs.Count);
        GameObject randomObject = prefabs[randomIndex];

        float minX = transform.position.x - widthOffset;
        float maxX = transform.position.x + widthOffset;

        Instantiate(randomObject, new Vector3(Random.Range(minX, maxX), transform.position.y, 0f), transform.rotation);
    }
}

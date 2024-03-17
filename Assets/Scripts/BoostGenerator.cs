using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostGenerator : MonoBehaviour
{
    public List<GameObject> boostPrefabs;
    public float spawnInterval = 5f;
    private float timer = 0;
    public float widthOffset = 10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnInterval) timer += Time.deltaTime;
        else
        {
            GenerateBoosts();
            timer = 0;
        }
    }

    void GenerateBoosts()
    {
        int randomIndex = Random.Range(0, boostPrefabs.Count);
        GameObject randomBoost = boostPrefabs[randomIndex];

        float minX = transform.position.x - widthOffset;
        float maxX = transform.position.x + widthOffset;

        Instantiate(randomBoost, new Vector3(Random.Range(minX, maxX), transform.position.y, 0f), transform.rotation);
    }
}

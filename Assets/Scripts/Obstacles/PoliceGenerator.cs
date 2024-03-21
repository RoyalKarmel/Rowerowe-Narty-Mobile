using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceGenerator : MonoBehaviour
{
    public GameObject[] policeGenerators;
    public List<GameObject> policePrefabs;
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
        GameObject selectedPolice = policePrefabs[randomIndex];

        float minY = selectedGenerator.transform.position.y - heightOffset;
        float maxY = selectedGenerator.transform.position.y + heightOffset;

        Instantiate(selectedPolice, new Vector3(selectedGenerator.transform.position.x, Random.Range(minY, maxY), 0f), selectedGenerator.transform.rotation);
    }
}

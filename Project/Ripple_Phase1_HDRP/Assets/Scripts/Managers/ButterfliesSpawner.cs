using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterfliesSpawner : MonoBehaviour
{
    //-----PUBLIC-----
    //REFERENCES
    [Header("References:")]
    public GameObject spawnPoint;
    public GameObject butterflyPrefab;

    [Space()]

    //PARAMETERS
    [Header("Parameters:")]
    public int butterfliesNumber;

    [Space()]

    public float spawnRadius;
    public bool randomRadius;
    public float minRadius;
    public float maxRadius;

    //-----PRIVATE-----
    Transform self;

    // Start is called before the first frame update
    void Start()
    {
        self = transform;
        if (randomRadius)
        {
            spawnRadius = Random.Range(minRadius, maxRadius);
        }
        SpawnButterfliesSpawner();
    }

    void SpawnButterfliesSpawner()
    {
        for (int i = 0; i < butterfliesNumber; i++)
        {
            float offsetX = Random.Range(minRadius, maxRadius);
            float offsetY = Random.Range(minRadius, maxRadius);
            float offsetZ = Random.Range(minRadius, maxRadius);
            Vector3 randomOffset = new Vector3((2 * offsetX) - offsetX, (2 * offsetY) - offsetY, (2 * offsetZ) - offsetZ);
            GameObject newSpawn = Instantiate(spawnPoint, self.position + randomOffset, Quaternion.identity, self);
            GameObject newButterfly = Instantiate(butterflyPrefab, self.position + randomOffset, Quaternion.identity, self);
            newButterfly.GetComponent<FollowAddForce>().target = newSpawn.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

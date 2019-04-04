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
			offsetX *= (int)Random.Range((int)0, (int)2) == 1 ? -1 : 1;
			print((int)Random.Range((int)0, (int)2));
            float offsetY = Random.Range(minRadius, maxRadius);
			offsetY *= (int)Random.Range((int)0, (int)2) == 1 ? -1 : 1;
			float offsetZ = Random.Range(minRadius, maxRadius);
			offsetZ *= (int)Random.Range((int)0, (int)2) == 1 ? -1 : 1;

			Vector3 randomOffset = new Vector3((2 * offsetX) - offsetX, (2 * offsetY) - offsetY, (2 * offsetZ) - offsetZ);
            GameObject newSpawn = Instantiate(spawnPoint, self.position + randomOffset, Quaternion.identity, self);
            GameObject newButterfly = Instantiate(butterflyPrefab, newSpawn.transform.position, Quaternion.identity);
            newButterfly.GetComponent<FollowAddForce>().target = newSpawn.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

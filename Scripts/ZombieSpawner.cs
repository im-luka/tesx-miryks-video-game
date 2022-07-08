using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombie;
    public List<GameObject> allZombies;
    public GameObject[] spawnPoints;
    bool isReadyToSpawn;

    void Start()
    {
        allZombies = new List<GameObject>();
        allZombies.Add(zombie);

        isReadyToSpawn = true;
    }

    void Update()
    {
        if(isReadyToSpawn && Player.isPlayerInTheWoods) {
            isReadyToSpawn = false;
            StartCoroutine(nameof(ShouldISpawn));
        }

    }

    IEnumerator ShouldISpawn() {
        if(allZombies.Count < 5) {
            SpawnZombie();
            yield return new WaitForSeconds(10f);
            isReadyToSpawn = true;
        }
    }

    void SpawnZombie() {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(zombie, spawnPoints[spawnIndex].transform.position, spawnPoints[spawnIndex].transform.rotation);
        allZombies.Add(zombie);
    }
}

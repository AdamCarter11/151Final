using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    [HideInInspector] public static bool objectSpawned;
    [SerializeField] private GameObject powerUpToSpawn;
    [SerializeField] private Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPowerUp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnPowerUp(){
        while(true){
            if(objectSpawned == false){
                Instantiate(powerUpToSpawn, spawnPoints[Random.Range(0,3)].position, Quaternion.identity);
                objectSpawned = true;
                print("Spawned shield powerUp");
            }
            yield return new WaitForSeconds(3f);
        }
    }
}

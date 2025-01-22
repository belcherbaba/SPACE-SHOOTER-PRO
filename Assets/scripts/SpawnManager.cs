using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
     [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject[] powerups;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //spawn objects every 5 seconds
    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-14f,14f),7,0);
            Instantiate(_enemyPrefab,posToSpawn,Quaternion.identity);
            yield return new WaitForSeconds(1.3f); 
        }

    }

    IEnumerator SpawnPowerupRoutine()
    {
        while(_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-10f,10f),7,0);
            int randomPowerUp = Random.Range(0,3);
            Instantiate(powerups[randomPowerUp],posToSpawn,Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3,8));


        }

    }

    private void OnPlayerDeath()
    {
        _stopSpawning = true;

    }
}

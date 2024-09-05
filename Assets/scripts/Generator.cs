using System.Collections;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] enemyPrefabs;
    public GameObject[] coinPrefabs;
    public Transform[] enemySpawnPoints;
    public Transform[] coinSpawnPoints;
    public float enemySpawnRate = 2f; // Спавн врагов раз в 2 секунды
    public float coinSpawnRate = 5f;  // Спавн монет раз в 5 секунд
    float timer1,timer2;
    [SerializeField] float acceleration;
    float koff;
    private void Start()
    {
       
    }
    public void Update()
    {
      
        if (PlayerPrefs.GetInt("koff")==1)
        {
            PlayerPrefs.SetInt("koff",0);
            koff = 0;
        }
        timer1 += Time.deltaTime;
        timer2 += Time.deltaTime;
        if (timer1 > enemySpawnRate-koff)
        {
            SpawnEnemy();
            timer1 = 0;
           
          
            
        }
        if (timer2  > coinSpawnRate-koff)
        {
            SpawnCoin();
            timer2 = 0;
            if (enemySpawnRate - koff > 0)
            {
                koff += acceleration;
            }
        }
    }

   

    private void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0 || enemySpawnPoints.Length == 0)
        {
            Debug.LogWarning("No enemy prefabs or spawn points assigned.");
            return;
        }

        // Выбираем случайный враг и точку спауна
        GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Transform spawnPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];

        Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
    }

    private void SpawnCoin()
    {
        if (coinPrefabs.Length == 0 || coinSpawnPoints.Length == 0)
        {
            Debug.LogWarning("No coin prefabs or spawn points assigned.");
            return;
        }

        // Выбираем случайную монетку и точку спауна
        GameObject coinToSpawn = coinPrefabs[Random.Range(0, coinPrefabs.Length)];
        Transform spawnPoint = coinSpawnPoints[Random.Range(0, coinSpawnPoints.Length)];

        Instantiate(coinToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}

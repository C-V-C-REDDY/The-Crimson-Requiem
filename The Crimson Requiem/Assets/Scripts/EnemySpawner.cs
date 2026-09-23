using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }
    public Transform portalSpawnPoint;
    public GameObject portalvfx;
    public WaveData[] waves;

    private int currentWave = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(SpawnWave(waves[currentWave]));
    }

    IEnumerator SpawnWave(WaveData wave)
    {
        for (int i = 0; i < wave.totalEnemyCount; i++)
        {
            SpawnEnemy(wave);
            yield return new WaitForSeconds(wave.spawnInterval);
        }
        EnemyManager.Instance.NotifyWaveFullySpawned(true);
    }

    void SpawnEnemy(WaveData wave)
    {
        Vector3 spawnPos = GetRandomSpawnPosition();
        GameObject portal = Instantiate(portalvfx, portalSpawnPoint.position, Quaternion.identity);
        Destroy(portal, 2f); // Destroy the portal effect after 2 seconds

        Enemy chosenPrefab = PickRandomPrefab(wave);

        Enemy enemyInstance = Instantiate(chosenPrefab, spawnPos, Quaternion.identity);
        EnemyManager.Instance.RegisterEnemy(enemyInstance);
    }

    Enemy PickRandomPrefab(WaveData wave)
    {
        float totalWeight = 0f;
        foreach (var entry in wave.enemyPool)
        {
            totalWeight += entry.spawnWeight;
        }

        float roll = Random.Range(0f, totalWeight);
        float sum = 0f;
        foreach (var entry in wave.enemyPool)
        {
            sum += entry.spawnWeight;
            if (roll <= sum)
            {
                return entry.enemyPrefab;
            }
        }

        return wave.enemyPool[0].enemyPrefab; // Fallback in case of rounding errors
    }
    Vector3 GetRandomSpawnPosition()
    {
        float radius = 10f;
        Vector2 randomCircle = Random.insideUnitCircle * radius;
        return portalSpawnPoint.position + new Vector3(randomCircle.x, 0f , randomCircle.y);
    }

    public void GotoNextWave()
    {
        currentWave++;
        if (currentWave >= waves.Length)
        {
            Debug.Log("All waves completed!");
            return;
        }
        StartCoroutine(SpawnWave(waves[currentWave]));
    }
}

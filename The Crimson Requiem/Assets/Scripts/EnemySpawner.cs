

/// <summary>
/// WHAT: Runs waves from WaveData: weighted random prefab picks, spawn interval, portal VFX per spawn.
/// USED BY: EnemyManager (GoToNextWave). DEPENDS ON: WaveData, Enemy prefabs, portalSpawnPoint.
/// NOTE: Wave is considered complete when all enemies are spawned AND EnemyManager.activeEnemies is empty.
/// </summary>



using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }
    public Transform portalSpawnPoint;
    public GameObject portalvfx;
    public static event System.Action OnFloodWaveFullySpawned;
    public static event System.Action OnwindUnlocked;
    public static event System.Action OnIceUnlocked;
    public static event System.Action OnLightningUnlocked;
    public static event System.Action OnFireUnlocked;
    public static event System.Action OnEarthUnlocked;
    public static event System.Action OnGalaxyUnlocked;
    public WaveData[] waves;

    private int currentWave = 0;

    void Awake()
    {
        if(Instance != null && Instance != this) { Destroy(gameObject); return;}
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(SpawnWave(waves[currentWave]));
    }

    IEnumerator SpawnWave(WaveData wave)
    {
        ToastUI.Instance.Show(wave.waveName);
        if(wave.waveNumber == 2) OnwindUnlocked?.Invoke();
        if(wave.waveNumber == 3) OnFireUnlocked?.Invoke();
        if(wave.waveNumber == 4) OnLightningUnlocked?.Invoke();
        if(wave.waveNumber == 5) OnIceUnlocked?.Invoke();
        if(wave.waveNumber == 6) OnEarthUnlocked?.Invoke();
        if(wave.waveNumber == 7) OnGalaxyUnlocked?.Invoke();

        if (wave.guaranteedSpawns != null)
        {
            foreach (var g in wave.guaranteedSpawns)
                for (int i = 0; i < g.count; i++)
                    SpawnBoss(g.enemyPrefab);
                    
        }
        for (int i = 0; i < wave.totalEnemyCount; i++)
        {
            SpawnEnemy(wave);
            yield return new WaitForSeconds(wave.spawnInterval);
        }

        EnemyManager.Instance.NotifyWaveFullySpawned(true);
        if (wave.isFloodWave) OnFloodWaveFullySpawned?.Invoke();
    }

    void SpawnEnemy(WaveData wave)
    {
        Debug.Log("Wave started, total : " + wave.totalEnemyCount + " enemies.");
        Debug.Log("Spawning enemy from wave: " + currentWave);
        Vector3 spawnPos = GetRandomSpawnPosition();
        GameObject portal = Instantiate(portalvfx, portalSpawnPoint.position, Quaternion.identity);
        Destroy(portal, 2f); // Destroy the portal effect after 2 seconds

        Enemy chosenPrefab = PickRandomPrefab(wave);

        Enemy enemyInstance = Instantiate(chosenPrefab, spawnPos, Quaternion.identity);
        EnemyManager.Instance.RegisterEnemy(enemyInstance);
    }
    
    void SpawnBoss(Enemy bossPrefab)
    {
        Debug.Log("Spawning boss: " + bossPrefab.name);
        Vector3 spawnPos = GetRandomSpawnPosition();
        GameObject portal = Instantiate(portalvfx, portalSpawnPoint.position, Quaternion.identity);
        Destroy(portal, 2f); // Destroy the portal effect after 2 seconds

        Enemy enemyInstance = Instantiate(bossPrefab, spawnPos, Quaternion.identity);
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
        AudioManager.Instance.Play(AudioManager.Instance.waveClear);
        currentWave++;
        if (currentWave >= waves.Length)
        {
            Debug.Log("All waves completed!");
            return;
        }
        StartCoroutine(SpawnWave(waves[currentWave]));
    }
}

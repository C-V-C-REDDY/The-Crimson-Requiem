using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable Objects/WaveData")]
public class WaveData : ScriptableObject
{
    [System.Serializable]
    public struct EnemySpawnEntry
    {
        public Enemy enemyPrefab;
        public float spawnWeight;
    }

    public string waveName;
    public int waveNumber;
    public int totalEnemyCount;
    public float spawnInterval;
    public bool isFloodWave = false;
    public EnemySpawnEntry[] enemyPool;
    
    }

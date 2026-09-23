using UnityEngine;
using System.Collections.Generic;
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance {get; private set;}

    private List<Enemy> activeEnemies = new List<Enemy>();
    private bool waveFullySpawned = false;

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

    public void RegisterEnemy(Enemy enemy)
    {
        activeEnemies.Add(enemy);
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        activeEnemies.Remove(enemy);
        CheckWaveCompletion();
    }

    public void NotifyWaveFullySpawned(bool value)
    {
        waveFullySpawned = true;
        Debug.Log("Wave completed! All enemies defeated.");

        CheckWaveCompletion();
    }

    private void CheckWaveCompletion()
    {
        if (waveFullySpawned && activeEnemies.Count == 0)
        {
            Debug.Log("Wave completed! All enemies defeated.");
            waveFullySpawned = false; // Reset for the next wave
            EnemySpawner.Instance.GotoNextWave();
            
        }
    }

}

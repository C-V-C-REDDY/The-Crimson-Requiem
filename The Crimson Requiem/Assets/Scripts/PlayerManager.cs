using System;
using UnityEngine;
using Unity.Collections;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public Transform playerTransform;

    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth;
    public static float speedMultiplier = 1f;

    public event Action<float, float> OnHealthChanged;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void ApplySlow(float slow, float duration)
    {
        Debug.Log("Applying slow!");
        // StartCoroutine(SlowRoutine(slow, duration));
    }

    // IEnumerator SlowRoutine(float slowMultiplier, float duration)
    // {
    //     Debug.Log("SlowApplied on player!");
    //     speedMultiplier = slowMultiplier;
    //     yield return new WaitForSeconds(duration);
    //     Debug.Log("Slow_Completed!");
    //     speedMultiplier = 1f;
    // }

    void Die()
    {
        // Handle player death (e.g., reload scene, show game over screen)
        Debug.Log("Player has died.");
    }


}

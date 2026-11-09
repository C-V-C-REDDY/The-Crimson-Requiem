

/// <summary>
/// WHAT: Singleton for Yami's position, health, TakeDamage / Die and the OnHealthChanged event.
/// USED BY: Enemy (target position, damage), HP bar UI, DamageVignetteUI (verify).
/// DEPENDS ON: Nothing. It's a standalone singleton.
/// NOTE: PlayerManager is a singleton so that enemies can always find the player, even if the player is destroyed and respawned.
/// <summary>

using System;
using System.Collections;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public bool isDead = false;

    public Transform playerTransform;

    public GameObject DebuffVFX;

    public event System.Action OnDied;

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
            isDead = false;
            currentHealth = maxHealth;
        }
        else
        {
            Destroy(gameObject);
        }
        DebuffVFX.transform.position = playerTransform.position + new Vector3(-1.5f, 0, -1.2f); // Adjust the position of the DebuffVFX to be above the player
        DebuffVFX.SetActive(false);
        
    }

    void Start()
    {
        currentHealth = maxHealth;
        BoundaryLine.Instance.OnBoundaryBroken += () => StartCoroutine(OnBoundaryBreached());
    }

    void Update()
    {
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }


    // ----- Damage Logic -----
    public void TakeDamage(float damage)
    {
        Debug.Log(currentHealth);
        AudioManager.Instance.Play(AudioManager.Instance.playerHit);
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }



// ----- Death Logic -----
    void Die()
    {
        OnDied?.Invoke();
        isDead = true;
        Debug.Log("Player has died.");
    }

// ----- Health Logic -----

private IEnumerator OnBoundaryBreached()
    {
       DebuffVFX.SetActive(true);
       DebuffVFX.transform.position = playerTransform.position + new Vector3(-1.5f, 0, -1.2f);
       while(currentHealth > 0)
       {
            TakeDamage(2f);
            yield return new WaitForSeconds(1f);
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
       }
    }



}

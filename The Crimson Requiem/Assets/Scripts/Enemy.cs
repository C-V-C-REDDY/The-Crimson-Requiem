using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : MonoBehaviour
{

    private Animator animator;
    [Header("Data")]
    public EnemyData data;
    public Slider healthBar;

    private NavMeshAgent agent;
    private Transform player;
    private float currentHealth;
    private float lastAttackTime;
    private SkinnedMeshRenderer meshRenderer;
    private Color originalColor;
    private MaterialPropertyBlock propertyBlock;
    [SerializeField] private float hitStopDuration = 0.1f; // Duration of hitstop in seconds
    [SerializeField] private float screenShakeForce = 0.1f; // Force of screen shake



    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        originalColor = meshRenderer.material.color;
        propertyBlock = new MaterialPropertyBlock();
    }
    
    void Start()
    {
        currentHealth = data.maxHealth;
        agent.speed = data.moveSpeed;
        healthBar.maxValue = data.maxHealth;
        healthBar.value = currentHealth;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
         if (distanceToPlayer > data.attackRange)
         {
            agent.SetDestination(player.position);
            animator.SetBool("IsChasing", true);
        }
        else
        {
            agent.ResetPath();
            animator.SetBool("IsChasing", false);
            TryAttack();
        }
            
        
        
    }

    void TryAttack()
    {
        if (Time.time - lastAttackTime >= data.attackCooldown)
        {
            lastAttackTime = Time.time;
            PlayerManager.Instance.TakeDamage(data.damage);
        }
    }


    public void TakeDamage(float amount)
    {

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, data.maxHealth);
        healthBar.value = currentHealth;
        JuiceManager.Instance.Hitstop(hitStopDuration);
        JuiceManager.Instance.ScreenShake(screenShakeForce);

        if (currentHealth <= 0f)
        {
            Die();
        }
        else
        {

            animator.SetTrigger("Hit");
            StartCoroutine(Flash(0.08f)); // Flash for 0.08 seconds
 // Screen shake with a force of 0.1f
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");
        agent.enabled = false;
        this.enabled = false; // Disable the Enemy script to stop further updates
        Debug.Log(data.enemyName + " has died.");
        EnemyManager.Instance.UnregisterEnemy(this);
        StartCoroutine(DestroyAfterDeath(3f)); // Destroy after 3 seconds to allow death animation to play
    }

    IEnumerator DestroyAfterDeath(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    IEnumerator Flash(float duration)
    {
        meshRenderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetColor("_Color", Color.black);
        meshRenderer.SetPropertyBlock(propertyBlock);
        yield return new WaitForSeconds(duration);
        propertyBlock.SetColor("_Color", originalColor);
        meshRenderer.SetPropertyBlock(propertyBlock);
    }
}

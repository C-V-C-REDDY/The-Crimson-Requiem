using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : MonoBehaviour
{

    private Animator animator;
    [Header("Data")]
    public EnemyData data;

    private NavMeshAgent agent;
    private Transform player;
    private float currentHealth;
    private float lastAttackTime;



    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }
    
    void Start()
    {
        currentHealth = data.maxHealth;
        agent.speed = data.moveSpeed;

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

        if (currentHealth <= 0f)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("Hit");
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");
        agent.enabled = false;
        this.enabled = false; // Disable the Enemy script to stop further updates
        Debug.Log(data.enemyName + " has died.");
        StartCoroutine(DestroyAfterDeath(3f)); // Destroy after 3 seconds to allow death animation to play
    }

    IEnumerator DestroyAfterDeath(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}

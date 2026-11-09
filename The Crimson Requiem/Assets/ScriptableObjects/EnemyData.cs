using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Identity")]
    public string enemyName;

    [Header("Stats")]
    public int maxHealth = 30;
    public float moveSpeed = 3f;
    public float damage = 10f;

    public float healthOnKill = 5f; // Amount of health the player gains when this enemy is killed

    [Header("Combat")]
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;
    public float attackWindup = 0f;

    [Header("AI")]

    public float detectionRange = 15f;    
}

using UnityEngine;

public enum HazardEffect {Damage, Slow}

[CreateAssetMenu(menuName ="HazardData")]
public class HazardData : ScriptableObject
{
    public HazardEffect effect;
    public GameObject zonePrefab;
    public LayerMask targetLayers;
    public float radius = 5f;
    public float activeDuaration = 2.5f;
    public float tickInterval = 0.5f;
    public float damage = 10f;
    public float slowFactor = 0.5f;
    public bool affectsPlayer = true;
    public bool affectsEnemies = true;
    
}

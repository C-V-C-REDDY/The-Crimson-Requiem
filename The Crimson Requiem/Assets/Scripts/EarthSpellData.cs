using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Earth")]
public class EarthSpellData : ElementalSpellData
{
    public GameObject dustPuffPrefab;
    public GameObject vortexPrefab;
    public float damageMultiplier = 1.5f;
    public float duration = 6f;
}

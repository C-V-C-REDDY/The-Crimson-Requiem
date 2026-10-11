using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Fire")]
public class FireSpellData : ElementalSpellData
{
    public GameObject fireVFXPrefab;
    public float totalDamage = 30f;
    public float burnDuration = 5f;
    public float tickInterval = 0.5f;
}

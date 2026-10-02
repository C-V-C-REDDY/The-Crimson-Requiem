using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Water")]
public class WaterSpellData : ElementalSpellData
{
    public float slowMlutiplier = 0.5f;
    public float slowDuration = 3f;
    public float fogDuration = 2f;
    public float tintDuration = 5f;
    public float tintFadeDuration = 1f;
    public Color tintColor = new Color(0.5f, 0.7f, 1f);
    public GameObject fogVFXPrefab;
    public Vector3 fogSpawnOffset;
}
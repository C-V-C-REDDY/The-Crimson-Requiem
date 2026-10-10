using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Ice")]
public class IceSpellData : ElementalSpellData
{
    public GameObject iceVFXPrefab;
    public Vector3 vfxPosition = new Vector3(5, 0, 30);
    public float vfxLifetime = 1.5f;
    public float impactDelay = 0.4f;
    public float freezeDuration = 3f;
    public Color tintColor = new Color(0.75f, 0.9f, 1f);
    public float tintDuration = 4f;
    public float tintFadeDuration = 1f;
    public float shakeforce = 0.5f;
    
}

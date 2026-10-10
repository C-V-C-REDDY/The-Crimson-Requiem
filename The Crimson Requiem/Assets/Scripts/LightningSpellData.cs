using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Lightning")]
public class LightningSpellData : ElementalSpellData
{
    public GameObject strikeVfxPrefab;
    public float damage = 20f;
    public float telegraphTime = 0.6f;
    public float maxStrikeStagger = 0.6f;
    public float shockDuration = 1f;
    public float shockMagnitute = 0.6f;
    public float shakeForce = 1.2f;
    public float shakeDuration = 1f;
    public float hitStopTime = 0.05f;
    public float deathDelay = 0.35f;
    public float strikeShake = 0.5f;
    public Color tintColor = new Color(1f, 0.93f, 0.47f);
    public Color dimColor = new Color(0.55f, 0.58f, 0.7f);
    public float tintFadeIn = 0.05f;
    public float tintHold = 0.4f;
    public float tintFadeOut = 0.6f;
    
}

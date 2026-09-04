using UnityEngine;

[CreateAssetMenu(fileName = "NewSpellData", menuName = "Scriptable Objects/Spell Data")]
public class SpellData : ScriptableObject
{
    public string spellName;
    public float cooldown;
    public float damage;
    public bool triggersEnvironmentColor = true;
    public Color environmentColor = Color.white;
    public float environmentColorDuration = 3f;
    public GameObject vfxPrefab;
}

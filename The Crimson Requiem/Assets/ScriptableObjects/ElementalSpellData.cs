using UnityEngine;

[CreateAssetMenu(fileName = "ElementalSpellData", menuName = "Scriptable Objects/ElementalSpellData")]
public abstract class ElementalSpellData : ScriptableObject
{
    public string spellName;
    public SpellTier tier;

}

public enum SpellTier {Heart, Daimond, Spade}

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementalSpellData", menuName = "Scriptable Objects/ElementalSpellData")]
public abstract class ElementalSpellData : ScriptableObject
{
    public string spellName;
    public SpellTier tier;
    public AnimationClip staffCastAnim;

    // public abstract void ApplyEffect(List<Enemy> activeEnemies);
    
}

public enum SpellTier {Heart, Daimond, Spade}

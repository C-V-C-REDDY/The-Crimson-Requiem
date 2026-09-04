using UnityEngine;

public interface ISpellEffect
{
    void Cast(Vector3 origin, Vector3 direction, SpellData data);
    
}

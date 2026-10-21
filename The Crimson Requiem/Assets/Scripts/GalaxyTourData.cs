using UnityEngine;
[CreateAssetMenu(menuName = "Spells/GalaxyTour")]
public class GalaxyTourData : ElementalSpellData
{
    public GameObject portalPrefab;
    public float countdownSeconds = 3f;
    public float sinkDuration = 1.2f;
    public float sinkDepth = 3f;
    public float damage = 100f;
    public float portalLifetime = 5;


}

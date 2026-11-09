
/// <summary>
/// WHAT: Yami's shield line. Holds the shield HP, loses HP when an enemy touches it, and
///       breaks (disables its collider) at 0 HP so later enemies can pass through to Yami.
/// USED BY: Enemy.cs (detects the touch via collider, then calls TakeDamage here), the boundary
///          HP bar and DamageVignetteUI (read the HP through its event) (verify).
/// DEPENDS ON: Nothing. It has no logic of its own for detecting enemies.
/// NOTE: Detection lives in Enemy.cs, not here. The enemy calls BoundaryLine.TakeDamage, then
///       kills itself with 999 damage (DamageSource.Boundary), so boundary kills never feed
///       SpellEconomy's charge counter.
/// </summary>

using UnityEngine;

public class BoundaryLine : MonoBehaviour
{
    public static BoundaryLine Instance { get; private set; }

    public float maxHP = 100f;
    [SerializeField] private float currentHP;
    [SerializeField] GameObject boundaryVFX; // Reference to the boundary VFX GameObject
    public bool IsBroken { get; private set; } = false;
    
    public event System.Action<float, float> OnBoundaryHPChange;

    public event System.Action OnBoundaryBroken;

    void Awake()
    {
        if(Instance != null && Instance != this) { Destroy(gameObject); return;}
        Instance = this;
    }

    public void TakeDamage(float amount)
    {
        if (IsBroken) return;
        AudioManager.Instance.Play(AudioManager.Instance.shieldHit);
        OnBoundaryHPChange?.Invoke(currentHP,maxHP);
        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0f, maxHP);
        Debug.Log("Boundary Line took " + amount + " damage. Current HP: " + currentHP);

        if(currentHP <= 0f)
        {
            Break();
        }
    }

    void Break()
    {
        IsBroken = true;
        ToastUI.Instance.Show("The boundary line is broken! Enemies can now reach Yami!");
        ToastUI.Instance.Show("Keep an eye on Yami's HEALTH!. Kill To SURVIVE!");
        GetComponent<Collider>().enabled = false;
        boundaryVFX.SetActive(false); // Disable the boundary VFX when the shield breaks
        Debug.Log("Boundary Line is broken!");
        OnBoundaryBroken?.Invoke();

        // Add any additional logic for when the boundary line is broken
    }
}

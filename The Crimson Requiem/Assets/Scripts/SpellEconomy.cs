using UnityEngine;

public class SpellEconomy : MonoBehaviour
{
    public static SpellEconomy Instance {get; private set;}

    [Header("Charge Economy")]
    [SerializeField] int killsPerCharge = 2;
    [SerializeField] int maxCharges = 3;

    [Header("Cooldown")]
    [SerializeField] float elementalGlobalCooldown = 15f;

    public int CurrentCharges {get; private set;}
    public int KillCount {get; private set;}

    float lastElementalCastTime = -999f;

    public event System.Action<int, int> OnChargeChanges;

    void Awake()
    {
        if (Instance != null && Instance != this) {Destroy(gameObject); return;}
        Instance = this;
    }

    public void OnPlayerKill()
    {
        KillCount++;
        Debug.Log("Kill Count: " + KillCount + ", Current Charges: " + CurrentCharges);
        if(KillCount % killsPerCharge == 0 && CurrentCharges < maxCharges)
        {
            CurrentCharges++;
            OnChargeChanges?.Invoke(CurrentCharges, maxCharges);
        }

    }

    public bool CanCastElemental()
    {
        return CurrentCharges > 0 && Time.time >= lastElementalCastTime + elementalGlobalCooldown;
    }

    public bool TryCastElemental()
    {
        if (!CanCastElemental()) {
            Debug.Log("Cannot cast elemental spell.");
            return false;
        }

        CurrentCharges--;
        lastElementalCastTime = Time.time;
        OnChargeChanges?.Invoke(CurrentCharges, maxCharges);
        return true;
    }

    public float CooldownRemaining()
    {
        return Mathf.Max(0f, (lastElementalCastTime + elementalGlobalCooldown) - Time.time);
        
    }

}

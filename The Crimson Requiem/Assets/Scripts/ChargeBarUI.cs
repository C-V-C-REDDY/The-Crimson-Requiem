using UnityEngine;
using UnityEngine.UI;

public class ChargeBarUI : MonoBehaviour
{
    [SerializeField] Image chargeFill;

    private SpellEconomy charge;

    private void Start()
    {
        charge = SpellEconomy.Instance;
        charge.OnChargeChanges += UpdateUI;
    }
    private void OnDestroy()
    {
        if(charge != null)
           charge.OnChargeChanges -= UpdateUI;
    }

    private void UpdateUI(float current , float max)
    {
        chargeFill.fillAmount = current / max;
    }

}

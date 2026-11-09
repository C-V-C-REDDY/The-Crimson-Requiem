using UnityEngine;
using UnityEngine.UI;

public class BoundaryBarUI : MonoBehaviour
{
 [SerializeField] Image BoundaryFill;

 private BoundaryLine BoundaryLineHp;

    private void Start()
    {
        BoundaryLineHp = BoundaryLine.Instance;
        BoundaryLineHp.OnBoundaryHPChange += UpdateUI;
    }

    private void OnDestroy()
    {
       if(BoundaryLineHp != null)
          BoundaryLineHp.OnBoundaryHPChange -= UpdateUI;       
    }

    private void UpdateUI(float current, float max)
    {
        BoundaryFill.fillAmount = current / max;
        if(current <= 0)
        {
            OnBoundaryBreached();
        }
    }

    private void OnBoundaryBreached()
    {
        BoundaryFill.fillAmount = 0f;
        ToastUI.Instance.Show("Boundary Breached! Run!!");
    }
}

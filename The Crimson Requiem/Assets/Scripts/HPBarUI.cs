using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private PlayerManager player;

    private void Start()
    {
        player = PlayerManager.Instance;
        player.OnHealthChanged += UpdateBar;
    }

    private void OnDestroy()
    {
        if(player != null)
            player.OnHealthChanged -= UpdateBar;
    }

    private void UpdateBar(float current, float max)
    {
        Debug.Log(current + "HP");
        fillImage.fillAmount = current / max;
    }
}

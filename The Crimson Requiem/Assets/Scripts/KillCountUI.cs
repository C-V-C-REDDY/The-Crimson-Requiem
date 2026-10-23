using TMPro;
using UnityEngine;

public class KillCountUI : MonoBehaviour
{
    [SerializeField] private TMP_Text label;

    private SpellEconomy kills;

    private void Start()
    {
        kills = SpellEconomy.Instance;
        kills.OnPlayerKills += UpdateLabel;
    }

    private void OnDestroy()
    {
        if(kills != null)
           kills.OnPlayerKills -= UpdateLabel;
    }

    private void UpdateLabel(int kills)
    {
        label.text = kills.ToString();
    }
}

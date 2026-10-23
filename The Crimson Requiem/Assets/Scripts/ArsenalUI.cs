using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ArsenalUI : MonoBehaviour
{
    [SerializeField] private Image waterfill;
    [SerializeField] private Image windfill;
    [SerializeField] private Image firefill;
    [SerializeField] private Image lightningfill;
    [SerializeField] private Image Icefill;
    [SerializeField] private Image Earthfill;
    [SerializeField] private Image Galaxyfill;

    private ElementalCaster elementalCaster;

    private void Start()
    {
        ElementalCaster.Instance.OnCastWater += () => StartCoroutine(UpdateUI(waterfill));
        ElementalCaster.Instance.OnCastWind += () => StartCoroutine(UpdateUI(windfill));
        ElementalCaster.Instance.OnCastFire += () => StartCoroutine(UpdateUI(firefill));
        ElementalCaster.Instance.OnCastLightning += () => StartCoroutine(UpdateUI(lightningfill));
        ElementalCaster.Instance.OnCastIce += () => StartCoroutine(UpdateUI(Icefill));
        ElementalCaster.Instance.OnCastEarth += () => StartCoroutine(UpdateUI(Earthfill));
        ElementalCaster.Instance.OnCastGalaxy += () => StartCoroutine(UpdateUI(Galaxyfill));
    }



    private IEnumerator UpdateUI(Image fill)
    {
        fill.fillAmount = 1f;
        yield return new WaitForSecondsRealtime(15f);
        fill.fillAmount = 0f;
    }
}

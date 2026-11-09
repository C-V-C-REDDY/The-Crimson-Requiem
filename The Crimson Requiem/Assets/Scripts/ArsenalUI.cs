
/// ArsenalUI: right-side spell slots, highlights on cast events from ElementalCaster.

using System;
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



    private Action castWater, castWind, castFire, castLightning, castIce, castEarth, castGalaxy;
    private Action unlockWind, unlockFire, unlockLightning, unlockIce, unlockEarth, unlockGalaxy; 
    private void Start()
    {
        // ElementalCaster.Instance.OnCastWater += () => StartCoroutine(UpdateUI(waterfill));
        // ElementalCaster.Instance.OnCastWind += () => StartCoroutine(UpdateUI(windfill));
        // ElementalCaster.Instance.OnCastFire += () => StartCoroutine(UpdateUI(firefill));
        // ElementalCaster.Instance.OnCastLightning += () => StartCoroutine(UpdateUI(lightningfill));
        // ElementalCaster.Instance.OnCastIce += () => StartCoroutine(UpdateUI(Icefill));
        // ElementalCaster.Instance.OnCastEarth += () => StartCoroutine(UpdateUI(Earthfill));
        // ElementalCaster.Instance.OnCastGalaxy += () => StartCoroutine(UpdateUI(Galaxyfill));
        // EnemySpawner.OnwindUnlocked += () => Onactivate(windfill);
        // EnemySpawner.OnFireUnlocked += () => Onactivate(firefill);
        // EnemySpawner.OnLightningUnlocked += () => Onactivate(lightningfill);
        // EnemySpawner.OnIceUnlocked += () => Onactivate(Icefill);
        // EnemySpawner.OnEarthUnlocked += () => Onactivate(Earthfill);
        // EnemySpawner.OnGalaxyUnlocked += () => Onactivate(Galaxyfill);


        castWater = () => StartCoroutine(UpdateUI(waterfill));
        castWind = () => StartCoroutine(UpdateUI(windfill));
        castFire = () => StartCoroutine(UpdateUI(firefill));
        castLightning = () => StartCoroutine(UpdateUI(lightningfill));
        castIce = () => StartCoroutine(UpdateUI(Icefill));
        castEarth = () => StartCoroutine(UpdateUI(Earthfill));
        castGalaxy = () => StartCoroutine(UpdateUI(Galaxyfill));

        ElementalCaster.Instance.OnCastWater += castWater;
        ElementalCaster.Instance.OnCastWind += castWind;
        ElementalCaster.Instance.OnCastFire += castFire;
        ElementalCaster.Instance.OnCastLightning += castLightning;
        ElementalCaster.Instance.OnCastIce += castIce;
        ElementalCaster.Instance.OnCastEarth += castEarth;
        ElementalCaster.Instance.OnCastGalaxy += castGalaxy;

        unlockWind = () => Onactivate(windfill);
        unlockFire = () => Onactivate(firefill);
        unlockLightning = () => Onactivate(lightningfill);
        unlockIce = () => Onactivate(Icefill);
        unlockEarth = () => Onactivate(Earthfill);
        unlockGalaxy = () => Onactivate(Galaxyfill);

        EnemySpawner.OnwindUnlocked += unlockWind;
        EnemySpawner.OnFireUnlocked += unlockFire;
        EnemySpawner.OnLightningUnlocked += unlockLightning;
        EnemySpawner.OnIceUnlocked += unlockIce;
        EnemySpawner.OnEarthUnlocked += unlockEarth;
        EnemySpawner.OnGalaxyUnlocked += unlockGalaxy;


    }

     void OnDestroy()
    {
        // ElementalCaster.Instance.OnCastWater -= () => StartCoroutine(UpdateUI(waterfill));
        // ElementalCaster.Instance.OnCastWind -= () => StartCoroutine(UpdateUI(windfill));
        // ElementalCaster.Instance.OnCastFire -= () => StartCoroutine(UpdateUI(firefill));
        // ElementalCaster.Instance.OnCastLightning -= () => StartCoroutine(UpdateUI(lightningfill));
        // ElementalCaster.Instance.OnCastIce -= () => StartCoroutine(UpdateUI(Icefill));
        // ElementalCaster.Instance.OnCastEarth -= () => StartCoroutine(UpdateUI(Earthfill));
        // ElementalCaster.Instance.OnCastGalaxy -= () => StartCoroutine(UpdateUI(Galaxyfill));
        // EnemySpawner.OnwindUnlocked -= () => Onactivate(windfill);
        // EnemySpawner.OnFireUnlocked -= () => Onactivate(firefill);
        // EnemySpawner.OnLightningUnlocked -= () => Onactivate(lightningfill);
        // EnemySpawner.OnIceUnlocked -= () => Onactivate(Icefill);
        // EnemySpawner.OnEarthUnlocked -= () => Onactivate(Earthfill);
        // EnemySpawner.OnGalaxyUnlocked -= () => Onactivate(Galaxyfill);



        ElementalCaster.Instance.OnCastWater -= castWater;
        ElementalCaster.Instance.OnCastWind -= castWind;
        ElementalCaster.Instance.OnCastFire -= castFire;
        ElementalCaster.Instance.OnCastLightning -= castLightning;
        ElementalCaster.Instance.OnCastIce -= castIce;
        ElementalCaster.Instance.OnCastEarth -= castEarth;
        ElementalCaster.Instance.OnCastGalaxy -= castGalaxy;

        EnemySpawner.OnwindUnlocked -= unlockWind;
        EnemySpawner.OnFireUnlocked -= unlockFire;
        EnemySpawner.OnLightningUnlocked -= unlockLightning;
        EnemySpawner.OnIceUnlocked -= unlockIce;
        EnemySpawner.OnEarthUnlocked -= unlockEarth;
        EnemySpawner.OnGalaxyUnlocked -= unlockGalaxy;
    }



    private IEnumerator UpdateUI(Image fill)
    {
        fill.fillAmount = 1f;
        yield return new WaitForSecondsRealtime(15f);
        fill.fillAmount = 0f;
    }

    private void Onactivate(Image fill)
    {
        fill.fillAmount = 0f;
    }

}

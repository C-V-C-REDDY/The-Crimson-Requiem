using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public static EndingManager Instance;
    [SerializeField] CanvasGroup fadeGroup;
    [SerializeField] float holdBeforeFade = 1.5f;
    [SerializeField] float fadeDuration = 2f;
    [SerializeField] float holdBlack = 0.5f;

    void Awake()
    {
        Instance = this;
    }

    public void StartEndingSequence() {StartCoroutine(EndSequence()); }

    IEnumerator EndSequence()
    {
        Debug.Log("Ending started. fadeGroung = " + (fadeGroup ? fadeGroup.name : "Null"));
        yield return new WaitForSecondsRealtime(holdBeforeFade);

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            fadeGroup.alpha = Mathf.Clamp01(t / fadeDuration);
            Debug.Log("alpha " + fadeGroup.alpha);
            yield return null;
        }
        Debug.Log("Fade finished, loading End");
        yield return new WaitForSecondsRealtime(holdBlack);
        SceneManager.LoadScene("End");
    }
}

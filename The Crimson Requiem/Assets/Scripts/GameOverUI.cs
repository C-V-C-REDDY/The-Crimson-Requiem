using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] float delay = 1f;


    void Start()
    {
        Time.timeScale = 1f;
        panel.SetActive(false);
        PlayerManager.Instance.OnDied += HandleDeath;
    }

    void OnDisable()
    {
      if(PlayerManager.Instance != null)
          PlayerManager.Instance.OnDied -= HandleDeath;   
    }

    void HandleDeath() => StartCoroutine(showRoutine());

    IEnumerator showRoutine()
    {
        Time.timeScale = 0.3f;
        yield return new WaitForSecondsRealtime(delay);
        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GoHome()
    {
        Debug.Log("Home Clicked");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");
    }
}

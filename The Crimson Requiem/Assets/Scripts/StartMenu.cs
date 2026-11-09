using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour
{
    public TMP_Text Label;
    public void StartGame()
    {
        Label.alpha = 1f;
        Debug.Log("Start CLicked!");
        SceneManager.LoadScene("Intro");
    } 
    public void QuitGame() => Application.Quit();
    
}

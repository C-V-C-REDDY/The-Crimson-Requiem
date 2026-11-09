using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI introText;
    [SerializeField] private float charDelay = 0.1f;
    [SerializeField] private float lineDelay = 0.4f;

    void Start()
    {
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        string[] lines =
    {
        "A Kingdom rarely falls with a roar. It falls in a hush.",
        " ",
        "Yami never wanted a crown. He only wanted peace.",
        " ",
        "Then the sky tore open.",
        " ",
        "A green portal, breathing, hungry, from a world that not ours.",
        " ",
        "What comes through does not speak. It does not stop. It only marches.",
        " ",
        "One thing stands between them and the kingdom: a shield woven from Yami's own magic.",
        " ",
        "While it holds, the kingdom lives. When it breaks, Yami stands alone.",
        " ",
        "His Dark Orb answers every call. But the elements answer only to the fallen.",
        " ",
        "Every enemy that dies awakens a little more power, and the elements wake slowly, one wave at a time.",
        " ",
        "Raise your staff, wizard. The requiem begins.",
        "",
        "",
        "LOADING..."
    };

    foreach (string line in lines)
    {
        foreach (char c in line)
        {
            introText.text += c;
            yield return new WaitForSeconds(charDelay);
        }
        introText.text += "\n";
        yield return new WaitForSeconds(lineDelay);

    }


    SceneManager.LoadScene("Main");
    

}
}

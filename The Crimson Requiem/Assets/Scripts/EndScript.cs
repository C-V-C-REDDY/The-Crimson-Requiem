using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI endText;
    [SerializeField] private float charDelay = 0.05f;
    [SerializeField] private float lineDelay = 0.4f;

    void Start()
    {
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        string[] lines =
        {
            "The portal is silent.",
            " ",
            "The sky is still.",
            " ",
            "The last shadow has sunk into the earth, and the earth keeps its secret.",
            " ",
            "Yami lowers his staff. He never wanted to be a hero. He only wanted the kingdom to see tomorrow.",
            " ",
            "The shield fades, and the stars return.",
            " ",
            "Not every requiem is a goodbye.",
            " ",
            "Some are promise.",
            " ",
            "Thank you for every orb you threw and every wave you survived.",
            " ",
            "This game was built by one student with a lot of coffee, a lot of bugs, and a lot of stubbornness.",
            " ",
            "Your time means more than you know.",
            " ",
            "CRIMSON WORLD. See you in the next one.",
            " ",
            " ",
            " ",
            "                                     -C.V.C.REDDY-     "
        };

        endText.text = "";
        foreach (string line in lines)
        {
            foreach (char c in line)
            {
                endText.text += c;
                yield return new WaitForSeconds(charDelay);
            }
            endText.text += "\n";
            yield return new WaitForSeconds(lineDelay);
        }

        
    }
    
}

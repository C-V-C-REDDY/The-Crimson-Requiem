using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
public class ToastUI : MonoBehaviour
{
    public static ToastUI Instance {get; private set;}

    [SerializeField] private TMP_Text label;
    [SerializeField] private CanvasGroup group;
    [SerializeField] private float charDelay = 0.03f;
    [SerializeField] private float holdTime = 1.5f;
    [SerializeField] private float fadeTime = 0.4f;

    private readonly Queue<String> queue = new Queue<string>();
    private bool isShowing;

    private void Awake()
    {
        Instance = this;
        group.alpha = 0f;
    }

    public void Show(string message)
    {
        queue.Enqueue(message);
        if(!isShowing) StartCoroutine(ProcessQueue());
    }

    private IEnumerator ProcessQueue()
    {
        isShowing = true;
        while(queue.Count > 0)
        {
            string msg = queue.Dequeue();
            label.text = "";
            group.alpha = 1f;

            foreach (char c in msg)
            {
                label.text += c;
                yield return new WaitForSecondsRealtime(charDelay);
            }

            yield return new WaitForSecondsRealtime(holdTime);

            float t = 0f;
            while (t < fadeTime)
            {
                t += Time.unscaledDeltaTime;
                group.alpha = 1f - t / fadeTime;
                yield return null;
            }

            group.alpha = 0f;
        }

        isShowing = false;
    }
    
}

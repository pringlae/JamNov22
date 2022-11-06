using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    [SerializeField] private float _fadeInTime = 0.5f, _fadeOutTime = 0.5f;
    [SerializeField] private CanvasGroup canvasGroup;
    
    private WaitForEndOfFrame _waiter = new WaitForEndOfFrame();

    public void FadeOutImmediate()
    {
        canvasGroup.alpha = 1f;
    }

    public void SetActive(bool value)
    {
        canvasGroup.alpha = value ? 1 : 0;
        canvasGroup.blocksRaycasts = value;
    }

    public IEnumerator FadeOut()
    {
        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= Time.deltaTime / _fadeOutTime;
            yield return _waiter;
        }
        canvasGroup.blocksRaycasts = false;
        yield break;
    }
    public IEnumerator FadeIn()
    {
        canvasGroup.blocksRaycasts = true;
        while (canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += Time.deltaTime / _fadeInTime;
            yield return _waiter;
        }
        yield break;
    }
}


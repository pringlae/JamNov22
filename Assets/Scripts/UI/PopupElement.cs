using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PopupElement : MonoBehaviour
{
    [SerializeField] private float _fadeInTime = 0.3f, _fadeOutTime = 0.3f;
    [SerializeField] private CanvasGroup canvasGroup;
    private WaitForEndOfFrame _waiter = new WaitForEndOfFrame();
    private Coroutine _switchVisibilityCoroutine = null;

    virtual public void ShowHide(bool show)
    {
        if (!gameObject.activeSelf)
        {
            canvasGroup.alpha = show ? 1 : 0;
            return;
        }

        if (_switchVisibilityCoroutine != null)
            StopCoroutine(_switchVisibilityCoroutine);
        if (show)
            _switchVisibilityCoroutine = StartCoroutine(On());
        else 
            _switchVisibilityCoroutine = StartCoroutine(Off());
    }

    private IEnumerator Off()
    {
        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= Time.deltaTime / _fadeOutTime;
            yield return _waiter;
        }
        canvasGroup.blocksRaycasts = false;
        _switchVisibilityCoroutine = null;
        yield break;
    }
    private IEnumerator On()
    {
        canvasGroup.blocksRaycasts = true;
        while (canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += Time.deltaTime / _fadeInTime;
            yield return _waiter;
        }
        _switchVisibilityCoroutine = null;
        yield break;
    }
}

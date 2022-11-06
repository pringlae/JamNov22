using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameCanvas : MonoBehaviour
{
    public static MinigameCanvas instance;

    [SerializeField] private GameObject _mainGameCanvas;
    System.Action _onClick;
    float prevCameraSize;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void Setup(System.Action onClick)
    {
        _onClick = onClick;
        prevCameraSize = Camera.main.orthographicSize;
        _mainGameCanvas.gameObject.SetActive(false);
        Map.Instance.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public void OnCloseClick()
    {
        _onClick.Invoke();
        Camera.main.orthographicSize = prevCameraSize;
        _mainGameCanvas.gameObject.SetActive(true);
        Map.Instance.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}

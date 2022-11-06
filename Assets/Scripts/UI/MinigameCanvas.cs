using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameCanvas : MonoBehaviour
{
    public static MinigameCanvas instance;

    [SerializeField] private GameObject _mainGameCanvas;
    Minigame _currentMinigame;
    float prevCameraSize;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void Setup(Minigame minigame)
    {
        _currentMinigame = minigame;
        prevCameraSize = Camera.main.orthographicSize;
        _mainGameCanvas.gameObject.SetActive(false);
        Map.Instance.gameObject.SetActive(false);
        gameObject.SetActive(true);
        minigame.gameObject.SetActive(true);
    }

    public void OnCloseClick()
    {
        _currentMinigame.Close();
        Camera.main.orthographicSize = prevCameraSize;
        _mainGameCanvas.gameObject.SetActive(true);
        Map.Instance.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
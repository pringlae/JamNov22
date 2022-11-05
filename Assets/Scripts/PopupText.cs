using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupText : PopupElement
{
    public static PopupText Instance;
    private Camera _camera;
    private TextMeshProUGUI _textMesh;
    private RectTransform _transform;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _camera = Camera.main;
        _textMesh = GetComponentInChildren<TextMeshProUGUI>();
        _transform = GetComponent<RectTransform>();
    }

    public void Setup(Transform spot, string text)
    {
        _textMesh.text = text;
        _transform.anchoredPosition = RectTransformUtility.WorldToScreenPoint(_camera, spot.position + Vector3.up * 0.5f);
        ShowHide(true);
    }

    public void Hide()
    {
        ShowHide(false);
    }
}

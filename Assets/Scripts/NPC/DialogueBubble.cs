using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueBubble : PopupElement
{
    public static DialogueBubble instance;

    //[SerializeField] private float _offsetY = 1f;
    private Camera _camera;
    private TextMeshProUGUI _textMesh;
    private RectTransform _transform;

    private void Awake()
    {
        instance = this;
        _textMesh = GetComponentInChildren<TextMeshProUGUI>();
        _transform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        _camera = Camera.main;
    }

    public void Setup(Vector3 position, string text)
    {
        _textMesh.text = text;
        _transform.anchoredPosition = RectTransformUtility.WorldToScreenPoint(_camera, position);
        ShowHide(true);
    }

    public void Hide()
    {
        ShowHide(false);
    }

}

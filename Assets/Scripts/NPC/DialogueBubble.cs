using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBubble : PopupElement
{
    public static DialogueBubble instance;

    private Camera _camera;
    private TextMeshProUGUI _textMesh;
    private RectTransform _transform;

    [SerializeField] private Sprite _commonSprite;
    [SerializeField] private Sprite _forDedSprite;
    private Image _image;

    private void Awake()
    {
        instance = this;
        _textMesh = GetComponentInChildren<TextMeshProUGUI>();
        _transform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }
    private void Start()
    {
        _camera = Camera.main;
    }

    public void Setup(Vector3 position, string text)
    {
        _image.sprite = _commonSprite;
        _textMesh.text = text;
        _transform.anchoredPosition = RectTransformUtility.WorldToScreenPoint(_camera, position);
        ShowHide(true);
    }

    public void Setup(Vector3 position, string text, float time)
    {
        _image.sprite = _commonSprite;
        _textMesh.text = text;
        _transform.anchoredPosition = RectTransformUtility.WorldToScreenPoint(_camera, position);
        ShowHide(true);
        Invoke(nameof(Hide), time);
    }

    public void Setup(Vector3 position, string text, bool isDed)
    {
        _image.sprite = isDed ? _forDedSprite : _commonSprite;

        _textMesh.text = text;
        _transform.anchoredPosition = RectTransformUtility.WorldToScreenPoint(_camera, position);
        ShowHide(true);
    }

    public void Hide()
    {
        ShowHide(false);
    }

}

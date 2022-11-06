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
    private Transform anchor;

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

    public void Setup(Transform position, string text)
    {
        _image.sprite = _commonSprite;
        _textMesh.text = text;
        anchor = position;
        ShowHide(true);
    }

    public void Setup(Transform position, string text, float time)
    {
        _image.sprite = _commonSprite;
        _textMesh.text = text;
        anchor = position;
        ShowHide(true);
        Invoke(nameof(Hide), time);
    }

    public void Setup(Transform position, string text, bool isDed)
    {
        _image.sprite = isDed ? _forDedSprite : _commonSprite;

        _textMesh.text = text;
        anchor = position;
        ShowHide(true);
    }

    private void Update()
    {
        if (canvasGroup.alpha > 0.1f)
        {
            _transform.anchoredPosition = Camera.main.WorldToScreenPoint(anchor.position) / Screen.width * 1920;
        }
    }
    public void Hide()
    {
        ShowHide(false);
    }

}

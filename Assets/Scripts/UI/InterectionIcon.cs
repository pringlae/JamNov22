using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterectionIcon : PopupElement
{
    public static InterectionIcon Instance;
    private Camera _camera;
    private RectTransform _transform;

    private Vector3 _pos;
    protected Transform _spot;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _camera = Camera.main;
        _transform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        _transform.anchoredPosition = RectTransformUtility.WorldToScreenPoint(_camera, _pos) / Screen.width * 1920;
    }
    public void Setup(Transform spot)
    {
        _spot = spot;
        _pos = spot.position + Vector3.up * 0.5f;
        _transform.anchoredPosition = RectTransformUtility.WorldToScreenPoint(_camera, _pos) / Screen.width * 1920;
        ShowHide(true);
    }

    public void Hide(Transform spot)
    {
        if (_spot == spot)
            ShowHide(false);
    }
}

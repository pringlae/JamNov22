using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    public static PopupText Instance;
    private Camera _camera;
    private TextMeshProUGUI textMesh;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _camera = Camera.main;
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        gameObject.SetActive(false);

    }

    public void Setup(Transform spot, string text)
    {
        textMesh.text = text;
        transform.position = RectTransformUtility.WorldToScreenPoint(_camera, spot.position + Vector3.up * 0.5f);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

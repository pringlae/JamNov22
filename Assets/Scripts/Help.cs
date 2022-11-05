using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Help : MonoBehaviour
{
    public static Help instance;
    [SerializeField] private GameObject _gameobject;
    [SerializeField] private TextMeshProUGUI _textMesh;
    [SerializeField] private string[] _hints;

    private void Awake()
    {
        instance = this;

        _textMesh.text = _hints[0];
    }

    public void Show()
    {
        _gameobject.SetActive(true);
    }

    public void Hide()
    {
        _gameobject.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Help : PopupElement
{
    public static Help instance;
    [SerializeField] private TextMeshProUGUI _textMesh;
    private string[] _hints;
    private int _hintIndex = 0;

    private void Awake()
    {
        instance = this;
    }

    public void SetHints(params string[] hints)
    {
        _hints = hints;
        _hintIndex = 0;
    }

    public void Show()
    {
        _textMesh.text = _hints[_hintIndex];
        _hintIndex = (_hintIndex + 1) % _hints.Length;
        ShowHide(true);
    }

    public void Hide()
    {
        ShowHide(false);
    }

}

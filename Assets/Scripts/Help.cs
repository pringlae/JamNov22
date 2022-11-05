using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Help : PopupElement
{
    public static Help instance;
    [SerializeField] private TextMeshProUGUI _textMesh;
    [SerializeField] private string[] _hints;

    private void Awake()
    {
        instance = this;

        _textMesh.text = _hints[0];
    }

    public void Show()
    {
        ShowHide(true);
    }

    public void Hide()
    {
        ShowHide(false);
    }

}

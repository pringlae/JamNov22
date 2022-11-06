using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Lock : Minigame
{
    private int[] _numbers = new int[3];
    [SerializeField] private string _numberStandard;
    [SerializeField] private TextMeshProUGUI[] _numberLabels;

    public void OnNumberButtonClickUp(int index)
    {
        _numbers[index]++;
        if (_numbers[index] > 9) _numbers[index] = 0;
        _numberLabels[index].text = _numbers[index].ToString();

        OnNumberChanged();
    }

    public void OnNumberButtonClickDown(int index)
    {
        _numbers[index]--;
        if (_numbers[index] < 0) _numbers[index] = 9;
        _numberLabels[index].text = _numbers[index].ToString();

        OnNumberChanged();
    }

    private void OnNumberChanged()
    {
        var currentNumber = string.Join("", _numbers);
        if (currentNumber == _numberStandard)
        {
            _success = true;
            MinigameCanvas.instance.OnCloseClick();
        }
    }
}

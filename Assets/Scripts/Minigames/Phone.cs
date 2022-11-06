using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Phone : Minigame
{
    [SerializeField] private TextMeshProUGUI _numberLabel;
    [SerializeField] private string _numberStandard;
    protected System.Action<bool, string> _onPnoneCompleted;

    private string _currentNumber, _bubbleText;

    protected override void StartMinigame()
    {
        base.StartMinigame();
        _currentNumber = "";
        _bubbleText = "";
    }

    public virtual void Launch(System.Action<bool, string> onCompleted)
    {
        var instance = Object.Instantiate(this, MinigameCanvas.instance.transform);
        instance._onPnoneCompleted = onCompleted;
        instance.transform.SetAsFirstSibling();
        instance.StartMinigame();
    }

    public void OnNumberButtonClick(string value)
    {
        _currentNumber += value;
        _numberLabel.text = _currentNumber;

        if (_currentNumber.Length == _numberStandard.Length)
        {
            if (_currentNumber == _numberStandard)
            {
                if (QuestSystem.IsEventCompleted("взял купон"))
                {
                    _success = true;
                    _bubbleText = "Ждите доставку!";
                }
                else
                {
                    _bubbleText = "Требуется купон!";
                }
            }
            else
            {
                _bubbleText = "Хватить баловаться!";
            }

            MinigameCanvas.instance.OnCloseClick();
        }
    }

    public override void Close()
    {
        _onPnoneCompleted.Invoke(_success, _bubbleText);
        Player.instance.gameObject.SetActive(true);
        Destroy(gameObject);
    }

}

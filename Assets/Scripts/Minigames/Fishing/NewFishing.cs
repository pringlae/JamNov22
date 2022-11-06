using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewFishing : Minigame
{
    [SerializeField] private GameObject _hook;
    [SerializeField] private Text _waitingText, _pressText;
    [SerializeField] private Vector2 _waitingRange;
    [SerializeField] private float _fishForce, _playerForce, _balanceRange;
    [SerializeField] private Slider _fishActionSlider;
    [SerializeField] private Animator _animator;

    private float _waitTime, _changeTime;
    private float _balance;
    private string _currentButton;

    protected override void StartMinigame()
    {
        base.StartMinigame();
        _waitTime = Random.Range(_waitingRange.x, _waitingRange.y);
        _fishActionSlider.gameObject.SetActive(false);
        _waitingText.gameObject.SetActive(true);
        _balance = 0;
    }

    void Update()
    {
        if (_waitTime > 0)
        {
            _waitTime -= Time.deltaTime;
            if (_waitTime <= 0)
            {
                _fishActionSlider.gameObject.SetActive(true);
                _waitingText.gameObject.SetActive(false);
                _animator.SetTrigger("fight");
                _changeTime = -1;
            }
        }
        else
        {
            _changeTime -= Time.deltaTime;
            if (_changeTime <= 0)
            {
                switch(Random.Range(0, 4))
                {
                    case 0: _currentButton = "Up"; _pressText.text = "ЖМИ ВВЕРХ!"; break;
                    case 1: _currentButton = "Left"; _pressText.text = "ЖМИ ВЛЕВО!"; break;
                    case 2: _currentButton = "Right"; _pressText.text = "ЖМИ ВПРАВО!"; break;
                    case 3: _currentButton = "Down"; _pressText.text = "ЖМИ ВНИЗ!"; break;
                }
                _changeTime = Random.value * 3 + 3;
            }
            if (Input.GetButtonDown(_currentButton))
                _balance += _playerForce;
            _balance -= _fishForce * Time.deltaTime * (Random.value + 1);

            _fishActionSlider.value = (_balance / _balanceRange + 1) / 2;
            if (Mathf.Abs(_balance) > _balanceRange)
            {
                enabled = false;
                _fishActionSlider.gameObject.SetActive(false);
                _waitingText.gameObject.SetActive(true);
                if (_balance > 0)
                {
                    _animator.SetTrigger("player_wins");
                    _success = true;
                    _waitingText.text = "Вы поймали рыбу! Вот это да! Ваш дед бы вами гордился...";
                }
                else
                {
                    _animator.SetTrigger("fish_wins");
                    _waitingText.text = "Мдааааа...";
                }
            }
        }
    }
}

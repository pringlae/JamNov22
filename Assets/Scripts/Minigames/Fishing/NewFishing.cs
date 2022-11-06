using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewFishing : Minigame
{
    [SerializeField] private GameObject _hook, _waitingText;
    [SerializeField] private Vector2 _waitingRange;
    [SerializeField] private float _fishForce, _playerForce, _balanceRange;
    [SerializeField] private Slider _fishActionSlider;
    [SerializeField] private Animator _animator;

    private float _waitTime;
    private float _balance;

    void Start()
    {
        _waitTime = Random.Range(_waitingRange.x, _waitingRange.y);
        _fishActionSlider.gameObject.SetActive(false);
        _waitingText.gameObject.SetActive(true);
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
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
                _balance += _playerForce;
            _balance -= _fishForce * Time.deltaTime * (Random.value + 1);

            _fishActionSlider.value = (_balance / _balanceRange + 1) / 2;
            if (Mathf.Abs(_balance) > _balanceRange)
            {
                enabled = false;
                _fishActionSlider.gameObject.SetActive(false);
                if (_balance > 0)
                {
                    _animator.SetTrigger("player_wins");
                    _success = true;
                }
                else
                    _animator.SetTrigger("fish_wins");
            }
        }
    }
}

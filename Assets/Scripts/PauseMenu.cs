using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    static public PauseMenu instance;

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private GameObject _gameobject;

    private void Awake()
    {
        instance = this;
    }

    public void ShowHide()
    {
        _gameobject.SetActive(!_gameobject.activeSelf);
        Player.instance.enabled = !_gameobject.activeSelf;
    }

    public void Continue()
    {
        _gameobject.SetActive(false);
        Player.instance.enabled = true;
    }

    public void MenuPressed()
    {
        SceneManager.LoadScene(0);
    }

    public void setVolume(float volume)
    {
        _audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    public void Sound()
    {
        AudioListener.pause = !AudioListener.pause;
    }
}

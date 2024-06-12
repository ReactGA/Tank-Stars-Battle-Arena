using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {
    public AudioListener _listener;
    public FadeCamera _fadeEffect;
    public GameObject _menuPanel;
    public int _mainMenuId;

    private bool _canLoad;

    void Update()
    {
        if(_canLoad)
        {
            if (_fadeEffect.opacity < 0.1f)
                SceneManager.LoadScene(_mainMenuId);
        }
    }

    public void Play()
    {
        _menuPanel.SetActive(false);
        Time.timeScale = 1;
        _listener.enabled = true;
    }

    public void Menu()
    {
        _menuPanel.SetActive(false);
        Time.timeScale = 1;
        _fadeEffect.FadeOut();
        _canLoad = true;
    }

    public void Pause()
    {
        _menuPanel.SetActive(true);
        Time.timeScale = 0;
        _listener.enabled = false;
    }
}

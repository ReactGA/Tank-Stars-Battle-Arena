using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public int _levelId;
    public FadeCamera _fadeEffect;
    public AudioSource _src;
    public AudioClip _buttonSound;

    private bool _canLoad;

    [ContextMenu("Reset Save")]
    public void ResetSave()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    void Start()
    {
        _fadeEffect.FadeIn();
        if (PlayerPrefs.HasKey("Level"))
        {
            _levelId = PlayerPrefs.GetInt("Level");
            PlayerPrefs.Save();
        }
        else
            _levelId = 1;

    }

    void Update()
    {
        if(_canLoad)
        {
            if (_fadeEffect.opacity < 0.1f)
            {
                SceneManager.LoadScene(_levelId);
            }
        }
    }

    public void Race()
    {
        _src.PlayOneShot(_buttonSound);
        _canLoad = true;
        _fadeEffect.FadeOut();
    }

    public void Quit()
    {
        _src.PlayOneShot(_buttonSound);
        Application.Quit();
    }
}

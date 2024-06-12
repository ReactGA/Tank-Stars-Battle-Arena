using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour {
    public int _nextLevelId;

    private FadeCamera _cameraFadeEffect;
    private bool _isFinished;

    [ContextMenu("Reset Save")]
    public void ResetSave()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    void Update()
    {
        if(_isFinished)
        {
            if (_cameraFadeEffect.opacity < 0.1f)
                SceneManager.LoadScene(_nextLevelId);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if(_cameraFadeEffect = GameObject.FindObjectOfType<FadeCamera>())
            {
                _cameraFadeEffect.FadeOut();
            }
            _isFinished = true;
            PlayerPrefs.SetInt("Level", _nextLevelId);
            PlayerPrefs.Save();
        }
    }
}

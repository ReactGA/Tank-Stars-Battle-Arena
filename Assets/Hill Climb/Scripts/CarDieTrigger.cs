using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarDieTrigger : MonoBehaviour {
    public CarController _car;
    public MobileCarController _carM;
    private bool _flip;
    private float _timer;
    public Text _warningFlipText;

    void Update()
    {
        if (_car != null) _car._isFliped = _flip;
        if (_carM != null) _carM._isFliped = _flip;
        if (_flip)
        {
            if (_timer < 1f)
            {
                

            }
            else {

                _timer += Time.deltaTime;
            
                _warningFlipText.color = new Color(_warningFlipText.color.r, _warningFlipText.color.g, _warningFlipText.color.b,
                              Mathf.MoveTowards(_warningFlipText.color.a, 0, Time.deltaTime));

                            
            }
            

        }
        if (!_flip)
            _timer = 0;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Ground"))
        {
            _flip = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            _flip = false;
        }
    }
}

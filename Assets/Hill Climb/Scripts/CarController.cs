using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(WheelJoint2D))]
public class CarController : MonoBehaviour
{
    [Header("Stats")]
    public float _speed;
    public float _rotationSpeed;
    public float _fuel;
    public float _fuelConsumption;

    [Header("Sounds")]
    public AudioClip _engineSound;
    public AudioClip _impactSound;
    public AudioSource _srcEngine;
    public AudioSource _srcImpact;

    [Header("UI")]
    public Image _fuelImage;
    public Text _lowFuelText;
    public Text _warningFlipText;

    [Header("Particles")]
    public ParticleSystem _impact;

    private Rigidbody2D _rig;
    private WheelJoint2D[] _wheels;
    [HideInInspector]public bool _isFliped;

    ParticleSystem dirt;

  public  GameObject Panel;

  public GameObject Canvas2;
    
    public void AddFuel(float fuelCount)
    {
        _fuel += fuelCount;
        _fuel = Mathf.Clamp(_fuel, 0, 100);
    }

	void Start ()
    {
        _rig = gameObject.GetComponent<Rigidbody2D>();
        _wheels = _rig.gameObject.GetComponents<WheelJoint2D>();
        _srcEngine.PlayOneShot(_engineSound);
     
    }

    void OnCollisionEnter2D(Collision2D col)
    {
      //  if (!_srcImpact.isPlaying) _srcImpact.PlayOneShot(_impactSound);
        _impact.transform.position = col.contacts[0].point;
        _impact.Play();

 
    }


 bool _isGameOver = false;

int count_gameover = 0;

int count_gamestart = 0;

	void Update ()
    {

if(count_gamestart < 50)
{
    count_gamestart++;
}else
{
 if (Canvas2 != null)
                Canvas2.SetActive(true);
}

 

           //Flip
           if (_isFliped)
           {
               _warningFlipText.color = new Color(_warningFlipText.color.r, _warningFlipText.color.g, _warningFlipText.color.b,
               Mathf.MoveTowards(_warningFlipText.color.a, 1, Time.deltaTime));

 
            _isGameOver = true;

    
           }
           else
           {
               _warningFlipText.color = new Color(_warningFlipText.color.r, _warningFlipText.color.g, _warningFlipText.color.b,
               Mathf.MoveTowards(_warningFlipText.color.a, 0, Time.deltaTime));
  
           }

if(_isGameOver)
{
    
    count_gameover++; Time.timeScale *= 0.99f; 

 _warningFlipText.color = new Color(_warningFlipText.color.r, _warningFlipText.color.g, _warningFlipText.color.b,
               Mathf.MoveTowards(_warningFlipText.color.a, 2, Time.deltaTime));


 

}

 

      if(!_isGameOver)
    {


           if (_fuel <= 0) _srcEngine.mute = true;
           if (_fuel > 0) _srcEngine.mute = false;

           //Fuel system
           _fuel -= Time.deltaTime * 0.4f + Mathf.Abs(Input.GetAxis("Horizontal")) * _fuelConsumption/10;
           _fuel = Mathf.Clamp(_fuel, -1, 100);
           _fuelImage.fillAmount = _fuel/100;
           if(_fuel < 0 && Mathf.Abs(_rig.velocity.x) < 0.1f)
            SceneManager.LoadScene("Menu");
           if (_fuel < 20) _lowFuelText.color = new Color(_lowFuelText.color.r, _lowFuelText.color.g, _lowFuelText.color.b, 
               Mathf.MoveTowards(_lowFuelText.color.a, 1, Time.deltaTime));
           else _lowFuelText.color = new Color(_lowFuelText.color.r, _lowFuelText.color.g, _lowFuelText.color.b,
               Mathf.MoveTowards(_lowFuelText.color.a, 0, Time.deltaTime));

           //Moving
           foreach (WheelJoint2D _w in _wheels)
           {
               if (_fuel > 0)
               {
                   var motor = _w.motor;
                   motor.motorSpeed = -Input.GetAxis("Horizontal") * _speed;
                   _w.motor = motor;

                   //Engine sound (NEW VERSION)
                   float _pitch = Mathf.Abs(_w.jointSpeed * Input.GetAxis("Horizontal") * 0.0015f);
                   _pitch = Mathf.Clamp(_pitch, 0.95f, 1.7f);
                
                   _srcEngine.pitch = Mathf.MoveTowards(_srcEngine.pitch, _pitch, Time.deltaTime*0.4f);
               }

           }

           if (Mathf.Abs(Input.GetAxis("Horizontal")) <= 0.4f)
           {
               foreach (WheelJoint2D _w in _wheels)
                   _w.useMotor = false;
           }

           //Rotation
          if (Input.GetAxis("Horizontal") != 0)
           {
               _rig.AddTorque(-Input.GetAxis("Horizontal") * _rotationSpeed);
           }

     }   

	}

    


}

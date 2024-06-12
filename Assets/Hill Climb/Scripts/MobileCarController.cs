
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MobileCarController : MonoBehaviour {



    [Header("Stats")]
    public float _speed;
    public float _rotationSpeed;
    public float _fuel;
    public float _fuelConsumption;

    [Header("Sounds")]
    //public AudioClip _engineSound;
    public AudioClip _impactSound;
   // public AudioSource _srcEngine;
    public AudioSource _srcImpact;

    [Header("UI")]
    public Image _fuelImage;
    public Text _lowFuelText;
    public Text _warningFlipText;

    [Header("Particles")]
    public ParticleSystem _impact;

    private Rigidbody2D _rig;
    private WheelJoint2D[] _wheels;
    private Vector2 _input;

    //[HideInInspector]
    public bool _isFliped;

    public  GameObject Panel;

    public GameObject ControlCanvas;


    public GameObject GameOverCanvas,endMenu;


    int countstart=0;

    bool GameOver = false;

    public GameObject gun1,man1,deadman1;

    public GameObject outoffuelOBJ;


    //  public GameObject playerRG;
    public EnemyWeapon enemyWeapon;


    int timer=0;

  

    public void AddFuel(float fuelCount)
    {
        _fuel += fuelCount;
        _fuel = Mathf.Clamp(_fuel, 0, 100);
    }

    void Start()
    {
        _rig = gameObject.GetComponent<Rigidbody2D>();

        _wheels = _rig.gameObject.GetComponents<WheelJoint2D>();

        enemyWeapon = new EnemyWeapon();
         
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        

  



            if (col.gameObject.tag == "eme")
            {


                Destroy(col.gameObject);

                 
               _fuel = -80;

                FindObjectOfType<AudioManager>().play("hit1");


            }

 

    }

    //MobileInput
    public void SetVerticalInput(int _value)
    {
        _input = new Vector2(_value, _input.y);

        _rig.AddForce(new Vector2(0,500));

       FindObjectOfType<AudioManager>().play("raise"); 
    }

    public void SetHorizontalInput(int _value)
    {
        _input = new Vector2(_input.x, _value);
         
    }

bool nextpage = false;

 public void closepanel1()
{

nextpage = true;

if(GameOverCanvas != null)
GameOverCanvas.SetActive(false);
if(ControlCanvas != null)
ControlCanvas.SetActive(false);
 if(endMenu != null)
 endMenu.SetActive(true);

        FindObjectOfType<AudioManager>().play("ButtonClick");

    }

    int countdie = 0;
 

    void Update()
    {

        countdie++;
        
     




if (!GameOver)
{

    if (countstart < 250)
    {   
    countstart++;
    }
            else if(countstart == 250)
    {
    if(ControlCanvas != null)
    ControlCanvas.SetActive(true);
 
    }
} 


 


    if(GameOver && !nextpage)
    {
            if (ControlCanvas != null)
                ControlCanvas.SetActive(false);

             if(GameOverCanvas != null)
            GameOverCanvas.SetActive(true);

       
    }



 

        //Flip
        if (_isFliped && !nextpage)
        {

 

            
            
          //  FindObjectOfType<AudioManager>().play("menu_song");



            if (timer >10)
{
         
            _warningFlipText.color = new Color(_warningFlipText.color.r, _warningFlipText.color.g, _warningFlipText.color.b,
            Mathf.MoveTowards(_warningFlipText.color.a, 2, Time.deltaTime));

            
            ControlCanvas.SetActive(false);

             if(GameOverCanvas != null)
            GameOverCanvas.SetActive(true);
              

            GameOver = true;


            if(gun1 != null)
    gun1.SetActive(false);


  if(deadman1 != null)
    {

                   

                    deadman1.SetActive(true);
                     
                    Vector3 pos = new Vector3(man1.transform.position.x , deadman1.transform.position.y, deadman1.transform.position.z);

                    deadman1.transform.position = pos;

                    FindObjectOfType<AudioManager>().play("screem");

                }


    if(true)

                {

                    man1.SetActive(true);

                    FindObjectOfType<AudioManager>().play("screem");

                    enemyWeapon.putoffbtn = false;
                }
   
  


}
else
{
    timer++;
}


        }
        else
        {
            timer=0;

            if(!GameOver)
            _warningFlipText.color = new Color(_warningFlipText.color.r, _warningFlipText.color.g, _warningFlipText.color.b,
            Mathf.MoveTowards(_warningFlipText.color.a, 0, Time.deltaTime));
        }

       
      //  if (_fuel <= 0) _srcEngine.mute = true;
     
       // if (_fuel > 0) _srcEngine.mute = false;

        //Fuel system
        _fuel -= Time.deltaTime * 0.4f + Mathf.Abs(_input.x) * _fuelConsumption / 10;
        _fuel = Mathf.Clamp(_fuel, -1, 100);
        _fuelImage.fillAmount = _fuel / 100;

        if (_fuel > 3 )
        {
           // outoffuelOBJ.SetActive(false);
        }

            if (_fuel > 0 && _fuel < 2 && Mathf.Abs(_rig.velocity.x) < 0.1f)
        {
            if (outoffuelOBJ != null)
                outoffuelOBJ.SetActive(true);
        }

        if (_fuel < 0 )
        {
            outoffuelOBJ.SetActive(false);

            //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            GameOver = true;
            _isFliped = true;

            if (GameOver)
            {
                ControlCanvas.SetActive(false);

                if (GameOverCanvas != null)
                    GameOverCanvas.SetActive(true);

                man1.SetActive(true);

                FindObjectOfType<AudioManager>().play("screem");

                enemyWeapon.putoffbtn = false;

            }

        }
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
                motor.motorSpeed = -_input.x * _speed;
                _w.motor = motor;

                //Engine sound (NEW VERSION)
                float _pitch = Mathf.Abs(_w.jointSpeed * Input.GetAxis("Vertical") * 0.0015f);
                _pitch = Mathf.Clamp(_pitch, 0.95f, 1.7f);
               // _srcEngine.pitch = Mathf.MoveTowards(_srcEngine.pitch, _pitch, Time.deltaTime * 0.4f);
            }

            
        }

        if (_input.x == 0)
        {
            foreach (WheelJoint2D _w in _wheels)
                _w.useMotor = false;
        }

        //Rotation
        if (_input.y != 0)
        {
            _rig.AddTorque(-_input.y * _rotationSpeed);
        }
    }





    public void Replay()
    {

      
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);


        //FindObjectOfType<AudioManager>().play("ButtonClick");


    }





}

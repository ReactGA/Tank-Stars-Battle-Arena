
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy_Movement : MonoBehaviour
{

    public float hitcount = 150;

    [Header("Stats")]
    public float _speed;
    public float _rotationSpeed;
    public float _fuel;
    public float _fuelConsumption;

    [Header("UI")]
    public Image _fuelImage;
    public Image _fuelImage2;
    public Image _fuelImage3;
    public Image _fuelImage4;
    public GameObject boos1;

    public GameObject die;

    public GameObject gun1;

    public GameObject Sparks;

    public GameObject Fire;

    public GameObject Fire1;

    public GameObject playagain;

    bool startCount = false;

    int count_win = 0;

    int score = 0;

    int count_Enemy1_Hit = 0;

    public GameObject ControlPanel;

    public GameObject win;

    public GameObject win2;

    public GameObject BadgeImg;

    public Text coincountTXT;

    int totalcoin = 0;

    public int counttotalcoin = 0;

    bool GameOver = false;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject star4;
    public GameObject panel1;

    public GameObject ground1;
    public GameObject ground2;

    public GameObject ground3;


    public GameObject purple1;
    public GameObject purple2;
    public GameObject purple3;
    public GameObject purple4;

    public GameObject Win3;

    public GameObject vs;

    float x, y;

    public Text cointxt;

    int countpurplecoins = 0;
    public Text purpletxt;
    public Text purpletxt2;




    public Rigidbody2D _rig;
    private WheelJoint2D[] _wheels;
    private Vector2 _input;
    [SerializeField]Transform SpawnedParent;

    EnemyWeapon enemyWeapon;
    AdsCaller adsCaller;

    public void AddFuel(float fuelCount)
    {
        _fuel += fuelCount;
        _fuel = Mathf.Clamp(_fuel, 0, 100);
    }

    float stars = 0;
    AudioManager audioManager;

    void Start()
    {
        upgradeCount = PlayerPrefs.GetFloat("upgrade");

        _fuelImage3.fillAmount = upgradeCount;

        counttotalcoin = totalcoin = 0;// PlayerPrefs.GetInt("totalcoin");

        cointxt.text = "" + totalcoin;

        stars = 0; //PlayerPrefs.GetFloat("star");

        _fuelImage2.fillAmount = stars;

        countpurplecoins2 = countpurplecoins = PlayerPrefs.GetInt("purplecoin");


        _rig = gameObject.GetComponent<Rigidbody2D>();

        _wheels = _rig.gameObject.GetComponents<WheelJoint2D>();


        _rig.AddForce(new Vector2(-500, 0));

        enemyWeapon = new EnemyWeapon();

        adsCaller = gameObject.AddComponent<AdsCaller>();
        adsCaller.ShowBannerAds();

        audioManager = FindObjectOfType<AudioManager>();

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "bull")
        {

            GameObject explode3 = Instantiate(Sparks, transform.position, transform.rotation);
            explode3.transform.parent = SpawnedParent;
            if (explode3 != null)
                Destroy(explode3, 2);
            if (col.gameObject != null)
                Destroy(col.gameObject);
            hitcount = 0;

            count_Enemy1_Hit++;



            if (count_Enemy1_Hit == 3)
            {
                var g = Instantiate(Fire, transform.position, transform.rotation);
                var g2 = Instantiate(Fire1, transform.position, transform.rotation);
                
                g.transform.parent = SpawnedParent;
                g2.transform.parent = SpawnedParent;

                audioManager.play("die4");

                var g3 = Instantiate(Sparks, boos1.transform.position, boos1.transform.rotation);
                g3.transform.parent = SpawnedParent;
                
                if (die != null)
                    die.SetActive(true);

                if (boos1 != null)
                    Destroy(boos1);
                if (gun1 != null)
                    Destroy(gun1);
                hitcount = 0;
                score++;
                enemyWeapon.putoffbtn = false;


                totalcoin += 50;

                countpurplecoins += 5;

                PlayerPrefs.SetInt("totalcoin", totalcoin);

                PlayerPrefs.SetInt("purplecoin", countpurplecoins);

                cointxt.text = "" + totalcoin;
            }



        }






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


    public void SetVerticalInput(int _value)
    {
        _input = new Vector2(_value, _input.y);

        //  _rig.AddForce(new Vector2(0, 500));
    }

    public void SetHorizontalInput(int _value)
    {
        _input = new Vector2(_input.x, _value);

        // 
    }
    int progressbadge = 0;

    Vector2 posbadge;

    int delaycoincount = 0;

    bool finding;

    int countFinding = 0;

    float upgradeCount = 0;

    int countpurplecoins2 = 0;

    int delaycountpurplecoins = 0;

    public GameObject badgeIMG;

    public GameObject movebadgeIMG;

    public GameObject GreenBTN;


    public GameObject Acc;

    public GameObject dcc;

    public GameObject bull;

    public GameObject firstpanel;

    public GameObject playerhealth;

    int count = 0;
    public GameObject optionMenu;
    void Update()
    {

        if (finding)
        {


            count++;


            if (count >= 1000)
                Replay();


        }


        //if (_isFliped)
        //{
        //if (_input.x == 0)
        //{
        //    foreach (WheelJoint2D _w in _wheels)
        //        _w.useMotor = false;
        //}

        //Rotation
        //if (_input.y != 0)
        //{
        //    _rig.AddTorque(-_input.y * _rotationSpeed);
        //}
        //}



        if (totalcoin >= 1000)
        {
            if (badgeIMG != null)
                badgeIMG.SetActive(false);

            if (movebadgeIMG != null)
                movebadgeIMG.SetActive(true);


            if (GreenBTN != null)
                GreenBTN.SetActive(true);
        }

        purpletxt.text = "" + countpurplecoins;

        purpletxt2.text = "" + countpurplecoins;

        if (finding)
        {

            countFinding++;

        }


        if (finding && countFinding > 500)
        {

            _fuel += Time.deltaTime * 0.4f + 3.2f * _fuelConsumption / 10;
            _fuel = Mathf.Clamp(_fuel, -1, 300);
            _fuelImage4.fillAmount = _fuel / 300;
        }


        if (GameOver == true && progressbadge > 1400 && progressbadge < 1450)
        {
            _fuel += Time.deltaTime * 0.4f + 3.2f * _fuelConsumption / 10;
            _fuel = Mathf.Clamp(_fuel, -1, 100);
            _fuelImage3.fillAmount = upgradeCount;

            upgradeCount += 0.001f;


            if (firstpanel != null)
                firstpanel.SetActive(false);

        }
        else if (progressbadge == 1450)
        { PlayerPrefs.SetFloat("upgrade", upgradeCount); progressbadge++; }
        else if (GameOver == true && progressbadge > 1450)
        {
            if (purple1 != null)
                purple1.SetActive(false);

            if (purple2 != null)
                purple2.SetActive(false);

            if (purple3 != null)
                purple3.SetActive(false);

            if (purple4 != null)
                purple4.SetActive(false);


            if (playagain != null)
                playagain.SetActive(true);
            if (firstpanel != null)
                firstpanel.SetActive(false);

        }



        if (GameOver == true && progressbadge < 100)
        {
            if (progressbadge % 150 == 0)
                audioManager.play("starin");

            _fuel += Time.deltaTime * 0.4f + 3.2f * 0.5f / 10;


            _fuel = Mathf.Clamp(_fuel, -1, 300);
            _fuelImage2.fillAmount = stars;

            stars += 0.0004f;



            progressbadge++;
        }

        if (GameOver == true && progressbadge >= 100)
        {

            //PlayerPrefs.SetFloat("star", stars);

            if (firstpanel != null)
                firstpanel.SetActive(false);
            if (secondplayBTN != null)
                secondplayBTN.SetActive(true);

            if (star1 != null)
                star1.SetActive(false);

            if (star2 != null)
                star2.SetActive(false);

            if (star3 != null)
                star3.SetActive(false);

            if (star4 != null)
                star4.SetActive(false);


            audioManager.play("coindone");

        }


        if (GameOver == true && progressbadge == 14)
        {

            audioManager.play("upgrade");

        }

        if (GameOver == true)
            progressbadge++;

        if (GameOver == true && readytoplay == true)
        {
            countreadytoplay++;

            if (countreadytoplay == 250)
            {

                if (secondplayBTN != null)
                    secondplayBTN.SetActive(true);
            }
        }

        if (GameOver == true && progressbadge == 110)
        {
            if (win != null)
                win.SetActive(false);
            if (win2 != null)
                win2.SetActive(true);
            readytoplay = true;

            if (ground1 != null)
                Destroy(ground1);
            if (ground2 != null)
                Destroy(ground2);
            if (ground3 != null)
                Destroy(ground3);
        }

        if (score == 1)
        {

            //FindObjectOfType<AudioManager>().play("burn");

            //FindObjectOfType<AudioManager>().StopPlaying("background");

            //FindObjectOfType<AudioManager>().StopPlaying("raise");

            startCount = true;

            if (ControlPanel != null)

                ControlPanel.SetActive(false);



            if (vs != null)
                Destroy(vs);

            if (Acc != null)
                Destroy(Acc);

            if (dcc != null)
                Destroy(dcc);

            if (bull != null)
                Destroy(bull);


            score++;
        }

        if (counttotalcoin < totalcoin && count_win >= 10)

        {
            delaycoincount++;

            if (delaycoincount > 10)
            {
                //  coincountTXT.text = "" + counttotalcoin++;

                delaycoincount = 0;
            }


        }




        if (startCount)
        {
            count_win++;


            if (count_win == 10)
            {
                //   FindObjectOfType<AudioManager>().play("dropcoin"); 
            }


            if (count_win == 100)
            {
                if (purple1 != null)
                    purple1.SetActive(false);

                if (purple2 != null)
                    purple2.SetActive(false);

                if (purple3 != null)
                    purple3.SetActive(false);

                if (purple4 != null)
                    purple4.SetActive(false);


                audioManager.play("upgrade");

            }

            if (count_win == 50)
            {



                if (firstpanel != null)
                    firstpanel.SetActive(false);

                if (playerhealth != null)
                    playerhealth.SetActive(false);

                if (win2 != null)
                    win2.SetActive(true);

                if (win != null)
                    win.SetActive(false);

                readytoplay = true;

                if (ground1 != null)
                    Destroy(ground1);
                if (ground2 != null)
                    Destroy(ground2);
                if (ground3 != null)
                    Destroy(ground3);

                if (ControlPanel != null)

                    ControlPanel.SetActive(false);



                if (vs != null)
                    Destroy(vs);

                if (Acc != null)
                    Destroy(Acc);

                if (dcc != null)
                    Destroy(dcc);

                if (bull != null)
                    Destroy(bull);



            }


            if (count_win == 300)
            {


                //if (playagain != null)
                //    playagain.SetActive(false);


                //if (secondplayBTN != null)
                //    secondplayBTN.SetActive(true);
                FindObjectOfType<AudioManager>().play("upgrade");

                //FindObjectOfType<AudioManager>().play("Gameplay_song");
            }




        }

        if (hitcount < 150)
        {
            _fuel -= Time.deltaTime * 0.4f + 3.2f * _fuelConsumption / 10;
            _fuel = Mathf.Clamp(_fuel, -1, 100);
            _fuelImage.fillAmount = _fuel / 100;


            hitcount++;
        }

    }


    public GameObject firstplayBTN;

    bool readytoplay;

    int countreadytoplay = 0;

    public GameObject secondplayBTN;


    public GameObject optiontank;

    public void GameOverMethod()
    {
        //    FindObjectOfType<AudioManager>().play("ButtonClick");
        GameOver = true;

        //   FindObjectOfType<AudioManager>().play("starin");
        if (firstplayBTN != null)
            Destroy(firstplayBTN);

        if (star1 != null)
            star1.SetActive(true);

        if (star2 != null)
            star2.SetActive(true);

        if (star3 != null)
            star3.SetActive(true);

        if (star4 != null)
            star4.SetActive(true);
    }

    public void ShareMethod()
    {
        // FindObjectOfType<AudioManager>().play("ButtonClick");

    }

    public void TankMethod()
    {


        if (purple1 != null)
            purple1.SetActive(false);

        if (purple2 != null)
            purple2.SetActive(false);

        if (purple3 != null)
            purple3.SetActive(false);

        if (purple4 != null)
            purple4.SetActive(false);


        //  FindObjectOfType<AudioManager>().play("ButtonClick");

        if (optionMenu != null)
            optionMenu.SetActive(true);

        if (optiontank != null)
            optiontank.SetActive(false);
    }


    public void SearchMethod()
    {

        //  FindObjectOfType<AudioManager>().StopPlaying("Gameplay_song");

        finding = true;

        FindObjectOfType<AudioManager>().play("ButtonClick");

        if (Win3 != null)
            Win3.SetActive(true);

        if (win2 != null)
            win2.SetActive(false);

        //Application.OpenURL("http://blubasquiat.com/");
        adsCaller.ShowInterstitialAds();
    }

    public void UpgradeMethod()
    {
        // FindObjectOfType<AudioManager>().play("ButtonClick");

    }

    public void playMethod()
    {
        //    FindObjectOfType<AudioManager>().play("ButtonClick");


    }

    public async void Replay()
    {

        // await System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://review/?ProductId=9P59MFRVCFHG"));
        //Application.OpenURL("ms-windows-store://review/?ProductId=9P59MFRVCFHG");

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("Level_3");

        //  Application.OpenURL("http://blubasquiat.com/");
        FindObjectOfType<AudioManager>().play("ButtonClick");


    }
}

using UnityEngine.Audio;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyWeapon : MonoBehaviour
{

    public Transform firePoint;

    public GameObject bulletPrefab;

    public Rigidbody2D rg;

    public GameObject patic;

    public GameObject gunbtnLoading;

  public  bool putoffbtn = true;

    public GameObject player1;

    public GameObject boos1;

    public GameObject die;

    public GameObject gun1;

    public GameObject Sparks;

    public GameObject Fire;

    public GameObject Fire1;

    int count = 0;

    float ran = 0;

    int count_Enemy1_Hit = 0;

    int hitcount = 0;

    [SerializeField]Transform SpawnedParent;

    void Update()
    {
        if(putoffbtn)
        {
            ran = Random.Range(150.0f, 500.0f);

            count++;

            if (count > ran)
            {
                Shoot2();

                count = 0;
            }

        }    
      
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

            

            putoffbtn = false;

            if (count_Enemy1_Hit == 3)
            {
                var g = Instantiate(Fire, transform.position, transform.rotation);
                var g2 = Instantiate(Fire1, transform.position, transform.rotation);

                g.transform.parent = SpawnedParent;
                g2.transform.parent = SpawnedParent;

                AudioManager.play("die4");

                var g3  = Instantiate(Sparks, boos1.transform.position, boos1.transform.rotation);
                g3.transform.parent = SpawnedParent;
                die.SetActive(true); 

                putoffbtn = false;
                 
            }



        }

       


        }



        public void Shoot2()
    {

    

        firePoint.rotation = Quaternion.Euler(0, 180, 0);

        GameObject BulletOut = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        AudioManager.play("gun");

        AudioManager.play("bulletfly");

        Instantiate(patic, BulletOut.transform.position, firePoint.rotation);

        Destroy(BulletOut, 3);

    }




}

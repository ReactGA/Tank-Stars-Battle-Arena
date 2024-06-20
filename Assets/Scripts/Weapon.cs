using UnityEngine.Audio;
using UnityEngine;



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Weapon : MonoBehaviour
{

    public Transform firePoint;

    public GameObject bulletPrefab;

    public Rigidbody2D rg;

    public GameObject patic;

    public GameObject gunbtnLoading;

    bool putoffbtn = false;

  
    int count = 0;
    [SerializeField]Transform SpawnedParent;


    void Update()
    {

      //  count++;

       
     
    }



    public void Shoot()
    {

        putoffbtn = true;
         

        GameObject BulletOut =  Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletOut.transform.parent = SpawnedParent;
        FindObjectOfType<AudioManager>().play("gun");

        FindObjectOfType<AudioManager>().play("bulletfly");

        var g = Instantiate(patic,BulletOut.transform.position, firePoint.rotation);
        g.transform.parent = SpawnedParent;
        Destroy(BulletOut, 3);
 
    }

 



}

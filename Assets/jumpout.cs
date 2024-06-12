using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class jumpout : MonoBehaviour
{


    public float speed = 4f;

    public Rigidbody2D rb;





    void Start()
    {
    //    rb.velocity = transform.right * speed * 15 * Time.deltaTime;


    }




    void OnTriggerEnter2D(Collider2D col)
    {

       // Destroy(rb);
        FindObjectOfType<AudioManager>().play("screem");

      

    }



    void Update()
    {

     



    }



}

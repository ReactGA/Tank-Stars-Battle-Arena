using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Bullet : MonoBehaviour
{


    public float speed = 4f;

    public Rigidbody2D rb;
     
        
    public GameObject BulletTrack;

    public GameObject Spark;
     
 

    void Start()
    {
       rb.velocity = transform.right * speed* 30 * Time.deltaTime;
         

    }

    
  

    void OnTriggerEnter2D(Collider2D col)
    {
    
        Destroy(rb);
        FindObjectOfType<AudioManager>().play("hit");

        GameObject explode3 = Instantiate(Spark,new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        Destroy(explode3, 2);
         
    }

  
     
    void Update()
    {

        GameObject explode2 = Instantiate(BulletTrack , transform.position , transform.rotation);
        Destroy(explode2, 2);
  
       

    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    public float damping = 1f;
    public Vector2 offset = new Vector2(2f, 1f);
    public Transform player;
    private float dynamicSpeed;
 

    void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        if(gameObject.GetComponent<FadeCamera>())
        {
            gameObject.GetComponent<FadeCamera>().FadeIn();
        }

        FindObjectOfType<AudioManager>().play("battle");


    }

    [ContextMenu("Calculate offset")]
    public void CalculateOffset()
    {
        offset = player.position - transform.position;
        offset = new Vector2(Mathf.Abs(offset.x), Mathf.Abs(offset.y));
    }


    int countready = 0;

    void FixedUpdate()
    {


        if(countready > 100)
        {
            if (player)
            {
                Vector3 target;
                target = new Vector3(player.position.x + offset.x, player.position.y + offset.y + dynamicSpeed, transform.position.z);
                Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
                transform.position = currentPosition;

            }

        }
        else
        {
            countready++;
        }

        if(countready == 100)
        {
           // FindObjectOfType<AudioManager>().play("Gameplay_song");
        }
       

       
    }
}

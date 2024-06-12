using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{



   public  float movespeed = 7.0f;

  

    void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);


        if(inputVector.y > 0.5f)
        {
            inputVector.y -= movespeed * Time.deltaTime;

        }


        if(Input.GetKey(KeyCode.UpArrow))
        {

            inputVector.y = +1;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {

            inputVector.y = -1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {

            inputVector.x = -1;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {

            inputVector.x = +1;
        }


        inputVector = inputVector.normalized;

        Vector3 movedir = new Vector3(inputVector.x, 0f, inputVector.y);

        transform.position += movedir * movespeed * Time.deltaTime;


    }
}

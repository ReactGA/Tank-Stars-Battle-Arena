﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{

    public float speed = 40f;

    public Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
         rb.velocity = transform.right * speed * 150 * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
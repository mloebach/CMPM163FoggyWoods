﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchRotate : MonoBehaviour
{
    // Start is called before the first frame update


    void Start()
    {
        //int rotate = Random.Range(0,360);
       // transform.Rotate(0, rotate, 0);    
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0.5f, 0); 
    }
}

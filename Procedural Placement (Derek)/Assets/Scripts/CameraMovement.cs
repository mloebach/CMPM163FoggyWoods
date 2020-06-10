using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();        
    }

    void CalculateMovement(){
        float horizontalInput = Input.GetAxis("Horizontal");
        float speed = 7.0f;

        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        transform.Translate(direction * Time.deltaTime * speed);
        transform.position = new Vector3(
                transform.position.x, transform.position.y,0);


        if(transform.position.x > 140){
            transform.position = new Vector3(
                0, transform.position.y,0);
        }else if(transform.position.x < 0){
            transform.position = new Vector3(
                140, transform.position.y,0);
        }

    }

}

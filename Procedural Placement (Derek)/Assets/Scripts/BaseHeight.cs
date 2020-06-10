using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHeight : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
       // float height = getRandHeight();
        //transform.position += new Vector3(0, height, 0);
    }

    public float getRandHeight(){
        float height = Random.Range(-0.0f,2.5f);
        transform.position += new Vector3(0, height, 0);
        return height;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootsHeight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float height = Random.Range(0,0.5f);
        transform.position += new Vector3(0, height, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

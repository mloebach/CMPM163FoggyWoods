using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchHeight : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Base =  default;


    void Start()
    {
        float cap = Base.GetComponent<BaseHeight>().getRandHeight();
        float height = Random.Range(0, cap);
        transform.position += new Vector3(0, height, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

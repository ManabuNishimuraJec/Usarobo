using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BornBlock : MonoBehaviour
{
    public GameObject Block;

    public float time = 0.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (time == 750)
        {
            Instantiate(Block, transform.position, Quaternion.identity);

            time = 0;
        }

        ++time;
    }
}

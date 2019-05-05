using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndControl : MonoBehaviour
{
    Rigidbody rid;
    [SerializeField]
    private float speed;

    void Start()
    {
        rid = GetComponent<Rigidbody>();
        MeshRenderer MeshR = GetComponent<MeshRenderer>();

        MeshR.material.color = new Color(1.0f, 0.0f, 0.1f,0.8f);
    }
    
    void Update()
    {
        rid.velocity = new Vector3(0.0f, 0.0f, speed);
    }
}

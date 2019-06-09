using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndControl : MonoBehaviour
{
    Rigidbody rid;
    [SerializeField]
    private float speed;
	private MapMaster mapMaster = new MapMaster();
    void Start()
    {
        rid = GetComponent<Rigidbody>();
        MeshRenderer MeshR = GetComponent<MeshRenderer>();
		mapMaster.EndLine = transform.position;
        //MeshR.material.color = new Color(1.0f, 0.0f, 0.1f,0.8f);
    }
    
    void Update()
    {
		mapMaster.EndLine = transform.position;
        rid.velocity = new Vector3(0.0f, 0.0f, speed);
    }
}

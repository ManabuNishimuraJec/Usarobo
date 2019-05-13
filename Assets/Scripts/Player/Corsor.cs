using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corsor : MonoBehaviour
{
	//  変数宣言
    Vector3 corsorPoint;
	
    void Update()
    {
		  this.corsorPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 a = new Vector3 (Input.mousePosition.x,Input.mousePosition.y,corsorPoint.z);
        transform.position = Camera.main.ScreenToWorldPoint (a);
    }
}

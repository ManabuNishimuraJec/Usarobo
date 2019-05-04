using UnityEngine;
using System.Collections;

public class MouseScript : MonoBehaviour
{
    //  変数宣言
    Vector3 screenPoint;

    void Update()
    {
        /*
        this.screenPoint = Camera.main.transform.position;

        Vector3 a = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        transform.position = Camera.main.ScreenToWorldPoint(a);

        Vector3 p = Camera.main.transform.position;
        p.y = transform.position.y;
        transform.LookAt(p);

        Vector3 v = Camera.main.transform.position;
        p.x = transform.position.x;
        transform.LookAt(v);
        */

        transform.position = Input.mousePosition;
    }
}
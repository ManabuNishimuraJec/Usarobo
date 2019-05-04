using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class canonControl : MonoBehaviour
{
    [SerializeField]
    private PlayerControl pc;

    [SerializeField]
    private GameObject Bullet;  // Bullet格納

    void Start()
    {
        pc.OnShotCanonmessage.Subscribe(_ =>
        { // バレット生成
            Instantiate(Bullet, transform.position + new Vector3(0.0f, 0.0f, 1.0f), Quaternion.identity);
        });
    }
}

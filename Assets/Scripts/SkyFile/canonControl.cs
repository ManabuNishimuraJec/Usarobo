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
    [SerializeField]
    private Transform center;

    private ShotUnitMaster shotMaster = new ShotUnitMaster();

    void Start()
    {
        pc.OnShotCanonmessage.Subscribe(_ =>
        {
			// バレット生成
            Instantiate(Bullet, transform.position + new Vector3(0.0f, 0.0f, 1.0f), Quaternion.identity);
        });
    }

    void Update()
    {
        // マウスカーソルの座標を取得
        var pos = Vector3.forward * Vector3.Distance(transform.position, center.position);
        Vector3 dir = (transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition + pos));

        // レイを飛ばす(発射点の座標、発射する向き)
        Ray ray = new Ray(transform.position, dir);
        // レイの向きをプレイヤーの向きと対応させる
        transform.rotation = Quaternion.LookRotation(ray.direction);
        MasterWrite();
    }

    void MasterWrite()
    {
        shotMaster.Rotation = transform.rotation;
    }
}

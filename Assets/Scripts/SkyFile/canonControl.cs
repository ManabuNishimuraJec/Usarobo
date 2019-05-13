using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class canonControl : MonoBehaviour
{
    [SerializeField]
    private PlayerControl pControl;

	private float time;
	private float maxtime = 0.04f;

    [SerializeField]
    private GameObject Bullet;  // Bullet格納
    [SerializeField]
    private Transform center;
	public Vector3 wallPos;		// カーソルに渡すようの座標変数
    private ShotUnitMaster shotMaster = new ShotUnitMaster();

	private Subject<Vector3> wallPosSubject = new Subject<Vector3>();     // Vector3引数のメッセージ送る用のUniRxデータ
	
	private Animator _anim;         // アニメータ

	public IObservable<Vector3> OnWallPosMessage		// メッセージを送る処理をする関数
	{
		get { return wallPosSubject; }
	}

	void Start()
    {
        _anim = GetComponent<Animator>();		// アニメータ
		
        pControl.OnShotCanonMessage.Subscribe(_ =>		// 信号キャッチ
        {
            _anim.Play("knockback",0,0.0f);   // Animation再生
            // バレット生成
            Instantiate(Bullet, transform.position + new Vector3(0.0f, 0.0f, 1.0f), transform.rotation);
        });
	}

    void Update()
    {
		// マウスカーソルの座標を取得
		var pos = Vector3.forward * Vector3.Distance(transform.position, center.position);
        Vector3 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition + pos) - transform.position);
		// レイ情報取得用変数
		RaycastHit hit;

        // レイを飛ばす(発射点の座標、発射する向き)
        Ray ray = new Ray(transform.position, dir);
		// レイを可視化
		Debug.DrawRay(ray.origin, dir, Color.red);
        // ShotUnitの向きとRayの向きを対応させる
        transform.rotation = Quaternion.LookRotation(ray.direction);

		// レイがちゃんと当たっているか確認
		if (Physics. Raycast(ray,out hit,90))
		{
			if(hit.collider.tag == "wall")
			{
				Debug.Log("こ、これはwallだ！！！");
				// wallの座標を取得
				wallPos = hit.collider.GetComponent<Transform>().position;
				// メッセージをCorsorControlに送る
				wallPosSubject.OnNext(wallPos);

				// Corsorに取得した座標を渡す
				//CC.transform.position = wallPos + new Vector3(0.0f,0.0f,-2.0f);
			}
		}
        MasterWrite();
    }

    void MasterWrite()
    {
        shotMaster.Rotation = transform.rotation;
    }
}

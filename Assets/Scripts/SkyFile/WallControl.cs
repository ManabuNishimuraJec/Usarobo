using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallControl : MonoBehaviour
{
    // 移動速度
	[SerializeField]
    private float speed = 0.0f;

	[SerializeField]
    // 障害物耐久度
    private int HP;

    // Rigidbody取得
    Rigidbody rid;
    // Transformを取得
    Transform trs;

    // 自身の座標を取得するための変数
    private Vector3 mypos;
    // Playerの座標を取得するための変数
	private Vector3 posDir;

    // WallFormer格納用
    private GameObject WallFormer;

	private float timeKeaper;
	[SerializeField]
	private float timeMax;

    void Start()
    {
        // Rigidbody取得
        rid = GetComponent<Rigidbody>();
        // Transform取得
        trs = GetComponent<Transform>();

		mypos = transform.position;
	}

    void Update()
    {
		// 回転を常にかける
		transform.Rotate(new Vector3(0.0f, 1.5f, 0.8f));

		// 現時点とプレイヤーとの座標差を求める
		posDir = Camera.main.transform.position - mypos;

		Debug.Log(Camera.main.transform.position + "           " + posDir);

		// ブロックにｚ軸に速度を加える
		rid.velocity = posDir * speed * 0.008f;

		// 時間を加算
		timeKeaper += Time.deltaTime;

		// 設定したタイムを超えたら消滅
		if(timeMax < timeKeaper)
		{
			Destroy(this.gameObject);
		}
        // HPが0になったら削除
        if(HP < 0)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        // バレットに当たったら耐久値を減らす
        if (collision.gameObject.tag == "bullet")
        {
            HP--;
        }
    }
}

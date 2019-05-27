using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallControl : MonoBehaviour
{
    // 移動速度
    private float speed = 0.0f;

    // 障害物耐久度
    private int HP;

    // Rigidbody取得
    Rigidbody rid;
    // Transformを取得
    Transform myTf;

    // 自身の座標を取得するための変数
    private Vector3 mypos;
    // Playerの座標を取得するための変数
    private Vector3 ppos;

    // 自身のZ座標を格納するための変数
    private float myz;
    // PlayerのZ座標を格納するための変数
    private float pz;

    // WallFormer格納用
    private GameObject WallFormer;

    // 障害物のナンバー用変数
    private int TypeNum;

    // 障害物の質量の分類
    public bool light;     // 軽い
    public bool middle;    // 中くらい
    public bool heavy;     // 重い

    void Start()
    {
        // Rigidbody取得
        rid = GetComponent<Rigidbody>();
        // Transform取得
        myTf = GetComponent<Transform>();
        // WallFormerの情報を取得
        WallFormer = GameObject.Find("wall  Former");
        
        // 障害物のタイプナンバーを代入 
        TypeNum = Random.Range(1, 4);

        // ナンバーによってタイプフラグを立てる
        switch (TypeNum)
        {
            case 1:
                light = true;
                break;
            case 2:
                middle = true;
                break;
            case 3:
                heavy = true;
                break;
        }

        // ランダムな形状、角度の障害物ステータスを代入
        myTf.localScale = new Vector3(Random.Range(1.0f, 20.0f), Random.Range(1.0f, 10.0f), Random.Range(1.0f, 2.0f));                          // 大きさ
        myTf.position = new Vector3(Random.Range(-20.0f, 20.0f), Random.Range(-7.0f, 7.0f),WallFormer.transform.position.z );                   // 生成ポイントを中心にした座標
        myTf.rotation = new Quaternion(Random.Range(-90.0f, 90.0f),Random.Range(-90.0f,90.0f), Random.Range(-180.0f, 180.0f),0.0f);             // 角度
        
        if (light)
        {
            // 質量、速度を代入,重さに対応した色に変更
            rid.mass = 30;
            gameObject.GetComponent<Renderer>().material.color = new Color(0f, 1f, 1f, 1f);
            speed = -40;
            HP = 4;
        }
        else if (middle)
        {
            // 質量、速度を代入,重さに対応した色に変更
            rid.mass = 300;
            gameObject.GetComponent<Renderer>().material.color = new Color(1f, 0.4f, 0f, 1f);
            speed = -18;
            HP = 13;
        }
        else if (heavy)
        {
            // 質量、速度を代入,重さに対応した色に変更
            rid.mass = 500;
            gameObject.GetComponent<Renderer>().material.color = new Color(0.5f, 0f, 1f, 1f);
            speed = -5;
            HP = 16;
        }
    }

    void Update()
    {
        // 自身の座標を取得
        mypos = myTf.position;
        // Playerの座標を取得
        ppos = GameObject.Find("Player").transform.position;

        // 自身のZ座標を取得
        myz = mypos.z;
        // PlayerのZ座標を取得
        pz = ppos.z;

        // ブロックにｚ軸に速度を加える
        rid.velocity = new Vector3(0, 0, speed);

        // 自身のZ座標がPlayerのZ座標より小さくなった時、自身を削除する
        if (myz < pz)
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

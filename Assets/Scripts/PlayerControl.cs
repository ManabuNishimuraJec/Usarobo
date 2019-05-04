using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System;

public class PlayerControl : MonoBehaviour
{
    //  移動速度
    public float speedX = 0.0f;
    public float speedY = 0.0f;
    public float speedZ = 0.0f;

    // HP
    public int HP;

    //  入力判定
    private float InputX = 0.0f;
    private float InputY = 0.0f;

	// 発射間隔
	public float maxtime;
	private float time;
    
	public Transform center;

    Rigidbody MoveX;
    Vector3 Move;

    private Subject<Unit> shotSubject = new Subject<Unit>();

    public IObservable<Unit> OnShotCanonmessage
    {
        get { return shotSubject; }
    }

    void Start()
    {
        //  RigidbodyをGetComponemt
        MoveX = GetComponent<Rigidbody>();
    }
    void Update()
    {

        int iNum = 10;
        int iNum2 = iNum;
		// マウスカーソルの座標を取得
		var pos = Vector3.forward * Vector3.Distance(transform.position, center.position);
		Vector3 dir = (transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition + pos));
		
		Debug.Log(dir);
        
		// レイを飛ばす(発射点の座標、発射する向き)
		Ray ray = new Ray(transform.position, dir);
        // レイの向きをプレイヤーの向きと対応させる
		transform.rotation = Quaternion.LookRotation(ray.direction);
        
        //  未入力時
        InputX = 0;
        InputY = 0;

        // HPが0になったら自身を削除
        if(HP <= 0)
        {
            Destroy(this.gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene("title");
        }

        //  Aが入力された場合
        if (Input.GetKey(KeyCode.A))
        {
            //Debug.Log("A");
            InputX = -1 ;
        }

        //  Wが入力された場合
        if (Input.GetKey(KeyCode.W))
        {
            InputY = 1;
        }

        //  Dが入力された場合された場合
        if (Input.GetKey(KeyCode.D))
        {
            InputX = 1;
        }

        //  Sが入力された場合
        if (Input.GetKey(KeyCode.S))
        {
            InputY = -1;
        }
		// ↓画面端で停止する
        if (transform.position.x > 5 && InputX == 1)
        {
            InputX = 0;
        }

        if (transform.position.x < -5 && InputX == -1)
        {
            InputX = 0;
        }

        if (transform.position.y > 3.6 && InputY == 1)
        {
            InputY = 0;
        }

        if (transform.position.y < -1.6 && InputY == -1)
        {
            InputY = 0;
        }
		// ↑画面端で停止する

        //  移動
        MoveX.velocity = new Vector3(speedX * InputX, speedY * InputY,0);

        time += Time.deltaTime;
        // 発射
        if (time > maxtime)
        {
            // マウス左クリック
            if (Input.GetMouseButton(0))
            {
                shotSubject.OnNext(Unit.Default);
            }

            time = 0.0f;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // 障害物に当たったらHPを減少
        if(other.gameObject.tag == "wall")
        {
            HP-=1;
        }

        if(other.gameObject.tag == "End")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Clear");
        }
    }
}

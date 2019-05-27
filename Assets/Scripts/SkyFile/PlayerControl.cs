using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System;

public class PlayerControl : MonoBehaviour
{
    private PlayerMaster pMaster = new PlayerMaster();

    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private float jumpPower = 0.0f;
    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private Transform center;

    private CharacterController playerControl;
    private float inputHorizontal;
    private float inputVertical;
    private Vector3 velocity;

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
    
    Rigidbody MoveX;
    Vector3 Move;

    private Subject<Unit> shotSubject = new Subject<Unit>();

    public IObservable<Unit> OnShotCanonMessage
    {
        get { return shotSubject; }
    }

    void Start()
    {
        //  RigidbodyをGetComponemt
        MoveX = GetComponent<Rigidbody>();
        playerControl = GetComponent<CharacterController>();
		Cursor.visible = false;
	}
    void Update()
    {
        if (pMaster.CheckMode)
        {
            ShootingControl();
        }

        else
        {
            ActionControl();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // 障害物に当たったらHPを減少
        if(other.gameObject.tag == "wall")
        {
            HP-=1;
        }
		// エネミーバレットに当たったらHPを減少
		if (other.gameObject.tag == "enemybullet")
		{
			HP -= 1;
		}

		if (other.gameObject.tag == "End")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Clear");
        }
    }

    void ActionControl()
    {
        ShotAtk();

        // 地面に立っている状態
        if (playerControl.isGrounded)
        {
            inputHorizontal = Input.GetAxis("Horizontal");
            inputVertical = Input.GetAxis("Vertical");

            // カメラの方向から、x-z平面の単位ベクトルを取得
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            velocity = Vector3.zero;

            // カメラの方向を加味して移動
            velocity = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;
            velocity *= moveSpeed;

            if (Input.GetKeyDown(KeyCode.V))
            {
                // ジャンプ
                velocity.y += jumpPower;
            }
        }

        // 重力を設定
        velocity.y += Physics.gravity.y * Time.deltaTime;


        playerControl.Move(velocity * Time.deltaTime);
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

    void ShootingControl()
    {
        //Debug.Log("R");
        int iNum = 10;
        int iNum2 = iNum;

        //  未入力時
        InputX = 0;
        InputY = 0;

        // HPが0になったら自身を削除
        if (HP <= 0)
        {
            Destroy(this.gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene("title");
        }

        //  Aが入力された場合
        if (Input.GetKey(KeyCode.A))
        {
            //Debug.Log("A");
            InputX = -1;
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
        if (transform.position.x > 4.0f && InputX == 1)
        {
            InputX = 0;
        }

        if (transform.position.x < -4.0f && InputX == -1)
        {
            InputX = 0;
        }

        if (transform.position.y > 2.0 && InputY == 1)
        {
            InputY = 0;
        }

        if (transform.position.y < -2.0f && InputY == -1)
        {
            InputY = 0;
        }
        // ↑画面端で停止する

        //  移動
        MoveX.velocity = new Vector3(speedX * InputX, speedY * InputY, 0.0f);

        time += Time.deltaTime;
        // 発射
        if (time > maxtime)
        {
            // マウス左クリック
            if (Input.GetMouseButton(0))
            {
				// 発射する信号を撃つ
                shotSubject.OnNext(Unit.Default);
            }

            time = 0.0f;
        }
    }

    void  ShotAtk()
    {
        // マウス左クリック
        if (Input.GetMouseButton(0))
        {
            shotSubject.OnNext(Unit.Default);
        }
    }
}

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
	private BossStage bossstage;

	GameObject gameobject;

	[SerializeField]
	private GameObject Bullet;
	[SerializeField]
	private Transform center;

	private CharacterController playerControl;
	private Vector3 velocity;
	Rigidbody MoveX;

	//	現在座標格納用
	private Vector3 prevPos;

	//	入力用
	//--------------------------------------------------------
	[SerializeField]
	private float atkPower = 0.0f;          //	攻撃力(Bullet)
	[SerializeField]
	private float jumpPower = 0.0f;     //	ジャンプ力
	[SerializeField]
	private Vector3 moveXYZ;                //	移動速度
	public int hp;                                 //	HP
	[SerializeField]
	private int maxHp;                                 //	HP
													   //--------------------------------------------------------

	private float inputHorizontal;
	private float inputVertical;


	//  入力判定
	private float InputX = 0.0f;
	private float InputY = 0.0f;

	// 発射間隔
	public float maxtime;
	private float time;


	private float startTime;                                //	時間カウント用
	private float timer;
	private float prevHorizontal;                      //	入力保存用
	private float prevVertical;                             //	入力保存用

	private Subject<Unit> shotSubject = new Subject<Unit>();

	public IObservable<Unit> OnShotCanonMessage
	{
		get { return shotSubject; }
	}

	void Awake()
	{
		//	各情報をMasterに保存
		pMaster.MaxHp = maxHp;
		pMaster.Hp = maxHp;
		pMaster.BulletPower = atkPower;
		pMaster.MoveSpeed = moveXYZ;
		pMaster.JumpPower = jumpPower;

		//	Masterの情報を格納
		hp = pMaster.MaxHp;

		//	現在の位置情報で初期化
		prevPos = transform.position;
	}
	void Start()
	{
		//  RigidbodyをGetComponemt
		MoveX = GetComponent<Rigidbody>();
		//	CharacterControllerをGetCompornent
		playerControl = GetComponent<CharacterController>();

	}
	void Update()
	{
		//	時間カウント用
		timer += Time.deltaTime;

		//	Stageで呼び出す情報を判断
		if (pMaster.CheckMode)
		{
			ShootingControl();
		}
		else
		{
			ActionControl();
		}

		//	差分を格納
		Vector3 diffPos = transform.position - prevPos;     //	角度を求める

		//	移動方向に向かせる
		if (diffPos != Vector3.zero)
		{
			if (playerControl.isGrounded)
			{
				transform.rotation = Quaternion.LookRotation(diffPos);
			}
		}

		//	Playerの位置情報をMasterに保存
		pMaster.PlayerPosition = transform.position;

		//	Playerの現在位置を保存
		prevPos = transform.position;
	}

	private void OnTriggerEnter(Collider other)
	{
		// 障害物に当たったらHPを減少
		if (other.gameObject.tag == "wall")
		{
			pMaster.Hp -= 1;    //	HP減少
		}
		if (other.gameObject.tag == "enemybullet")
		{
			pMaster.Hp -= 1;    //	HP減少
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
			inputHorizontal = 0;
			inputVertical = 0;

			//-----------------------------------------------------
			//  入力開始
			//-----------------------------------------------------
			if (Input.GetKey(KeyCode.W))
			{
				Debug.Log("in:" + prevVertical);
				inputVertical = 1;
			}
			else if (Input.GetKey(KeyCode.S))
			{
				inputVertical = -1;
			}
			if (Input.GetKey(KeyCode.A))
			{
				inputHorizontal = -1;
			}
			else if (Input.GetKey(KeyCode.D))
			{
				inputHorizontal = 1;
			}

			//-----------------------------------------------------
			//  同じキー判断
			//-----------------------------------------------------
			if (inputHorizontal != 0)
			{
				if (inputHorizontal == prevHorizontal)
				{
					Debug.Log("horizon:" + inputHorizontal);
					Debug.Log("prevhorizon:" + inputHorizontal);
					if (timer <= 0.5f)
					{
						inputHorizontal++;
					}
				}
			}
			if (inputVertical != 0)
			{
				if (inputVertical == prevVertical)
				{
					if (timer <= 0.5f)
					{
						inputVertical++;
					}
				}
			}


			//-----------------------------------------------------
			//  入力判断
			//-----------------------------------------------------
			if (Input.GetKeyUp(KeyCode.W))
			{
				Debug.Log(prevVertical);
				prevVertical = inputVertical;
				timer = 0;
			}
			else if (Input.GetKeyUp(KeyCode.A))
			{
				Debug.Log(prevHorizontal);
				prevHorizontal = inputHorizontal;
				timer = 0;
			}
			else if (Input.GetKeyUp(KeyCode.S))
			{
				Debug.Log(prevVertical);
				prevVertical = inputVertical;
				timer = 0;
			}
			else if (Input.GetKeyUp(KeyCode.D))
			{
				Debug.Log(prevHorizontal);
				prevHorizontal = inputHorizontal;
				timer = 0;
			}


			// カメラの方向から、x-z平面の単位ベクトルを取得
			Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
			velocity = Vector3.zero;

			// カメラの方向を加味して移動
			velocity = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;
			velocity *= pMaster.MoveSpeed.x;

			if (Input.GetKeyDown(KeyCode.V))
			{
				// ジャンプ
				velocity.y += pMaster.JumpPower;
			}
		}

		// 重力を設定
		velocity.y += Physics.gravity.y * Time.deltaTime;

		//	移動させる
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
		if (hp <= 0)
		{
            Debug.Log("PlayerHP" + hp);
            UnityEngine.SceneManagement.SceneManager.LoadScene("title");
            Destroy(this.gameObject);
		}

		//  Aが入力された場合
		if (Input.GetKey(KeyCode.A))
		{
			Debug.Log("A");
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
		MoveX.velocity = new Vector3(pMaster.MoveSpeed.x * InputX, pMaster.MoveSpeed.y * InputY, 0);

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

	void ShotAtk()
	{
		// マウス左クリック
		if (Input.GetMouseButton(0))
		{
			shotSubject.OnNext(Unit.Default);
		}
	}
}

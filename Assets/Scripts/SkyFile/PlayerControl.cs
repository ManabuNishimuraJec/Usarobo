using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System;

public class PlayerControl : MonoBehaviour
{
	private PlayerMaster pMaster = new PlayerMaster();

	GameObject gameobject;

	[SerializeField]
	private GameObject Bullet;

	private CharacterController playerControl;
	private Vector3 velocity;
	Rigidbody Move;

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
	private float prevHorizontal;							//	入力保存用
	private float prevVertical;                             //	入力保存用


	private Subject<Unit> shotSubject = new Subject<Unit>();

	// ルート追跡用変数↓
	[SerializeField]
	private PlayerChasePointSettings playerChasePointSettings;  //	PlayerChasePointSettings

	private Vector3 prevTarPos;             // 達したポイントの座標格納用
	private Vector3 nextTarPos;             // 次に向かうポイントの座標格納用
	private int pointNum = 0;               // ポイントに対応した要素番号
	private GameObject chasePoint;          // ポイントの情報用変数
	private bool lastChasePointFlag = true; // 最終ポイントに達した時用のフラグ
	[SerializeField]
	private float chaseSpeed;               // ルート追跡速度
	private bool start = true;				// 疑似スタートメソッド用フラグ

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
		Move = GetComponent<Rigidbody>();
		//	CharacterControllerをGetCompornent
		playerControl = GetComponent<CharacterController>();

	}
	void Update()
	{
		// Playerの位置情報をスクリーン座標に変換
		pMaster.ScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
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
		// PlayerChasePointに当たった場合
		if (other.tag == "PlayerChasePoint")
		{
			Debug.Log("pointNum " + pointNum);
			// 最後のポイントかどうかのフィルター
			if ((pointNum + 1) < playerChasePointSettings.GetChasePointCount())
			{
				// 向かっていたポイントを一番最近に通ったポイントに更新
				prevTarPos = transform.position;

				// 次に向かうポイントを更新
				nextTarPos = playerChasePointSettings.GetChasePoint(pointNum + 1).transform.position;
				
				// ポイントナンバーを更新
				pointNum++;
			}
			else
			{
				// ラストに達したというフラグを立てる
				lastChasePointFlag = false;
			}
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

			inputHorizontal = Input.GetAxis("Horizontal");
			inputVertical = Input.GetAxis("Vertical");

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
		if(lastChasePointFlag)
		{
			// ルート追跡関数呼び出し
			RootChase();
		}
		
		//  未入力時
		inputHorizontal = 0;
		inputVertical = 0;
		Move.velocity = Vector3.zero;
		Move.angularVelocity = Vector3.zero;

		// 入力時
		inputHorizontal = Input.GetAxis("Horizontal");
		inputVertical = Input.GetAxis("Vertical");

		// HPが0になったら自身を削除
		if (hp <= 0)
		{
            Destroy(this.gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene("title");
		}
		
		// 自身が画格より外に出たら動きを止める
		if (pMaster.ScreenPosition.x > Screen.width && inputHorizontal == 1
			|| pMaster.ScreenPosition.x < 0 && inputHorizontal == -1)
		{
			inputHorizontal = 0;
		}
		if (pMaster.ScreenPosition.y > Screen.height && inputVertical == 1
			|| pMaster.ScreenPosition.y < 0 && inputVertical == -1)
		{
			inputVertical = 0;
		}
		// ↑画面端で停止する

		// カメラの方向から、x-z平面の単位ベクトルを取得
		Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
		velocity = Vector3.zero;

		// カメラの方向を加味して移動
		velocity = Camera.main.transform.up * inputVertical + Camera.main.transform.right * inputHorizontal;
		velocity *= pMaster.MoveSpeed.x;

		//	移動させる
		Move.velocity = new Vector3(Time.deltaTime * velocity.x, Time.deltaTime * velocity.y, 0).normalized;

		// 発射用タイマー
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

	void RootChase()
	{
		if(start)	// 疑似スタートメソッド
		{
			if (0 < playerChasePointSettings.GetChasePointCount())
			{
				// ChasePointの情報を知る
				chasePoint = playerChasePointSettings.GetChasePoint(pointNum);
				// 要素番号０のポイントの座標を初期地点として格納
				prevTarPos = chasePoint.transform.position;
				// 要素番号０＋１のポイントの座標を次に向かう座標として格納
				nextTarPos = playerChasePointSettings.GetChasePoint(pointNum + 1).transform.position;
				// 初期地点にワープ
				transform.position = prevTarPos;

				// スタートフラグを下げる
				start = false;
			}
		}
		// 向かう方向を求める
		var heading = (nextTarPos - prevTarPos);
		// 方向に進む
		transform.Translate(heading * chaseSpeed * Time.deltaTime, Space.World);
	}
}

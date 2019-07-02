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

	private CharacterController playerControl;
	private Vector3 moveVec;
	Rigidbody Move;
    private Transform cameraPos;

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
	[SerializeField]
	private float position_z;
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

        if(Camera.main != null)
        {
            cameraPos = Camera.main.transform;
        }

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
			moveVec = Vector3.zero;

			// カメラの方向を加味して移動
			moveVec = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;
			moveVec *= pMaster.MoveSpeed.x;

			if (Input.GetKeyDown(KeyCode.V))
			{
				// ジャンプ
				moveVec.y += pMaster.JumpPower;
			}
		}

		// 重力を設定
		moveVec.y += Physics.gravity.y * Time.deltaTime;

		//	移動させる
		playerControl.Move(moveVec * Time.deltaTime);

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
		if (hp <= 0)
		{
			Destroy(this.gameObject);
			UnityEngine.SceneManagement.SceneManager.LoadScene("title");
		}

		inputHorizontal = Input.GetAxis("Horizontal");
		inputVertical = Input.GetAxis("Vertical");

		moveVec = transform.localPosition;
		moveVec.z = position_z;

		moveVec.x += inputHorizontal * pMaster.MoveSpeed.y * Time.deltaTime;
		moveVec.y += inputVertical * pMaster.MoveSpeed.x * Time.deltaTime;

		transform.localPosition = moveVec;

		Clamp();        // 位置補正

		// 発射用タイマー
		time += Time.deltaTime;

		// 発射
		if (time > maxtime)
		{
			ShotAtk();
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
	
	void Clamp()
	{d
		// プレイヤーの座標を取得
		Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
		Vector3 newPos = PosCheck(pos, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 0.0f));
		Vector3 playerNewPos = Camera.main.ViewportToWorldPoint(newPos);

		transform.position = playerNewPos;
	}


	// 画角範囲の補正
	Vector3 PosCheck(Vector3 playerPoint,Vector3 min,Vector3 max)
	{
		Vector3 newPos = playerPoint;
		newPos.z = playerPoint.z;

		if(playerPoint.x <= min.x || playerPoint.y <= min.y)
		{
			if(playerPoint.x <= min.x)
			{
				newPos.x = min.x;
			}
			if (playerPoint.y <= min.y)
			{
				newPos.y = min.y;
			}
			return newPos;
		}
		if (playerPoint.x >= max.x || playerPoint.y >= max.y)
		{
			if (playerPoint.x >= max.x)
			{
				newPos.x = max.x;
			}
			if (playerPoint.y >= max.y)
			{
				newPos.y = max.y;
			}
			return newPos;
		}
		return playerPoint;
	}
}
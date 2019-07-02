using UnityEngine;

public class MendakoZako : MonoBehaviour
{
	private Rigidbody rig;      //rigidbody
	private Collider col;       //colllder

	[SerializeField]
	private float speed;        //初速
	[SerializeField]
	private float maxSpeed;     //最大速度(20.0fを推奨)
	[SerializeField]
	private float minSpeed;		//最小速度（3.0fを推奨）
	[SerializeField]
	private float stopSpeed;	//減速スピード
	private PlayerMaster pM = new PlayerMaster();//PlayerManager
	private bool isMove;		//移動判定
	[SerializeField]
	private float maxTime;		//最大待機時間
	private float time;			//現在待機時間
	private bool acceleration;	//加速判定
	[SerializeField]
	private float destroyPos;	//デストロイポジション
	[SerializeField]
	private float rotedis;		//回転し続ける距離
	[SerializeField]
	private float maxDestroyTime;	//消えるまでの最大時間
	private float destroyTime;      //消えるまでの現在時間

	private bool isLive=true;       //生死確認用
	private Vector3 dethRolling;	//死亡後の回転

	private float nowSpeed;			//現在の速さ（確認用）

	void Start()
	{
		rig = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();
		isMove = true;
		acceleration = true;
		time = 0.0f;
		DirectionChange();		//プレイヤーの方を見る
	}
	void Update()
	{
		destroyTime += Time.deltaTime;			//いなくなるまでの時間加算
		if (isLive)
		{
			
			if (isMove)
			{
				if (acceleration)
				{
					if (CheckMaxSpeed()) Acceleration();    //一定速度まで加速
					else acceleration = false;              //加速フラグを下げる
				}
				else if (CheckMinSpeed()) { isMove = false; rig.velocity = Vector3.zero; }      //移動フラグを下して止める
				else Deceleration();												            //減速
			}
			else
			{
				//一定時間止まる
				if (time > maxTime)
				{
					isMove = true;					//移動フラグを上げる
					acceleration = true;			//加速フラグを上げる
					time = 0.0f;					//待機時間を０にする
					//一定距離まで向きを変える
					if (DistanceAbs() > rotedis)
					{
						DirectionChange();			//向きを変える
					}
				}
				time += Time.deltaTime;
			}
			if (transform.position.z < pM.PlayerPosition.z - destroyPos) Destroy(gameObject);
		}
		else
		{
			rig.AddForce(stopSpeed * transform.forward, ForceMode.Force);
			rig.angularVelocity = new Vector3(0.0f, 0.0f, 5.0f);
		}
		if (destroyTime > maxDestroyTime) Destroy(gameObject);
	}
	private void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "bullet")
		{
			isLive = false;         //死んだことにする
			col.enabled = false;	//当たり判定を消す
			dethRolling = new Vector3(Time.time % 10.0f, Time.time % 10.0f, Time.time % 10.0f);
		}
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "bullet")
		{
			isLive = false;				//死んだことにする
			col.enabled = false;        //当たり判定を消す
			dethRolling = new Vector3(Time.time%10.0f, Time.time % 10.0f, Time.time % 10.0f);
		}
	}

	//関数群

	//プレイヤーの方向を返す関数
	Vector3 Aim() { return (pM.PlayerPosition - transform.position) * -1.0f; }
	//プレイヤーとの距離を返す関数
	float DistanceAbs() { return Vector3.Distance(transform.TransformPoint(transform.position), pM.PlayerPosition); }
	//向きをプレイヤーの方向へ向かせる関数
	void DirectionChange() { transform.rotation = Quaternion.LookRotation(Aim()); }
	//加速させる関数
	void Acceleration() { rig.AddForce(-speed * transform.forward, ForceMode.Impulse); }
	//減速させる関数
	void Deceleration() { rig.AddForce(stopSpeed * transform.forward, ForceMode.Force); }
	//速度が最大速度を超えていないかチェックする関数
	bool CheckMaxSpeed() { return rig.velocity.magnitude < maxSpeed; }
	//速度が最小速度を下回ったことを返す関数
	bool CheckMinSpeed() { return rig.velocity.magnitude < minSpeed; }
}
//修正すべき課題
//めんだこの進行方向の設定　*完了
//めんだこの動き方
using UnityEngine;

public class MendakoZako : MonoBehaviour
{
	private Rigidbody rig;
	[SerializeField]
	private float speed;
	[SerializeField]
	private float stopSpeed;
	private PlayerMaster pM = new PlayerMaster();
	[SerializeField]
	private Vector3 Dec;
	private bool isMove;
	[SerializeField]
	private float maxTime;
	private float time;
	private bool a;
	private Quaternion d;
	void Start()
    {
		rig = GetComponent<Rigidbody>();
		isMove = true;
		a = true;
		time = 0.0f;
		var aim = (pM.PlayerPosition - transform.position)*-1.0f;
		transform.localRotation = Quaternion.LookRotation(aim, transform.up);
		d = new Quaternion(70.0f, 70.0f, 70.0f,0.0f);
    }

	void Update()
	{
		//float angle = transform.eulerAngles.z * (Mathf.PI);
		//Vector3 nowVector = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f);
		if (isMove)
		{
			if (a)
			{
				if (rig.velocity.magnitude < 20.0f) rig.AddForce(-speed * transform.forward, ForceMode.Impulse);	//一定速度まで加速
				else a = false;																						//加速フラグを下げる
			}
			else if (rig.velocity.magnitude < 1.0f) { isMove = false; }												//移動フラグを下す
			else rig.AddForce(stopSpeed * transform.forward, ForceMode.Force);										//減速
		}
		else
		{
			//一定時間止まる
			if (time > maxTime)
			{
				isMove = true;
				a = true;
				time = 0.0f;
				var aim = (pM.PlayerPosition - transform.position)*-1.0f;
				d= Quaternion.LookRotation(aim);
				if ((d.x > -60.0f && d.x < 60.0f) && (d.y > -60.0f && d.y < 60.0f))
				{
					transform.localRotation = Quaternion.LookRotation(aim);
				}
			}
			time += Time.deltaTime;

			
		}
		if (transform.position.z <pM.PlayerPosition.z-1.0f) Destroy(gameObject);
	}
	private void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag=="bullet")
		Destroy(gameObject);
	}
}

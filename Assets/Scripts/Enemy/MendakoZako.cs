using UnityEngine;

public class MendakoZako : MonoBehaviour
{
	private Rigidbody rig;
	[SerializeField]
	private float speed;
	[SerializeField]
	private float stopSpeed;
	private PlayerMaster pM = new PlayerMaster();
	private bool isMove;
	[SerializeField]
	private float maxTime;
	private float time;
	private bool a;
	[SerializeField]
	private float destroyPos;
	[SerializeField]
	private float rotedis;
	[SerializeField]
	private float disAbs;
	[SerializeField]
	private float destroyTime;
	void Start()
	{
		rig = GetComponent<Rigidbody>();
		isMove = true;
		a = true;
		time = 0.0f;
		var aim = (pM.PlayerPosition - transform.position)*-1.0f;
		transform.localRotation = Quaternion.LookRotation(aim, transform.up);
	}
	void Update()
	{
		destroyTime += Time.deltaTime;
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
				disAbs = Vector3.Distance(transform.TransformPoint(transform.position), pM.PlayerPosition);
				//一定距離まで向きを変える
				if (disAbs>rotedis)
				{
					Debug.Log(disAbs);
					transform.rotation = Quaternion.LookRotation(aim);
				}
			}
			time += Time.deltaTime;
		}
		if (transform.position.z < pM.PlayerPosition.z - destroyPos) Destroy(gameObject);
		if (destroyTime > 9.0f) Destroy(gameObject);
	}
	private void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag=="bullet")
		Destroy(gameObject);
	}
}
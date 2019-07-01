using UnityEngine;
[ExecuteInEditMode, DisallowMultipleComponent]
public class FollowingCamera : MonoBehaviour
{
	public GameObject target; // an object to follow
	public Vector3 offset; // offset form the target object

	[SerializeField] private float distance = 4.0f; // distance from following object

	[SerializeField] private float zAngle = 45.0f;
	[SerializeField] private float yAngle = 45.0f;
	[SerializeField] private float minXDistance = -5.0f;
	[SerializeField] private float maxXDistance = 5.0f;
	[SerializeField] private float minYDistance = -2.5f;
	[SerializeField] private float maxYDistance = 2.5f;
	[SerializeField] private float minZDistance = 1.0f;
	[SerializeField] private float maxZDistance = 7.0f;

	[SerializeField] private float xDistance = 0.0f;
	[SerializeField] private float yDistance = 0.0f;
	[SerializeField] private float zDistance = 0.0f;

	private float inputHorizontal;
	private float inputVertical;
	
	private Vector3 moveVec;

	void LateUpdate()
	{
		var lookAtPos = target.transform.position + offset;
		updatePosition(lookAtPos);
	}

	private void Update()
	{
		PlayerControl();
	}

	void PlayerControl()
	{
		inputHorizontal = Input.GetAxis("Horizontal");
		inputVertical = Input.GetAxis("Vertical");

		moveVec = Vector3.zero;

		// カメラの方向を加味して移動
		moveVec = Camera.main.transform.up * inputVertical + Camera.main.transform.right * inputHorizontal;

		// カメラから見たPlayerの座標を計算
		xDistance = xDistance + moveVec.x;
		yDistance = yDistance + moveVec.y;
		zDistance = zDistance + moveVec.z;
		
		// カメラの視認範囲内に移動制限
		MoveRestriction();
	}

	void updatePosition(Vector3 lookAtPos)
	{
		//yAngle = Camera.main.transform.localEulerAngles.x;
		zAngle = Camera.main.transform.localEulerAngles.y;
		var da = yAngle * Mathf.Deg2Rad;// Camera.main.transform.localEulerAngles.z * Mathf.Deg2Rad;//Mathf.Atan2(Camera.main.transform.rotation.x, Camera.main.transform.rotation.z);/*Mathf.Atan2(transform.position.x - Camera.main.transform.position.x,transform.position.z - Camera.main.transform.position.z);*/
		var dp = zAngle * Mathf.Deg2Rad;// Camera.main.transform.localEulerAngles.z * Mathf.Deg2Rad;//Mathf.Atan2(Camera.main.transform.rotation.x, Camera.main.transform.rotation.z);/*Mathf.Atan2(transform.position.x - Camera.main.transform.position.x,transform.position.z - Camera.main.transform.position.z);*/
		//var dp = Camera.main.transform.localEulerAngles.y * Mathf.Deg2Rad;//Mathf.Atan2(Camera.main.transform.rotation.y, Camera.main.transform.rotation.z);/*-Mathf.Atan2( transform.position.y - Camera.main.transform.position.y, transform.position.z - Camera.main.transform.position.z);*/
																  //Debug.Log("da" + da*Mathf.Rad2Deg + "dp" + dp*Mathf.Rad2Deg);
		transform.position = new Vector3(
			lookAtPos.x + xDistance + distance * Mathf.Sin(dp) * Mathf.Cos(da),     // 球面座標
			lookAtPos.y + zDistance + distance * Mathf.Sin(dp) * Mathf.Sin(da),
			lookAtPos.z + yDistance + distance * Mathf.Cos(dp)
			);
	}

	void  MoveRestriction()
	{
		// 三次元の最小値、最大値をカメラから見た相対座標の値に変換
		xDistance = Mathf.Clamp(xDistance, minXDistance, maxXDistance);
		yDistance = Mathf.Clamp(yDistance, minYDistance, maxYDistance);
		zDistance = Mathf.Clamp(zDistance, minZDistance, maxZDistance);
	}
}
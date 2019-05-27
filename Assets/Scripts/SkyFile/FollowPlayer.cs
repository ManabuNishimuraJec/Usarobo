using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
	[SerializeField]
	private Transform target;    // ターゲットへの参照

	[SerializeField]
	private Vector3 offset;

	[SerializeField]
	private float distance = 4.0f; // distance from following object
	[SerializeField]
	private float azimuthalAngle = 45.0f; // angle with x-axis
	[SerializeField]
	private float polarAngle = 45.0f; // angle with y-axis

	[SerializeField]
	private float mouseXSensitivity = 5.0f;
	[SerializeField]
	private float mouseYSensitivity = 5.0f;

	[SerializeField]
	private float minPolarAngle = 5.0f;
	[SerializeField]
	private float maxPolarAngle = 75.0f;

	

	void Start()
	{
		
	}

	void Update()
    {
		if(Input.GetMouseButton(1))
		{
			UpdateAngle(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
		}
		var lookAtPos = target.transform.position + offset;
		UpdatePosition(lookAtPos);
		transform.LookAt(lookAtPos);

    }

	void UpdateAngle(float x,float y)
	{
		x = azimuthalAngle - x * mouseXSensitivity;
		azimuthalAngle = Mathf.Repeat(x, 360);

		y = polarAngle + y * mouseYSensitivity;
		polarAngle = Mathf.Clamp(y, minPolarAngle, maxPolarAngle);

	}

	void UpdatePosition(Vector3 lookAtPos)
	{
		var da = azimuthalAngle * Mathf.Deg2Rad;
		var dp = polarAngle * Mathf.Deg2Rad;
		// 球面座標系を使用
		// x = r sinθ cosφ
		// y = r sinθ sinφ
		// z = r cosθ
		transform.position = new Vector3(
			lookAtPos.x + distance * Mathf.Sin(dp) * Mathf.Cos(da),
			lookAtPos.y + distance * Mathf.Cos(dp),
			lookAtPos.z + distance * Mathf.Sin(dp) * Mathf.Sin(da));
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Cinemachine.CinemachineVirtualCamera))]

public class CinemaControl : MonoBehaviour
{
	private Cinemachine.CinemachineVirtualCamera vcam;

	public const int PRIORITY_HIGH = 11; // 優先度 高
	public const int PRIORITY_LOW = PRIORITY_HIGH - 1; // HIGHより低ければなんでもOK

	// コライダーに当たったら
	void OnTriggerEnter(Collider col)
	{
		// 優先度マックスにする
		SetPriority(PRIORITY_HIGH);
	}

	// コライダーから外れたら
	void OnTriggerExit(Collider col)
	{
		// 優先度を最低にする
		SetPriority(PRIORITY_LOW);
	}

	// 優先度設定
	private void SetPriority(int priority)
	{
		// virtualCameraが取得できていなければ取得する
		if (vcam == null)
		{
			vcam = GetComponent<Cinemachine.CinemachineVirtualCamera>();
		}
		// virtualCameraがあれば優先度を代入
		if (vcam)
		{
			vcam.Priority = priority;
		}
	}
}

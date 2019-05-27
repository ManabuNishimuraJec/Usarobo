using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class CameraTriigerSignal : MonoBehaviour
{
	// 信号を送る準備
	private Subject<Unit> triggerSubject = new Subject<Unit>();

	// 信号を送る処理
	public IObservable<Unit> onTriggerMessage
	{
		get { return triggerSubject; }
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "ChasePoint")
		{
			Debug.Log("当たった");
			// 親に信号を送る
			triggerSubject.OnNext(Unit.Default);
		}
	}
}

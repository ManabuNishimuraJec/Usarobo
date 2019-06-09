using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class CameraTriigerSignal : MonoBehaviour
{
	// 信号を送る準備
	private Subject<ChasePointControl> triggerSubject = new Subject<ChasePointControl>();

	// 信号を送る処理
	public IObservable<ChasePointControl> onTriggerMessage
	{
		get { return triggerSubject; }
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "ChasePoint")
		{
			// 親に信号を送る
			triggerSubject.OnNext(other.GetComponent<ChasePointControl>());
		}
	}
}

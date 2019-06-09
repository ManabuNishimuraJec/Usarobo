using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class BossStage : MonoBehaviour
{
	//	Masterを宣言
	PlayerMaster pMaster = new PlayerMaster();

	//	位置情報保存用
	Vector3 prevPos;

	//	行動範囲を制限するためのメッセージ
	private Subject<Unit> bornBossSubject = new Subject<Unit>();
	public IObservable<Unit>OnBornBossMessge
	{
		get { return bornBossSubject; }
	}

	//	BOSSを表示させるためのメッセージ
	private Subject<Unit> getBossSubject = new Subject<Unit>();
	public IObservable<Unit>OnBossMessge
	{
		get { return getBossSubject; }
	}

	void Start()
    {
	}
    void Update()
    {
		//	円の計算
		prevPos = pMaster.PlayerPosition;
	}

	private void OnTriggerEnter(Collider other)
	{
		//	Playerと衝突したら
		if (other.tag == ("Player"))
		{
			//	メッセージを送る
			bornBossSubject.OnNext(Unit.Default);		//	行動範囲を制限するためのメッセージを送る
			getBossSubject.OnNext(Unit.Default);        //	BOSSを表示させるためのメッセージを送る
		}
		
	}
}
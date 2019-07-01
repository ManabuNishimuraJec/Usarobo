using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class BossStatus : MonoBehaviour
{
	[SerializeField]
	private float bossHp=3;

	[SerializeField]
	private BossStage bossstage;

	//	BOSS表示のためのメッセージを送る
	private Subject<Unit> bossStatusSubject = new Subject<Unit>();
	public IObservable<Unit> OnBossStatusMessge
	{
		get { return bossStatusSubject; }
	}

	// Start is called before the first frame update
	void Start()
    {
		//	オブジェクトを消しておく
		gameObject.SetActive(false);

		//	Playerがエリア内に入ったらBOSSを表示させる
		bossstage.OnBossMessge.Subscribe(_ =>
		{
			gameObject.SetActive(true);
		});

	}

    // Update is called once per frame
    void Update()
    {
		//	BOSSのHPが0になった場合
		if (bossHp <= 0)
		{
			//	GameObjectを削除
			Destroy(gameObject);

			//	信号を送る
			bossStatusSubject.OnNext(Unit.Default);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		//	Playerと振れた場合
		if (collision.gameObject.tag == "Player")
		{
			//	HPを減少させる
			--bossHp;
		}

	}

}

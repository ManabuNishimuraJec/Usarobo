using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	//	Masterを宣言
    private PlayerMaster playerMaster = new PlayerMaster();

    public GameObject explosion;

    Coroutine coroutine;

	//	絶対値格納用
	Vector3 xyzAbs;

    [SerializeField]
   private float speedParameter = 10;

    void Update()
    {
		xyzAbs.x = Mathf.Abs(this.gameObject.transform.position.x - playerMaster.PlayerPosition.x);
		xyzAbs.y = Mathf.Abs(this.gameObject.transform.position.y - playerMaster.PlayerPosition.y);
		xyzAbs.z = Mathf.Abs(this.gameObject.transform.position.z - playerMaster.PlayerPosition.z);

		//	アイテムとPlayerの距離が10以下の場合
		if(Vector3.Distance(transform.position, playerMaster.PlayerPosition) <= 10)
		{
			//	引き寄せる
			if (coroutine == null)
			{
				coroutine = StartCoroutine(MoveCoroutine());
			}

			//	距離が1以下になったらgameObjectをDestroy
			if (Vector3.Distance(transform.position, playerMaster.PlayerPosition) <= 1)
			{
				Destroy(gameObject);
			}
		}

		IEnumerator MoveCoroutine()
		{
			float speed = speedParameter * Time.deltaTime;

			while (xyzAbs.x > 0 || xyzAbs.y > 0 || xyzAbs.z > 0)
			{
				yield return new WaitForEndOfFrame();
				//	補間を求めてその位置に移動
				gameObject.transform.position = Vector3.MoveTowards(transform.position, playerMaster.PlayerPosition, speed);
			}
		}
    }
}
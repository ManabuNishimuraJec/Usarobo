using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchCharacter : MonoBehaviour
{
	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "Player")
		{
            // 敵キャラクターの状態を取得
            Enemy.EnemyState state = GetComponentInParent<Enemy>().GetState();
            // 敵キャラクターが追いかける状態でなければ追いかける設定に変更
            if(state != Enemy.EnemyState.Chase)
            {
                Debug.Log("プレイヤー検知");
                GetComponentInParent<Enemy>().SetState("chase", col.transform);
            }
		}
	}
}

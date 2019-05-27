using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItem : MonoBehaviour
{
	//	入力用
	//----------------------------------
	[SerializeField]
	private GameObject item;
	[SerializeField]
	private int itemVolume = 0;
	//----------------------------------

	//	カウント用
	private int cnt = 0;

	//	Stageの大きさ格納用
	private Vector3 stageSize;

	void Start()
    {
	}
    void Update()
    {
		//	アイテムアイテム生成
		if (itemVolume > cnt)
		{
			//	アイテム出現の範囲を決める
			stageSize.x = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);
			stageSize.y = Random.Range(-transform.localScale.y / 2, transform.localScale.y / 2);
			stageSize.z = Random.Range(-transform.localScale.z / 2, transform.localScale.z / 2);

			//	ランダム生成
			var obj = Instantiate(item, new Vector3(stageSize.x + transform.position.x, stageSize.y + 5, stageSize.z + transform.position.z), Quaternion.identity) as GameObject;
			obj.transform.rotation = Quaternion.Euler(0, 0, 90f);

			++cnt;		//	カウント
		}
	}
}
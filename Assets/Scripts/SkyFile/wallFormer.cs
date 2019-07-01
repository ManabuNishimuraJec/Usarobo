using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallFormer : MonoBehaviour
{
	// 障害物のリスト
	[SerializeField]
	private List<GameObject> wallList = new List<GameObject>();   // wall用のリスト

	private int rand;

	private float time;     // 時間加算用
	[SerializeField]
	private float timeMax;  // 生成時間設定用

    void Start()
    {
        // ０～３秒の整数を生成時間設定に代入
        timeMax = 1.0f;
    }

    void Update()
    {
        // 時間を加算
        time += Time.deltaTime;

        if(time > timeMax)
        {
			rand = Random.Range(0, 4);

			switch (rand)
			{
				// 障害物生成
				case 0:
					Instantiate(wallList[0], transform.position, Quaternion.identity);
					break;
				case 1:
					Instantiate(wallList[1], transform.position, Quaternion.identity);
					break;
				case 2:
					Instantiate(wallList[2], transform.position, Quaternion.identity);
					break;
				case 3:
					Instantiate(wallList[3], transform.position, Quaternion.identity);
					break;
			}
			
            // タイムリセット
            time = 0.0f;

            // 生成時間変更
            timeMax =　timeMax = Random.Range(1.5f, 3.0f);
        }
    }
}

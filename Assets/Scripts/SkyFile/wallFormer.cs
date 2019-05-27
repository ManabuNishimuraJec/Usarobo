using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallFormer : MonoBehaviour
{
    // 障害物プレハブを格納
	[SerializeField]
    private GameObject Wall;
	private Rigidbody rid;

    [SerializeField]
    private float time;     // 時間加算用
    private float timeMax;  // 生成時間設定用

    void Start()
    {
		rid = GetComponent<Rigidbody>();

        // ０～３秒の整数を生成時間設定に代入
        timeMax = 1.0f;
    }

    void Update()
    {
        // 時間を加算
        time += Time.deltaTime;

		// 自身のZ座標を加算していく
		rid.velocity = new Vector3(0.0f, 0.0f, 0.0f);

        if(time > timeMax)
        {
            // 障害物生成
            Instantiate(Wall, transform.position, Quaternion.identity);

            // タイムリセット
            time = 0.0f;

            // 生成時間変更
            timeMax =　timeMax = Random.Range(0.5f, 1.5f);
        }
    }
}

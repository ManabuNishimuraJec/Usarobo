// カメラが追うべき対象の処理

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    // メッシュレンダラー
    private MeshRenderer meshrender;

    [SerializeField]
    private PlayerChasePointSettings playerChasePointSettings;  //	PlayerChasePointSettings

    private Vector3 prevTarPos;             // 達したポイントの座標格納用
    private Vector3 nextTarPos;             // 次に向かうポイントの座標格納用
    private int pointNum = 0;               // ポイントに対応した要素番号
    private GameObject chasePoint;          // ポイントの情報用変数
    private bool lastChasePointFlag = true; // 最終ポイントに達した時用のフラグ
    [SerializeField]
    private float chaseSpeed;               // ルート追跡速度
    private Vector3 heading;                // 向かうべき方向
    [SerializeField]
    private bool MeshSwitch;

    void Start()
    {
        if(MeshSwitch)
        {
            // メッシュレンダラー取得
            MeshRenderer meshrender = GetComponent<MeshRenderer>();
            // 処理が始まったら非表示
            meshrender.enabled = false;
        }
        
        if (0 < playerChasePointSettings.GetChasePointCount())
		{
			// ChasePointの情報を知る
			chasePoint = playerChasePointSettings.GetChasePoint(pointNum);
			// 要素番号０のポイントの座標を初期地点として格納
			prevTarPos = chasePoint.transform.position;
			// 要素番号０＋１のポイントの座標を次に向かう座標として格納
			nextTarPos = playerChasePointSettings.GetChasePoint(pointNum + 1).transform.position;
			// 初期地点にワープ
			transform.position = prevTarPos;
		}
	}
	
    void Update()
    {
        // ラストに達していなければポイントを追う
        if (lastChasePointFlag)
        {
            PointChase(heading);               // 実際に追う処理呼び出し
        }
    }

	private void OnTriggerEnter(Collider other)
	{
		// PlayerChasePointに当たった場合
		if (other.tag == "PlayerChasePoint")
		{
			Debug.Log("pointNum " + pointNum);
			// 最後のポイントかどうかのフィルター
			if ((pointNum + 1) < playerChasePointSettings.GetChasePointCount())
			{
				// 向かっていたポイントを一番最近に通ったポイントに更新
				prevTarPos = transform.position;

				// 次に向かうポイントを更新
				nextTarPos = playerChasePointSettings.GetChasePoint(pointNum + 1).transform.position;

				// ポイントナンバーを更新
				pointNum++;

                // 計算呼び出し
                heading = ChaseVectorCalculator(prevTarPos, nextTarPos);
            }
			else
			{
				// ラストに達したというフラグを立てる
				lastChasePointFlag = false;
			}
		}
	}


    public Vector3 ChaseVectorCalculator(Vector3 prevTarPos,Vector3 nextTarPos)
    {
        // prevTarPosからnextTarPosまでの方向を計算
        var heading = (nextTarPos - prevTarPos);
        // 計算した方向を返す
        return heading;
    }

    // ポイントを追う
    public void PointChase(Vector3 heading)
    {
        // 方向に向かって進む処理
        //次点方向に進む
        transform.Translate(heading * chaseSpeed * Time.deltaTime, Space.World);
    }
}

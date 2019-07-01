// ポイントを辿るカメラのスクリプト
// 後々、シーン再生直後にポイントの座標すべてを記録して、
// ポイントオブジェクト自体は削除するスクリプトに改良

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ChaseCamera : MonoBehaviour
{
	[SerializeField]
	private float chaseSpeed;               // 📷の移動スピード
	private Vector3 prevTarPos;				// 達したポイントの座標格納用
	private Vector3 nextTarPos;				// 次に向かうポイントの座標格納用
	private int pointNum　= 0;				// ポイントに対応した要素番号
	private Vector3 heading;				// 向かうべき方向
	private bool lastChasePointFlag = true; // 最終ポイントに達した時用のフラグ

	[SerializeField]
	private CameraTriigerSignal cSignal;

	[SerializeField]
	private ChasePointSettings chasePointSettings;  // ChasePointSettingsスクリプト
	[SerializeField]
	private GameObject target;						//　プレイヤーの情報

    void Start()
    {
		// ChasePointの情報を知る
		var chasePoint = chasePointSettings.GetChasePoint(pointNum);	

		// ChasePointが存在する場合
		if(0 < chasePointSettings.GetChasePointCount())
		{
			// 要素番号０のポイントの座標を初期地点として格納
			prevTarPos = chasePoint.transform.position;
			
			// 要素番号０＋１のポイントの座標を次に向かう座標として格納
			nextTarPos = chasePointSettings.GetChasePoint(pointNum + 1).transform.position;

			// 計算呼び出し
			heading = ChaseVectorCalculator(prevTarPos, nextTarPos);

			// 自身を一番最初のスタートポイントに転送
			transform.position = prevTarPos;
		}
		
		// 信号を感知
		cSignal.onTriggerMessage.Subscribe(rot =>
		{
			// ポイントが最終地点であるかでなければ
			if((pointNum+1) < chasePointSettings.GetChasePointCount())
			{
				// 向かっていたポイントを一番最近に通ったポイントに更新
				prevTarPos = transform.position;

				// 次に向かうポイントを更新
				nextTarPos = chasePointSettings.GetChasePoint(pointNum + 1).transform.position;

				// ポイントナンバーを更新
				pointNum++;
				
				// 計算呼び出し
				heading = ChaseVectorCalculator(prevTarPos, nextTarPos);
			}
			else
			{
				// ラストに達したというフラグを立てる
				lastChasePointFlag = false;
				Debug.Log(lastChasePointFlag);
			}
		});
	}

	void LateUpdate()
	{
	    PointChase(heading);               // 実際に追う処理呼び出し
	}

	// 進む方向を計算する
	public Vector3 ChaseVectorCalculator(Vector3 prevTarPos,Vector3 nextTarPos)     
	{
		// prevTarPosからnextTarPosまでの方向を計算
		var heading = (nextTarPos - prevTarPos);
		// 計算した方向を返す
		return heading;
	}
	
	public Vector3 TargetVectorCaluculator(Vector3 myPos,Vector3 targetPos)
	{
		var targetVector = (targetPos - myPos);
		return targetVector;
	}

	// ポイントを追う
	public void PointChase(Vector3 heading)				
	{
		// 方向に向かって進む処理
		//割り出した方向に向く
		transform.rotation = Quaternion.LookRotation(TargetVectorCaluculator(transform.position,target.transform.position));

        // ラストに達していなければ実際にポイントを追う（移動する）
        if (lastChasePointFlag)
        {
            //次点方向に進む
            transform.Translate(heading * chaseSpeed * Time.deltaTime, Space.World);
        }
	}
}

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
	private float chaseSpeed;           // 📷の移動スピード
	private Vector3 prevTarPos;			// 達したポイントの座標格納用
	private Vector3 nextTarPos;			// 次に向かうポイントの座標格納用
	private int pointNum　= 0;			// ポイントに対応した要素番号
	private Vector3 heading;            // 向かうべき方向

	[SerializeField]
	private CameraTriigerSignal cSignal;

	[SerializeField]
	private ChasePointSettings chasePointSettings;	// ChasePointSettingsスクリプト

    void Start()
    {
		var chasePoint = chasePointSettings.GetChasePoint(pointNum);	

		// 要素番号０のポイントの座標を初期地点として格納
		prevTarPos = chasePoint.transform.position;
		Debug.Log("start時点でのprevTarPos" + prevTarPos);

		// 要素番号０＋１のポイントの座標を次に向かう座標として格納
		nextTarPos = chasePointSettings.GetChasePoint(pointNum + 1).transform.position;
		Debug.Log("start時点でのnextTarPos" + nextTarPos);

		// 計算呼び出し
		heading = ChaseVectorCalculator(prevTarPos, nextTarPos);

		// 自身を一番最初のスタートポイントに転送
		transform.position = prevTarPos;

		// 信号を感知
		cSignal.onTriggerMessage.Subscribe(_ =>
		{
			Debug.Log("ポイントに達した");
			// 向かっていたポイントを一番最近に通ったポイントに更新
			prevTarPos = transform.position;
			Debug.Log("ポイントに達した時のprevTarPos" + prevTarPos);

			// 次に向かうポイントを更新
			nextTarPos = chasePointSettings.GetChasePoint(pointNum + 1).transform.position;
			Debug.Log("ポイントに達した時のnextTarPos" + nextTarPos);

			// ポイントナンバーを更新
			pointNum++;

			// 計算呼び出し
			heading = ChaseVectorCalculator(prevTarPos, nextTarPos);
		});
	}
	
    void Update()
	{
		Debug.Log("transform.position" + transform.position);
		PointChase(heading);               // 実際に追う処理呼び出し
	}

	// 進む方向を計算する
	public Vector3 ChaseVectorCalculator(Vector3 prevTarPos,Vector3 nextTarPos)     
	{
		// prevTarPosからnextTarPosまでの方向を計算
		var heading = (nextTarPos - prevTarPos).normalized;
		// 計算した方向を返す
		return heading;
	}

	// ポイントを追う
	public void PointChase(Vector3 heading)				
	{
		// 方向に向かって進む処理
		// 割り出した方向に向く
		transform.rotation = Quaternion.LookRotation(heading);
		// 向いている方向に進む
		transform.Translate(heading * chaseSpeed *Time.deltaTime,Space.World);
	}
}

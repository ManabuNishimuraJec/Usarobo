using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CorsorControl : MonoBehaviour
{
	// [SerializeField]
	// private canonControl cControl;

	[SerializeField]
	private List<canonControl> cControl = new List<canonControl>();

	// マウスの位置座標
	private Vector3 position;
	// スクリーン座標をワールド座標に変換した位置座標
	private Vector3 screenToWorldPointPosition;
	[SerializeField]
	private float CorsorZ;          // InspectorでZ座標変更用

	void Start()
	{
		foreach (canonControl cCon in cControl)
		{
			cCon.OnWallPosMessage.Subscribe(wallPos => // 信号キャッチ
			{
				// 信号に乗ってわたってきたwallの座標を自身の座標に代入
				transform.position = wallPos + new Vector3(0.0f, 0.0f, -5.0f);
			});
		}
		//cControl.OnWallPosMessage.Subscribe(wallPos => // 信号キャッチ
		//{
		//	// 信号に乗ってわたってきたwallの座標を自身の座標に代入
		//	transform.position = wallPos + new Vector3(0.0f,0.0f,-2.0f);
		//});
	}

	void Update()
    {
		// positionでマウス位置座標を取得する
		position = Input.mousePosition;
		// Z軸修正
		position.z = CorsorZ;
		// マウス位置座標をスクリーン座標からワールド座標に変換する
		screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);
		// ワールド座標に変換されたマウス座標を代入
		transform.position = screenToWorldPointPosition;
	}
}

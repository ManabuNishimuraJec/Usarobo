// カメラが追うポイントの制御

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePointControl : MonoBehaviour
{
	// メッシュレンダラー
	private MeshRenderer meshrender;

	void Start()
    {
		// メッシュレンダラー取得
		MeshRenderer meshrender = GetComponent<MeshRenderer>();
		// 処理が始まったら非表示
		meshrender.enabled = false;
	}
}

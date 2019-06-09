// カメラが追うポイントの制御

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePointControl : MonoBehaviour
{
	// メッシュレンダラー
	private MeshRenderer meshrender;
	// カメラに渡したい角度を格納
	[SerializeField]
	private Quaternion cameraVector = new Quaternion(0.0f,0.0f,0.0f,0.0f);

	public Quaternion CameraVector { get => cameraVector; }

	// この下がGet
	/*
	public Quaternion SampleCameraVector()
	{
		return cameraVector;
	}

	// この下がSET
	public void SampleCameraVector(Quaternion value)
	{
		cameraVector = value;
	}
	*/

	void Start()
    {
		// メッシュレンダラー取得
		MeshRenderer meshrender = GetComponent<MeshRenderer>();
		// 処理が始まったら非表示
		meshrender.enabled = false;
	}
}

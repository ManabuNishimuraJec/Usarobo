// カメラがたどるルートを可視化するスクリプト

using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
public class ChaseRoot : MonoBehaviour
{
#if UNITY_EDITOR
	static double waitTime = 0;
	[SerializeField]
	private ChasePointSettings chasePointSettings;		// ChasePointSettings 
	bool startFlag = true;								// スタートメソッドフラグ
	LineRenderer line;									// LineRenderer

	void OnEnable()
	{
		waitTime = EditorApplication.timeSinceStartup;
		EditorApplication.update += EditorUpdate;
	}

	void OnDisable()
	{
		EditorApplication.update -= EditorUpdate;
	}

	// 更新処理
	void EditorUpdate()
	{
		//if (startFlag)
		{
			foreach (var go in Selection.gameObjects)
			{
				// 選択中のオブジェクトのみ更新
				if (go == this.gameObject)
				{
					startFlag = false;
					// １／６０秒に１回更新
					if ((EditorApplication.timeSinceStartup - waitTime) >= 0.01666f)
					{
						line = GetComponent<LineRenderer>();	// LineRenderer取得
						
						// 君だけの更新処理を書こう！
						UpdateChaseRoot();

						SceneView.RepaintAll(); // シーンビュー更新
						waitTime = EditorApplication.timeSinceStartup;
						break;
					}
				}
			}
		}
	}
#endif
	// ルート可視化処理
	void UpdateChaseRoot()
	{
		// ラインを引く数を取得
		line.positionCount = chasePointSettings.GetChasePointCount();
		// ポイントの座標をまとめて配列で取得
		line.SetPositions(chasePointSettings.GetChasePointVecter());
	}
}
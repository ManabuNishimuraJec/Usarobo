using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
public class wierframe : MonoBehaviour
{
#if UNITY_EDITOR
	static double waitTime = 0;

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
		foreach (var go in Selection.gameObjects)
		{
			// 選択中のオブジェクトのみ更新
			if (go == this.gameObject)
			{
				// １／６０秒に１回更新
				if ((EditorApplication.timeSinceStartup - waitTime) >= 1f)
				{
					// 君だけの更新処理を書こう！
					WierFrameStart();

					SceneView.RepaintAll(); // シーンビュー更新
					waitTime = EditorApplication.timeSinceStartup;
					break;
				}
			}
		}
	}
#endif
	// Use this for initialization
	void Start()
	{
		
	}
	void WierFrameStart()
	{
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh = GetComponent<MeshFilter>().mesh;

		var triangleList = mesh.GetIndices(0);
		var triangleNum = triangleList.Length / 3;
		var lineList = new int[triangleNum * 6];
		for (var n = 0; n < triangleNum; ++n)
		{
			var t = n * 3;
			var l = n * 6;
			lineList[l + 0] = triangleList[t + 0];
			lineList[l + 1] = triangleList[t + 1];

			lineList[l + 2] = triangleList[t + 1];
			lineList[l + 3] = triangleList[t + 2];

			lineList[l + 4] = triangleList[t + 2];
			lineList[l + 5] = triangleList[t + 0];
		}
		mesh.SetIndices(lineList, MeshTopology.Lines, 0);
	}
}
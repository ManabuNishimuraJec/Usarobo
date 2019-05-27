// ポイントを作るためのGUIをつくるスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChasePointSettings))] // 拡張するクラスを指定
public class PointSettingsEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		ChasePointSettings chasepointSettings = target as ChasePointSettings;

		// PointEditボタンをインスペクターに作成
		if(GUILayout.Button("PointEdit"))
		{
			// chasePointSettingsのPointEditクラスを呼び出す
			chasepointSettings.PointEdit();
		}

		// PointDeleteボタンをインスペクターに作成
		if (GUILayout.Button("PointDelete"))
		{
			// chasePointSettingsのPointDeleteクラスを呼び出す
			chasepointSettings.PointDelete();
		}

		// PointAllDeleteボタンをインスペクターに作成
		if (GUILayout.Button("PointAllDelete"))
		{
			// chasePointSettingsのPointAllDeleteクラスを呼び出す
			chasepointSettings.PointAllDelete();
		}
	}
}

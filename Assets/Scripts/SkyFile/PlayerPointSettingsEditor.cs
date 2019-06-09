// ポイントを作るためのGUIをつくるスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerChasePointSettings))] // 拡張するクラスを指定
public class PlPointSettingsEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		PlayerChasePointSettings chasepointSettings = target as PlayerChasePointSettings;

		// PointEditボタンをインスペクターに作成
		if (GUILayout.Button("PointEdit"))
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

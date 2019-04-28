using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyEditScript))] // 拡張するクラスを指定
public class EnemyEditScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EnemyEditScript enemyEdit = target as EnemyEditScript;
        // ボタンを表示
        if(GUILayout.Button("EnemySearch"))
        {
            enemyEdit.EnemySearch();
        }

        // 初期化
        if(GUILayout.Button("Clear"))
        {
            enemyEdit.EnmeyClear();
        }

        // ファイル書き込み
        if (GUILayout.Button("WriteEnemyData"))
        {
            enemyEdit.EnemyCSVWrite();
        }

        // ファイル読み込み
        if (GUILayout.Button("ReadEnemyData"))
        {
            enemyEdit.EnemyCSVRead();
        }
    }
}

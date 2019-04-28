using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class EnemyEditScript : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> EnemyList = new List<GameObject>();

    public struct EnemyData
    {
        public string prefabName;
        public string prefabPath;
        public Vector3 position;
        public Quaternion rotation; 
    }

    private List<EnemyData> enemyDataList = new List<EnemyData>();

    public void EnemySearch()
    {
        GameObject[] enemyObj = GameObject.FindGameObjectsWithTag("Enemy");
        EnemyList.Clear();
        foreach (var obj in enemyObj)
        {

            EnemyList.Add(obj);
        }
    }

    public void EnmeyClear()
    {
        foreach(var obj in EnemyList)
        {
            GameObject.DestroyImmediate(obj);
        }
        EnemyList.Clear();
    }

    public void EnemyCSVWrite()
    {
        if(EnemyList.Count == 0)
        {
            Debug.LogWarning("敵データがありません:EnemyEditScript");
            return;
        }
        // ファイル書き出し
        StreamWriter sw = new StreamWriter(@"Assets\EnemyData\EnemyData.csv", false, Encoding.GetEncoding("Shift_JIS"));
        string[] s1 = { "EnemyPrefabName","EnemyPrefabPath","EnemyPosX", "EnemyPosY", "EnemyPosZ", "EnemyRotationX", "EnemyRotationY", "EnemyRotationZ", "EnemyRotationW" };
        // 文字列の結合
        string s2 = string.Join(",", s1);
        // 書き出し
        sw.WriteLine(s2);

        // データ出力
        foreach(var obj in EnemyList)
        {
            Object prefab = PrefabUtility.GetPrefabParent(obj);
            string[] str =  {
                prefab.name.ToString(),
                AssetDatabase.GetAssetPath(prefab),
                obj.transform.position.x.ToString(),
                obj.transform.position.y.ToString(),
                obj.transform.position.z.ToString(),
                obj.transform.rotation.x.ToString(),
                obj.transform.rotation.y.ToString(),
                obj.transform.rotation.z.ToString(),
                obj.transform.rotation.w.ToString()
            };
            string str2 = string.Join(",", str);
            sw.WriteLine(str2);
        }

        sw.Close();
    }

    public void EnemyCSVRead()
    {
        enemyDataList.Clear();
        StreamReader sr = new StreamReader(@"Assets\EnemyData\EnemyData.csv",Encoding.GetEncoding("Shift_JIS"));
        string line;
        List<string[]> listData = new List<string[]>();

        // 先頭行スキップ
        line = sr.ReadLine();
        while ((line = sr.ReadLine()) != null)
        {
            Debug.Log(line);
            var data = line.Split(',');
            listData.Add(data);
        }

        foreach(var data in listData)
        {
            EnemyData edata;
            edata.prefabName = data[0];
            edata.prefabPath = data[1];
            edata.position = new Vector3(float.Parse(data[2]),
                                         float.Parse(data[3]),
                                         float.Parse(data[4]));
            edata.rotation = new Quaternion(float.Parse(data[5]),
                                            float.Parse(data[6]),
                                            float.Parse(data[7]),
                                            float.Parse(data[8]));

            enemyDataList.Add(edata);
        }
        CreateEnemy();
    }
    public void CreateEnemy()
    {
        EnemyList.Clear();
        foreach(var obj in enemyDataList)
        {
            string[] path = obj.prefabPath.Split('.');
            GameObject prefab = (GameObject)Resources.Load(path[0].Replace("Assets/Resources/","" ));
            var eObj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            EnemyList.Add(eObj);
            eObj.transform.position = obj.position;
            eObj.transform.rotation = obj.rotation;
        }
    }
}

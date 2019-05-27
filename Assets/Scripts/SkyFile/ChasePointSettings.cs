// カメラが追う用のポイントの情報を管理するスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePointSettings : MonoBehaviour
{
	[SerializeField]
	private GameObject chasePointPrefab;								// Prefab格納用

	[SerializeField]
	private List<GameObject> chasePointList = new List<GameObject>();   // chasePoint用のリスト

	public List<GameObject> ChasePointList { get => chasePointList; }

	// リストの中身を返す関数
	public GameObject GetChasePoint (int num)
	{
		if (num == 0 && ChasePointList.Count == 0)
		{
			return null;
		}
		return ChasePointList[num];
	}

	public Vector3[] GetChasePointVecter()
	{
		Vector3[] pos = new Vector3[chasePointList.Count];
		int cont = 0;
		foreach(var obj in chasePointList)
		{
			pos[cont] = obj.transform.position;
			cont++;
		}
		return pos;
	}

	// リストの要素数を返す関数
	public int GetChasePointCount()
	{
		return ChasePointList.Count;
	}
	public void PointEdit()			// ポイントを生成
	{
		// Obj変数
		GameObject obj;

		// Obj変数に生成したポイントの情報を格納
		if(ChasePointList.Count == 0)// 要素数が０の時のみの生成
		{
			// ポイントを(0,0,0)の地点に生成
			obj = Instantiate(chasePointPrefab, transform.position, transform.rotation)/* as GameObject*/;
		}
		else						 // 要素がある時の生成
		{
			// 最新の要素番号の要素の場所に生成する
			obj = Instantiate(chasePointPrefab, ChasePointList[ChasePointList.Count - 1].transform.position, transform.rotation) /*as GameObject*/;
		}
		// 生成したポイントをリストに格納
		ChasePointList.Add(obj);
	}

	public void PointDelete()			// ポイントを削除
	{
		// ポイントが1個以上ある時のみ処理
		if(ChasePointList.Count > 0)
		{
			// 一番最近に作ったポイントオブジェクトを削除
			GameObject.DestroyImmediate(ChasePointList[ChasePointList.Count - 1]);
			// 一番最近に作ったポイントリストを削除
			ChasePointList.RemoveAt(ChasePointList.Count - 1);
		}
	}

	public void PointAllDelete()		// ポイントを全て削除
	{
		// ポイントが1個以上ある時
		if(ChasePointList.Count > 0)
		{
			// ポイントオブジェクトをすべて削除する
			foreach (var obj in ChasePointList)
			{
				GameObject.DestroyImmediate(obj);
			}
			// ポイントを全て削除する
			ChasePointList.Clear();
		}
	}
}

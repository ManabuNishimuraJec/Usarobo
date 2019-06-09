using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseRootTest : MonoBehaviour
{
	[SerializeField]
	private ChasePointSettings chasePointSettings;      // ChasePointSettings 
	[SerializeField]
	private PlayerChasePointSettings playerChasePointSettings;

	void OnDrawGizmos()
	{
		for (int i = 0; i < (chasePointSettings.GetChasePointCount() - 1); i++)
		{
			Gizmos.color = Color.red;       // Rayのカラーを赤に変更
			// 線分をポイント間で引く
			Gizmos.DrawLine(chasePointSettings.ChasePointList[i].transform.position, chasePointSettings.ChasePointList[i + 1].transform.position);
		}

		for (int i = 0; i < (playerChasePointSettings.GetChasePointCount() - 1); i++)
		{
			Gizmos.color = Color.blue;       // Rayのカラーを赤に変更
			// 線分をポイント間で引く
			Gizmos.DrawLine(playerChasePointSettings.ChasePointList[i].transform.position, playerChasePointSettings.ChasePointList[i + 1].transform.position);
		}
	}
}

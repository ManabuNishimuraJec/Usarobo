using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : MonoBehaviour
{

	// 初期位置
	private Vector3 startPosition;

	// 目的地
	private Vector3 destination;

    void Start()
    {
		startPosition = transform.position;
		//SetDestination(transform.position);
    }

	public void CreateRandomPosition()
	{
		// ランダム位置を得る
		var randDestination = Random.insideUnitCircle * 8;

		// 現在地にランダムな位置を足して目的地とする
		SetDestination(startPosition + new Vector3(randDestination.x, 0, randDestination.y));
	}

	public void SetDestination(Vector3 position)
	{
		destination = position;
	}

	public Vector3 GetDestination()
	{
		return destination;
	}
}

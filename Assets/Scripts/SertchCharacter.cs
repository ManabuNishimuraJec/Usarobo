using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchCharacter : MonoBehaviour
{
	private void OnTriggerStay(Collider col)
	{
		if (col.tag == "Player")
		{
			//MoveEnemy.EnemyState state = GetComponentInParent<MoveEnemy>()
		}
	}
}

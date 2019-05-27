using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaster : MonoBehaviour
{
    private static Vector3 destinaion;
	private static float hp;
	private static float moveSpeed;

	public float Hp { get => hp; set => hp = value; }
	public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
	public Vector3 Destinaion { get => destinaion; set => destinaion = value; }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaster : MonoBehaviour
{
    private static Vector3 playerPosition;
    private static bool checkMode;
    private static float bulletPower;
	private static int hp;
	private static int maxHp;
	private static float jumpPower;
	private static Vector3 moveSpeed;
	private static GameObject gameobject;

	public GameObject Gameobject { get => gameobject; set => gameobject = value; }
	public  Vector3 MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
	public int Hp { get => hp; set => hp = value; }
	public int MaxHp { get => maxHp; set => maxHp = value; }
	public float JumpPower { get => jumpPower; set => jumpPower = value; }
	public Vector3 PlayerPosition { get => playerPosition; set => playerPosition = value; }
	public bool CheckMode { get => checkMode; set => checkMode = value; }
	public float BulletPower { get => bulletPower; set => bulletPower = value; }
}

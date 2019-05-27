using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonData : MonoBehaviour
{
	private static bool rayHitFlag;

	public bool RayHitFlag { get => rayHitFlag; set => rayHitFlag = value; }

	private static string raycollitionTag;

	public string RaycollitionTag { get => raycollitionTag; set => raycollitionTag = value; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaster : MonoBehaviour
{
	private static string mapStyle;
	private static Vector3 endLine;
	public string MapStyle { get => mapStyle; set => mapStyle = value; }
	public Vector3 EndLine { get => endLine; set => endLine = value; }
}

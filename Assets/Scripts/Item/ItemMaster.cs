using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMaster : MonoBehaviour
{
	private static int itemName;
	private static string itemNumber;

	public int ItemName { get => itemName; set => itemName = value; }
	public string ItemNumber { get => itemNumber; set => itemNumber = value; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotUnitMaster : MonoBehaviour
{
    private static Quaternion rotation;

    public Quaternion Rotation { get => rotation; set => rotation = value; }
}

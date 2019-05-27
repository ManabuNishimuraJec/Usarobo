using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextField : MonoBehaviour
{
    //private PlayerMaster pMaster = new PlayerMaster();
    private EnemyMaster eMaster = new EnemyMaster();

    private ShotUnitMaster shotMaster = new ShotUnitMaster();

	private CanonData canonData = new CanonData();
    void OnGUI()
    {
        // テキストフィールドを表示する
        GUI.Label(new Rect(10, 10, 200, 200),"DebuData","box");
        GUI.Label(new Rect(13, 25, 200, 20), "Rotation:" + shotMaster.Rotation.ToString());
		GUI.Label(new Rect(13, 40, 200, 20), "RayHitFlag:" + canonData.RayHitFlag.ToString());
		//GUI.Label(new Rect(13, 55, 200, 20), "RayHitTag:" + canonData.RaycollitionTag.ToString());

		//GUI.Label(new Rect(13, 40, 200, 20), "Speed   :" + pMaster.Speed.ToString());*/

	}
}

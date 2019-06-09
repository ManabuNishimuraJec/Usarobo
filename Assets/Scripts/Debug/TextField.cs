using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextField : MonoBehaviour
{
    private PlayerMaster pMaster = new PlayerMaster();
    private EnemyMaster eMaster = new EnemyMaster();

    private ShotUnitMaster shotMaster = new ShotUnitMaster();
    void OnGUI()
    {
        // テキストフィールドを表示する
        GUI.Label(new Rect(10, 10, 200, 200),"DebuData","box");
        GUI.Label(new Rect(13, 25, 200, 20), "Rotation:" + pMaster.ScreenPosition.ToString());
		GUI.Label(new Rect(13, 35, 200, 20), "whidth:" + Screen.width.ToString());
		GUI.Label(new Rect(13, 45, 200, 20), "height:" + Screen.height.ToString());
		//GUI.Label(new Rect(13, 40, 200, 20), "Speed   :" + pMaster.Speed.ToString());*/

	}
}

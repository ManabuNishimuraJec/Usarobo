using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextField : MonoBehaviour
{
    //private PlayerMaster pMaster = new PlayerMaster();
    private EnemyMaster eMaster = new EnemyMaster();
    void OnGUI()
    {
        // テキストフィールドを表示する
        GUI.Label(new Rect(10, 10, 200, 200),"DebuData","box");
        GUI.Label(new Rect(13, 25, 200, 20), "Position:" + eMaster.Destinaion.ToString());
        //GUI.Label(new Rect(13, 40, 200, 20), "Speed   :" + pMaster.Speed.ToString());*/

    }
}

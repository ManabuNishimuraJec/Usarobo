using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoder : MonoBehaviour
{
    [SerializeField]
    private string mapStyle;
    private PlayerMaster playerMaster = new PlayerMaster();
    private MapMaster mMaster = new MapMaster();


    void Start()
    {
        if(mapStyle=="Space")
        {
            playerMaster.CheckMode = true;
        }
        else if(mapStyle=="Planet")
        {
            playerMaster.CheckMode = false;
        }
    }
}

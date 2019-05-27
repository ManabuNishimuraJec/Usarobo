using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
	private PlayerMaster pMaster=new PlayerMaster();

    private Slider slider;

    private GameObject playerCanvas;
    
    void Start()
    {
	
        slider = GetComponent<Slider>();
		slider.maxValue = pMaster.MaxHp;
		Debug.Log(slider.maxValue);

		playerCanvas = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
		slider.value = pMaster.Hp;

		playerCanvas.transform.LookAt(GameObject.Find("Main Camera").transform);
    }
}

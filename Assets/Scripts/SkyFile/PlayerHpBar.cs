using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
	private PlayerMaster pMaster=new PlayerMaster();
	private int pHP;
    private Slider slider;
	private GameObject player = null;
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
		pHP= pMaster.Hp;
		slider.value = pHP;

		playerCanvas.transform.LookAt(GameObject.Find("Main Camera").transform);
    }
}

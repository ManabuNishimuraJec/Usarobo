using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    private Slider slider;
    private int pHP;

	private GameObject Player=null;
    private GameObject playerCanvas;
    
    void Start()
    {
		Player = GameObject.FindGameObjectWithTag("Player");

        pHP = Player.gameObject.GetComponent<PlayerControl>().HP;

        slider = GetComponent<Slider>();
        slider.value = pHP;
        slider.maxValue = pHP;

        playerCanvas = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        pHP = Player.gameObject.GetComponent<PlayerControl>().HP;

        slider.value = pHP;

        playerCanvas.transform.LookAt(GameObject.Find("Main Camera").transform);
    }
}

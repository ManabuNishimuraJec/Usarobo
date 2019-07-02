using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : MonoBehaviour
{
	//出したいオブジェクト
	[SerializeField]
	GameObject mob;
	//出したい数
	[SerializeField]
	int cloneVolum;
	//出す間隔
	[SerializeField]
	float maxtime;
	float time;
	[SerializeField]
	private Transform par;
    void Start()
    {
		time = 0.0f;
    }

    void Update()
    {
		time += Time.deltaTime;
		//指定時間を超えたら出す
		if (time > maxtime)
		{
			Instantiate(mob,transform.position,Quaternion.identity,par);
			time = 0.0f;
			--cloneVolum;
		}
		//出し切ったら消える
		if (cloneVolum < 0)
		{
			Destroy(gameObject);
		}
    }
}

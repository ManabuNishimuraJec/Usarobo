using UnityEngine;
using UnityEngine.UI;

public class SkyGoalDistance : MonoBehaviour
{
	//スライダー
	private Slider slider;
	//スライダーの現在の数値
	private int goaldistance;
	//プレイヤーとゴールの最大距離（初めの状態）
	private float Maxdistance;
	//プレイヤーとゴールの今の距離
	private float Nowdistance;
	//プレイヤーの場所
	private GameObject player;
	//ゴールの場所
	private GameObject goal;
	private MapMaster mapmaster = new MapMaster();
	//一度きりの処理用
	private bool once=true;

	void Start()
    {
		//スライダー取得
		slider = GetComponent<Slider>();
		//プレイヤーの情報取得
		player = GameObject.FindGameObjectWithTag("Player");
	}

    void Update()
    {
		if (once)
		{
			//最大距離算出
			Maxdistance = Vector3.Distance(mapmaster.EndLine, player.transform.position);
			once = false;
		}
		//現在距離算出
		Nowdistance = Vector3.Distance(mapmaster.EndLine, player.transform.position);
		//プレイヤーがどれくらい進んだか算出
		goaldistance =(int) (((Maxdistance - Nowdistance) / Maxdistance) * 1000.0f);
		//スライダーに反映
		slider.value = goaldistance;
	}
}

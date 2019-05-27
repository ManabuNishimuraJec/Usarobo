using UnityEngine;

public class SkyItem : MonoBehaviour
{
	//プレイヤーを見つけるまでnull
	private GameObject Player=null;
	//前進する用
	private Vector3 movePos;
	//プレイヤーを見つけてからの速さ用
	[SerializeField]
	private float speed;
    void Start()
    {
		movePos = new Vector3(0.0f, 0.0f, 0.07f);
    }

    void Update()
    {
		//プレイヤーを見つけるまで前進
		if (Player == null)
			transform.position -= movePos;
		else
			//プレイヤーを見つけたらプレイヤーの方へ行く
			transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);

		//プレイヤーとの距離が１ユニットより少なくなったら
		if(Player != null)
		{
			if (Vector3.Distance(transform.position, Player.transform.position) <= 1)
			{
				Destroy(gameObject);
			}
			//画面外に出たら消える
			if (transform.position.z <= -5.0f)
			{
				Destroy(gameObject);
			}
		}
    }
	private void OnTriggerEnter(Collider col)
	{
		//一定範囲内のプレイヤーにぶつかったら情報を代入
		if(col.gameObject.tag=="Player")
		Player = col.gameObject;
	}
}

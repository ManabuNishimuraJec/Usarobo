using UnityEngine;

public class SkyEnemy : MonoBehaviour
{
    [SerializeField]
    private float speed;					//前に進む速さ
    [SerializeField]
    private SkyEnemyDirection direction;	//左右の動き
	[SerializeField]
	private GameObject bullet;				//エネミーバレット
	[SerializeField]
	private GameObject item;				//落とすアイテム

	[SerializeField]
	private float shotmaxtime;				//弾を撃つ間隔
	private float shottime;					//クールタイム用

	[SerializeField]
	private int maxHP;						//エネミー最大HP
	private int HP;                         //エネミー現在HP

	private Rigidbody rigidbody;

	//初期化
	void Start()
    {
		shottime = 0.0f;
		HP = maxHP;
		rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
		shottime += Time.deltaTime;
		//エネミーの動き
		rigidbody.velocity = new Vector3(speed * direction.chengx, speed * direction.chengy, speed*-0.2f);
		//弾発射条件
		if (shottime > shotmaxtime)
		{
			//弾生成
			Instantiate(bullet, transform.position + new Vector3 ( 0.0f, 0.0f, -1.0f ), Quaternion.identity);
			shottime = 0.0f;
		}
		//死亡判定
		if (HP <= 0)
		{
			//アイテム生成
			Instantiate(item, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
		//範囲外ならデストローーーーーーイ
		if (transform.position.z <= -5.0f)
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter(Collider col)
	{
		//プレイヤーバレットに当たったらダメージを受ける
		if (col.gameObject.tag == "bullet")
		{
			--HP;
		}
	}
}

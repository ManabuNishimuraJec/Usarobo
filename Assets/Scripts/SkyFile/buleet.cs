using UnityEngine;

public class buleet : MonoBehaviour
{
    // 推進力
	public float speed;

    // rigidbody取得
	Rigidbody rid;

    // time管理変数
	private float time = 0.0f;
	public float maxtime;

    // 自身の座標を取得するための変数
    private float myposZ;
    // 削除する座標を格納する変数
    public float eposZ;

    public GameObject ShotUnit;

    private ShotUnitMaster shotMaster = new ShotUnitMaster();


    void Start()
    {
		// Rigidbodyをgetconponent
		rid = GetComponent<Rigidbody>();
        // Playerの情報を格納
		//ShotUnit = GameObject.Find("ShotUnit");

        // 自身の向きにPlayerの向きを格納
		transform.rotation = shotMaster.Rotation;
    }

    // 動かす
    void Update()
    {
        // 現在のZ座標を記憶
        myposZ = transform.position.z;
        // 時間を加算
		time += Time.deltaTime;
        
        // スピードを加算
		rid.AddForce(transform.forward.normalized * speed * -1,ForceMode.Impulse);
        
        if (time > maxtime)
		{
			Destroy(this.gameObject);
		
		}
    }

    private void OnCollisionEnter(Collision collision)
    {
        // バレットに当たったら耐久値を減らす
        if (collision.gameObject.tag == "wall")
        {
            Destroy(this.gameObject);
        }
    }
}

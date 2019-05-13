using UnityEngine;

public class buleet : MonoBehaviour
{
    // 推進力
	public float speed;

    // rigidbody取得
	Rigidbody rid;

    // time管理変数
	private float time;
	public float maxtime;

    // 自身の座標を取得するための変数
    private float myposZ;
    // 削除する座標を格納する変数
    public float eposZ;

    void Start()
    {
		// Rigidbodyをgetconponent
		rid = GetComponent<Rigidbody>();
    }

    // 動かす
    void Update()
    {
        // 現在のZ座標を記憶
        myposZ = transform.position.z;
        // 時間を加算
		time += Time.deltaTime;
        
        // スピードを加算
		rid.AddForce(transform.forward * speed,ForceMode.Impulse);
        
        if (myposZ > eposZ || time > maxtime)
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

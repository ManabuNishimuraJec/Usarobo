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
	
	public GameObject taget = null;

	void Start()
    {
		// Rigidbodyをgetconponent
		rid = GetComponent<Rigidbody>();
    }

    // 動かす
    void Update()
    {
        // 時間を加算
		time += Time.deltaTime;
		if (taget != null)
		{
			transform.transform.position = Vector3.MoveTowards(transform.position, taget.transform.position, speed * 0.025f * Time.deltaTime);
		}
		else
		{
			// スピードを加算
			rid.AddForce(transform.forward * speed, ForceMode.Impulse);
		}

		// 時間に達したら
		if (time > maxtime)
		{
			// 削除
			Destroy(this.gameObject);
		
		}
	}

    private void OnCollisionEnter(Collision collision)
    {
        // バレットに当たったら耐久値を減らす
        if (collision.gameObject.tag == "wall" || collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}

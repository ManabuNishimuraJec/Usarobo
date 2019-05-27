using UnityEngine;

public class SkyEnemy : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private SkyEnemyDirection direction;
	[SerializeField]
	private GameObject bullet;
	[SerializeField]
	private GameObject item;

    [SerializeField]
    private float movemaxtime;
    private float movetime;
	[SerializeField]
	private float shotmaxtime;
	private float shottime;

	[SerializeField]
	private int maxHP;
	private int HP;

	private Rigidbody rigidbody;


	void Start()
    {
		movetime = 0.0f;
		shottime = 0.0f;
		HP = maxHP;
		rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        movetime += Time.deltaTime;
		shottime += Time.deltaTime;
		//if (movetime > movemaxtime)
		//{
		//    transform.position = new Vector3(transform.position.x+direction.chengx, transform.position.y+direction.chengy, transform.position.z + speed);
		//    movetime = 0.0f;
		//}
		rigidbody.velocity = new Vector3(speed * direction.chengx, speed * direction.chengy, speed*-1.0f);
		if (shottime > shotmaxtime)
		{
			Instantiate(bullet, transform.position + new Vector3 ( 0.0f, 0.0f, -1.0f ), Quaternion.identity);
			shottime = 0.0f;
		}

		if (HP <= 0)
		{
			Instantiate(item, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
		if (transform.position.z <= -5.0f)
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "bullet")
		{
			--HP;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	// 歩くスピード
	[SerializeField]
	private float walkSpeed;

	// 目的地
	[SerializeField]
	private Vector3 destination;

	// 速度
	[SerializeField]
	private Vector3 velocity;

	// 移動方向
	[SerializeField]
	private Vector3 direction;

	[SerializeField]
	private float waitTime = 5f;

	private SetPosition setPosition;

    [SerializeField]
	private float elapsedTime;

    [SerializeField]
    private bool arrived;

    private CharacterController enemyController;
    private EnemyMaster eMaster = new EnemyMaster();

    public enum EnemyState
	{
		Walk,
		Wait,
		Chase
	};

	private EnemyState state;       // 敵ステータス

	void Start()
    {
		velocity = Vector3.zero;
		enemyController = GetComponent<CharacterController>();
		arrived = false;
		setPosition = GetComponent<SetPosition>();
		setPosition.CreateRandomPosition();
		destination = setPosition.GetDestination();
		velocity = Vector3.zero;
		elapsedTime = 0f;
		state = EnemyState.Wait;
	}

    void Update()
    {
        eMaster.Destinaion = destination;

        if (!arrived)
		{
			// 着地チェック
			if (enemyController.isGrounded)
			{
				// 移動方向
				direction = (destination - transform.position).normalized;

				transform.LookAt(new Vector3(destination.x, transform.position.y, destination.z));
				velocity = direction * walkSpeed;
				
			}
			velocity.y += Physics.gravity.y * Time.deltaTime;
			enemyController.Move(velocity * Time.deltaTime);

            
            var nowPosition = new Vector3(transform.position.x, 0, transform.position.z);
            var newDestination = new Vector3(destination.x, 0, destination.z);
            if (Vector3.Distance(nowPosition, newDestination) < 0.5f)
			{
				arrived = true;
			}
		}
		else
		{
			elapsedTime += Time.deltaTime;

			// 待ち時間を超えたら次の目的地を設定
			if(elapsedTime > waitTime)
			{
				setPosition.CreateRandomPosition();
				destination = setPosition.GetDestination();
				arrived = false;
				elapsedTime = 0f;

			}
		}
    }

	public void SetState(string mode, Transform obj = null)
	{
		if (mode == "walk")
		{

		}
	}
}

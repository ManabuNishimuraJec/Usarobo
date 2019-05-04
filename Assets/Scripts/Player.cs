using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
<<<<<<< HEAD
	private float moveSpeed = 5.0f;
=======
    private float moveSpeed = 5.0f;
>>>>>>> master
    [SerializeField]
    private float jumpPower = 0.0f;
    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private Transform center;

    private CharacterController PlayerControl;
    private float inputHorizontal;
    private float inputVertical;
    private Vector3 velocity;

    void Start()
    {
<<<<<<< HEAD
		PlayerControl = GetComponent<CharacterController>();
	}
=======
        PlayerControl = GetComponent<CharacterController>();
    }
>>>>>>> master

    void Update()
    {
        // 地面に立っている状態
        if (PlayerControl.isGrounded)
        {
            inputHorizontal = Input.GetAxis("Horizontal");
            inputVertical = Input.GetAxis("Vertical");

            // カメラの方向から、x-z平面の単位ベクトルを取得
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            velocity = Vector3.zero;

            // カメラの方向を加味して移動
<<<<<<< HEAD
            velocity = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal ;
=======
            velocity = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;
>>>>>>> master
            velocity *= moveSpeed;

            if (Input.GetKeyDown(KeyCode.V))
            {
                // ジャンプ
                velocity.y += jumpPower;
            }
        }

        // 重力を設定
        velocity.y += Physics.gravity.y * Time.deltaTime;

        PlayerControl.Move(velocity * Time.deltaTime);
<<<<<<< HEAD
    } 
=======
    }
>>>>>>> master
}
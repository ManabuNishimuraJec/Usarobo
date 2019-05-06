using UnityEngine;
using UniRx;
using System;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;
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

    private Subject<Unit> shotSubject = new Subject<Unit>();

    public IObservable<Unit> OnShotCanonMessage
    {
        get { return shotSubject; }
    }

    void Start()
    {

        PlayerControl = GetComponent<CharacterController>();
    }


    void Update()
    {
        ShotAtk();

        // 地面に立っている状態
        if (PlayerControl.isGrounded)
        {
            inputHorizontal = Input.GetAxis("Horizontal");
            inputVertical = Input.GetAxis("Vertical");

            // カメラの方向から、x-z平面の単位ベクトルを取得
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            velocity = Vector3.zero;

            // カメラの方向を加味して移動
            velocity = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;
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
    }

    void ShotAtk()
    {
        // マウス左クリック
        if (Input.GetMouseButton(0))
        {
            shotSubject.OnNext(Unit.Default);
        }
    }
}
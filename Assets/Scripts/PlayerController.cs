using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private float horizontal;
    private float vertical;

    private Vector2 lookDirection = new Vector2(1, 0);

    private PlayerAnimation playerAnim;

    [SerializeField]
    private float moveSpeed = 3.0f;

    void Start()
    {
        TryGetComponent(out rb);

        TryGetComponent(out playerAnim);
    }

    void Update()
    {
        // キー入力判定
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (playerAnim)  // この if 文はなんのためにあるのか考えてみてください
        {
            // 移動する方向と移動アニメの同期
            SyncMoveAnimation();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * vertical + Camera.main.transform.right * horizontal;

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す
        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

        // キー入力により移動方向が決まっている場合には、キャラクターの向きを進行方向を合わせる
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    /// <summary>
    /// 移動する方向と移動アニメの同期
    /// </summary>
    private void SyncMoveAnimation()
    {
        // 移動している場合
        if (!Mathf.Approximately(horizontal, 0.0f) || !Mathf.Approximately(vertical, 0.0f))
        {
            // 移動している方向の情報をセット
            lookDirection.Set(horizontal, vertical);

            // 正規化
            lookDirection.Normalize();

            // 待機アニメから移動アニメへ遷移
            playerAnim.ChangeAnimationFromFloat(PlayerAnimationState.Speed, lookDirection.sqrMagnitude);

            Debug.Log("移動アニメ再生 : " + lookDirection.sqrMagnitude);
        }
        else
        {
            // 移動していない場合、移動アニメから待機アニメへ遷移
            playerAnim.ChangeAnimationFromFloat(PlayerAnimationState.Speed, 0);
            Debug.Log("停止");
        }
    }
}

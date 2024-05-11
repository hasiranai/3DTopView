using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody rb;

    private PlayerAnimation playerAnim;   // Rigidbody と同じ private なので、ここに追加する

    [SerializeField, Header("接地判定")]
    private bool isGrounded;

    [SerializeField, Header("ジャンプ力")]
    private float jumpPower;

    [SerializeField, Header("地面判定用レイヤー")]
    private LayerMask groundLayer;

    void Start()
    {
        TryGetComponent(out rb);

        // アニメ用のコンポーネント取得
        TryGetComponent(out playerAnim);

        // TODO ジャンプ力を外部クラスの情報から設定
    }

    void Update()
    {
        // Linecastでキャラの足元に地面があるか判定　地面があるときはTrueを返す
        isGrounded = Physics.Linecast(transform.position + transform.up * 1,
                        transform.position - transform.up * 0.3f,
                        groundLayer);

        if (!isGrounded)
        {
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    private void Jump()
    {
        // アニメ処理
        playerAnim.ChangeAnimationTrigger(PlayerAnimationState.Jump);

        // ジャンプ
        rb.AddForce(Vector3.up * jumpPower);
    }
}

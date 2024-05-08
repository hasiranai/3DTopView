using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3.0f;

    private Rigidbody rb;

    private float moveX;
    private float moveZ;

    void Start()
    {
        // このスクリプトのアタッチしているゲームオブジェクトに対して、TryGetComponent メソッドを実行し、
        // Rigidbody コンポーネントの取得ができた場合には、Rigidbody コンポーネントの情報を rb 変数に代入する
        TryGetComponent(out rb);
    }

    void Update()
    {
        // キー入力を感知し、それぞれの変数に値を代入する。その値を Move メソッド内で利用する
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        // 移動
        Move();
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        // 入力方向をベクトル化して正規化
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        // Rigidbody の velocity の値を normalized した値が、Vector3.zero(0, 0, 0) ではない場合
         // => つまり、「移動している場合」として解釈できる
         if (moveDirection != Vector3.zero)
        {
            // 入力方向を向く回転を計算
            // このスクリプトをアタッチしているゲームオブジェクト(プレイヤー)の Transform の Rotation の値に対して
            // Rigidbody の velocity の値を normalized した値を、LookRotation メソッドを利用して角度の値(Quaternion)として変換し、それを代入する
            // => つまり、「移動方向にキャラの向きを合わせる」として解釈できる
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);

            // 回転を適用
            rb.MoveRotation(newRotation);
        }

        // 移動している場合、移動方向にキャラの向きを合わせる
        // 移動ベクトルを計算
        Vector3 movement = moveDirection * moveSpeed;

        // 移動ベクトルを Rigidbody に適用
        // Rigidbody のVelocity(速度)の値を更新して、物理演算によってプレイヤーの移動を行う
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // TODO 移動や停止のアニメーションの同期
    }
}

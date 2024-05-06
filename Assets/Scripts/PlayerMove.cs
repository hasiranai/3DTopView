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
        // Rigidbody の Velocity(速度)の値を更新して、物理演算によってプレイヤーの移動を行う
        rb.velocity = new Vector3(moveX, rb.velocity.y, moveZ) * moveSpeed;
        //rb.velocity.y

        // TODO 移動や停止のアニメーションの同期

        // Rigidbody の velocity の値を normalized した値が、Vector3.zero(0, 0, 0) ではない場合
        // => つまり、「移動している場合」として解釈できる
        if (rb.velocity.normalized != Vector3.zero)
        {
            // このスクリプトのアタッチしているゲームオブジェクト(プレイヤー)の Transform の Rotation の値に対して
            // Rigidbody の velocity の値を normalized した値を、LookRotation メソッドを利用して角度の値(Quaternion)として変換し、それを代入する
            // => つまり、「移動方向にキャラの向きを合わせる」として解釈できる
            transform.rotation = Quaternion.LookRotation(rb.velocity.normalized);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private PlayerAnimation playerAnim;

    private int attackPower;

    [SerializeField]
    private BoxCollider boxCol;   // 武器のゲームオブジェクトをアサインし、コライダーの情報を登録する

    void Start()
    {
        TryGetComponent(out playerAnim);

        // 武器のコライダーをオフにする
        boxCol.enabled = false;

        // TODO CharaData から貰う
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !playerAnim.GetAnimator().IsInTransition(0))
        {
            playerAnim.ChangeAnimationTrigger(PlayerAnimationState.Attack);
            playerAnim.ChangeAnimationFromFloat(PlayerAnimationState.Speed, 0);
        }
    }

    /// <summary>
    /// AnimationEvent から実行
    /// コライダーのオンオフ切り替え
    /// </summary>
    /// <param name="switchIndex"></param>
    private void SwitchWeaponCollider(int switchIndex)
    {
        // コライダーのオンオフ切り替え
        boxCol.enabled = switchIndex == 0 ? true : false;

        Debug.Log(boxCol.enabled);
    }

    // 武器のコライダーによる侵入判定
    private void OnTriggerEnter(Collider col)
    {
        // 武器にコライダーがアタッチされていない場合には、この判定処理を行わない
        if (!boxCol.enabled)
        {
            return;
        }

        // 武器のコライダーが、別のコライダーに侵入した時
        // そのコライダーのゲームオブジェクトに Health スクリプトがアタッチされていれば = すなわち、敵のゲームオブジェクトなら
        if (col.gameObject.TryGetComponent(out Health health))
        {
            // 敵の Hp を減少させる
            health.TakeDamage(-attackPower);

            Debug.Log("攻撃ヒット");
        }
    }
}

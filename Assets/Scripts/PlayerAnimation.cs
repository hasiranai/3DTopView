using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        if (!TryGetComponent(out anim))
        {
            Debug.Log("Animator を取得できません");
        }
    }

    /// <summary>
    /// 移動アニメと速度の同期
    /// </summary>
    /// <param name="nextState"></param>
    /// <param name="amount"></param>
    public void ChangeAnimationFromFloat(PlayerAnimationState nextAnimState, float amount)
    {
        // Locomotion の parameter にチェックを入れて Speed の parameter と連動させる
        anim.SetFloat(nextAnimState.ToString(), amount);
    }

    /// <summary>
    /// Bool パラメータのアニメの再生と停止
    /// </summary>
    /// <param name="nextAnimState"></param>
    /// <param name="isChange"></param>
    public void ChangeAnimationBool(PlayerAnimationState nextAnimState, bool isChange)
    {
        anim.SetBool(nextAnimState.ToString(), isChange);
    }

    // TODO Trigger 型のパラメータのアニメの再生

    /// <summary>
    /// Animator コンポーネントの取得用
    /// </summary>
    /// <returns></returns>
    public Animator GetAnimator()
    {
        return anim;
    }

    internal void ChangeAnimationFromFloat(object speed, float sqrMagnitude)
    {
        throw new NotImplementedException();
    }
}

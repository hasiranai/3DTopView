using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHp;

    private int currentHp;

    [SerializeField]
    private Slider healthSlider; // HPゲージを表示するSlider

    void Start()
    {
        // Hpの初期化
        InitialHealth(maxHp);
    }

    /// <summary>
    /// Hpの初期化
    /// </summary>
    /// <param name="initialHp"></param>
    public void InitialHealth(int initialHp)
    {
        maxHp = initialHp;
        currentHp = maxHp;

        // Hp 用のスライダー設定。Slider の最大値と現在値に、現在の Hp の値をセットする
        healthSlider.maxValue = currentHp;
        healthSlider.value = currentHp;
    }

    /// <summary>
    /// ダメージ用
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        currentHp = Mathf.Max(currentHp - damage, 0);

        // HPゲージをアニメさせて変動させる
        healthSlider.DOValue(currentHp, 0.5f);

        // Hp の計算と生存確認
        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 回復用
    /// </summary>
    /// <param name="recoveryPoint"></param>
    public void Heal(int recoveryPoint)
    {
        currentHp = Mathf.Min(currentHp + recoveryPoint, maxHp);
    }
}

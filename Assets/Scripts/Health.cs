using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHp;

    private int currentHp;

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
    }

    /// <summary>
    /// ダメージ用
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        currentHp = Mathf.Max(currentHp - damage, 0);

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

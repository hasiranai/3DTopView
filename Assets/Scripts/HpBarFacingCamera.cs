using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarFacingCamera : MonoBehaviour
{
    private void LateUpdate()
    {
        // HpBar を常にカメラの方向に向ける
        LookHpBarToCamera();
    }

    /// <summary>
    /// HpBarを常にカメラの方向に向ける
    /// </summary>正対
    private void LookHpBarToCamera()
    {
        // オブジェクトの回転をカメラの回転と完全に一致させることで、
        // このスクリプトをアタッチしているゲームオブジェクトを回転させ、カメラの方向に正対させる
        transform.rotation = Camera.main.transform.rotation;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonFadeControl : MonoBehaviour
{
    //  フェード時間（秒）
    [SerializeField] private float _fadeSpeed = 0.5f;

    /// <summary>
    /// フェードイン
    /// </summary>
    /// <param name="canvasGroup">対象のキャンバスグループ</param>
    /// <returns></returns>
    public IEnumerator FadeIn(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1.0f;
        while (canvasGroup.alpha > 0.0f)
        {
            canvasGroup.alpha -= Time.deltaTime * (1.0f / _fadeSpeed);
            if (canvasGroup.alpha <= 0.0f)
                canvasGroup.alpha = 0.0f;
            yield return null;
        }
    }

    /// <summary>
    /// フェードアウト
    /// </summary>
    /// <param name="canvasGroup">対象のキャンバスグループ</param>
    /// <returns></returns>
    public IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0.0f;
        while (canvasGroup.alpha < 1.0f)
        {
            canvasGroup.alpha += Time.deltaTime * (1.0f / _fadeSpeed);
            if (canvasGroup.alpha >= 1.0f)
                canvasGroup.alpha = 1.0f;
            yield return null;
        }
    }
}

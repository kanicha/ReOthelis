using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    //�L�����o�X�O���[�v�̎擾
    [SerializeField] private CanvasGroup _canvasGroup;
    public CanvasGroup FadeCanvasGroup
    {
        get { return _canvasGroup; }
    }

    //�C���[�W�̎擾
    [SerializeField] private Image _fadeImage;
    public Image FadeImage
    {
        get { return _fadeImage; }
    }

    //�t�F�[�h�C��
    public IEnumerator FadeIn(CanvasGroup canvasGroup, Image image)
    {
        canvasGroup.alpha = 1.0f;
        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= Time.deltaTime;
            if (canvasGroup.alpha <= 0f)
            {
                canvasGroup.alpha = 0f;
            }
            yield return null;
        }
        image.raycastTarget = false;
    }

    //�t�F�[�h�A�E�g
    public IEnumerator FadeOut(CanvasGroup canvasGroup, Image image)
    {
        image.raycastTarget = true;
        canvasGroup.alpha = 0.0f;
        while(canvasGroup.alpha < 1.0f)
        {
            canvasGroup.alpha += Time.deltaTime;
            if (canvasGroup.alpha >= 1.0f)
            {
                canvasGroup.alpha = 1.0f;
            }
            yield return null;
        }
    }
}

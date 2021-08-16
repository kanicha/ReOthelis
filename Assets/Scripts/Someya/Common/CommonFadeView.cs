using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonFadeView : MonoBehaviour
{
    [SerializeField] private CanvasGroup _fadeCanvasGroup;
    public CanvasGroup FadeCanvasGroup
    {
        get
        {
            return _fadeCanvasGroup;
        }
    }

    [SerializeField] private Image _fadeImage;
    public Image FadeImage
    {
        get { return _fadeImage; }
    }
}

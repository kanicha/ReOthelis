using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackOutControl : MonoBehaviour
{
    public Image blackOutImage;

    public void FadeIn()
    {
        blackOutImage.color = new Color(0,0,0,0.5f);
    }

    public void FadeOut()
    {
        blackOutImage.color = new Color(0,0,0,0);
    }
}

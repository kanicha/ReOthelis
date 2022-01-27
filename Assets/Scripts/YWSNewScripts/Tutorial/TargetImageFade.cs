using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetImageFade : TitleNameFade
{
    //0左上 1左下 2右上 3右下
    [SerializeField, Header("点滅させるオブジェクト")]
    private Image[] _targetImageArray = new Image[4];

    // Start is called before the first frame update
    void Start()
    {
        _targetImageArray[0].color = new Color(255, 255, 255, 0);
        _targetImageArray[1].color = new Color(255, 255, 255, 0);
        _targetImageArray[2].color = new Color(255, 255, 255, 0);
        _targetImageArray[3].color = new Color(255, 255, 255, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // 前の値とセレクトの値が違った場合色を初期化
        if (_lastnum != ModeSelect._selectCount)
            _targetImageArray[_lastnum].color = new Color(255, 255, 255, 255);

        // モードの値におうじてフェード
        if (TutorialDirector.Instance.tutorialPhase == TutorialDirector.TutorialPhase.MoveLeft)
        {
            _targetImageArray[0].color = GetAlphaColor(_targetImageArray[0].color);
            _targetImageArray[1].color = GetAlphaColor(_targetImageArray[1].color);
            _lastnum = 0;
        }
        else if (TutorialDirector.Instance.tutorialPhase == TutorialDirector.TutorialPhase.MoveRight)
        {
            _targetImageArray[0].color = new Color(255, 255, 255, 0);
            _targetImageArray[1].color = new Color(255, 255, 255, 0);
            _targetImageArray[2].color = GetAlphaColor(_targetImageArray[2].color);
            _targetImageArray[3].color = GetAlphaColor(_targetImageArray[3].color);
            _lastnum = 1;
        }
        else
        {
            _targetImageArray[0].color = new Color(255, 255, 255, 0);
            _targetImageArray[1].color = new Color(255, 255, 255, 0);
            _targetImageArray[2].color = new Color(255, 255, 255, 0);
            _targetImageArray[3].color = new Color(255, 255, 255, 0);
        }
    }
}

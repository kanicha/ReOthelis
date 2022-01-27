using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetImageFade : TitleNameFade
{
    //0���� 1���� 2�E�� 3�E��
    [SerializeField, Header("�_�ł�����I�u�W�F�N�g")]
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
        // �O�̒l�ƃZ���N�g�̒l��������ꍇ�F��������
        if (_lastnum != ModeSelect._selectCount)
            _targetImageArray[_lastnum].color = new Color(255, 255, 255, 255);

        // ���[�h�̒l�ɂ������ăt�F�[�h
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

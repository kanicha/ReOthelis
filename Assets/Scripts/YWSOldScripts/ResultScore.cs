using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour
{
    //�J�E���g�A�b�v���s���Ă��邩�ǂ���
    private bool IsCounting = true;
    //�J�E���g�̏����l
    private float StartCount = 0.0f;
    //�J�E���g�̌��ݒl
    private float NowCount = 0.0f;
    //�J�E���g�̍ŏI�l�i�v���C���[�̊l���X�R�A�j
    private float Score = 0.0f;
    //�K�v����
    private float RequiredTime = 0.5f;
    //���݌o�ߎ���
    private float CountedTime = 0.0f;

    private void Start()
    {
        IsCounting = true;
        StartCount = 0.0f;
        NowCount = 0.0f;
        Score = 0.0f;
        RequiredTime = 0.5f;
        CountedTime = 0.0f;
    }

    private void Update()
    {
        if (IsCounting == true)
        {
            if (NowCount > Score)
            {
                NowCount = Score;
                return;
            }
            else if (NowCount == Score)
            {
                IsCounting = false;
                return;
            }
            NowCount = StartCount + CountedTime / RequiredTime;
        }
        else
        {
            return;
        }
    }
}
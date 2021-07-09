using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CharaImageMoved2P : Player2Base
{
    [SerializeField] private Image charactorImage2P;
    [SerializeField] private Sprite[] charactorImageArray2P;
    [SerializeField] private GameObject[] charactorButtonWhite2P;
    private int _prev2P = 0;

    // �L�����N�^�[�^�C�v
    public enum CharaType2P
    {
        Cow,
        Mouse,
        Rabbit,
        Tiger
    }
    public static CharaType2P charaType2P = CharaType2P.Cow;

    // Start is called before the first frame update
    void Start()
    {
        // ����������
        charactorImage2P.sprite = charactorImageArray2P[0];
        charactorButtonWhite2P[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        base.SaveKeyValue();
        base.KeyInput();

        Player2CharaMoved();
    }

    /// <summary>
    /// 1P �摜�����֐�
    /// </summary>
    void Player2CharaMoved()
    {
        // ���͕���
        if ((_DS4_horizontal_value < 0 && last_horizontal_value == 0))
        {
            charaType2P--;

            // Active�����{�^��false�ɂ��鏈��
            for (int i = 0; i < charactorButtonWhite2P.Length; i++)
            {
                charactorButtonWhite2P[i].SetActive(false);
            }
        } 
        else if ((_DS4_horizontal_value > 0 && last_horizontal_value == 0))
        {
            charaType2P++;

            for (int i = 0; i < charactorButtonWhite2P.Length; i++)
            {
                charactorButtonWhite2P[i].SetActive(false);
            }
        }

        // prev �� result �ϐ��̒��g(int�^)��������ꍇ�`�揈��
        if (_prev2P != (int)charaType2P)
        {
            _prev2P = (int)charaType2P;

            if (charaType2P < 0)
            {
                charaType2P = (CharaType2P)(charactorImageArray2P.Length - 1);
            }
            else if ((int)charaType2P >= charactorImageArray2P.Length)
            {
                charaType2P = 0;
            }

            charactorImage2P.sprite = charactorImageArray2P[(int)charaType2P];
            charactorButtonWhite2P[(int)charaType2P].SetActive(true);
        }
    }
}
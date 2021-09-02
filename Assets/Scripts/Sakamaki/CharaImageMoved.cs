using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CharaImageMoved : Player1Base
{
    [SerializeField] private CharactorInfo _charactorInfo = null;
    [SerializeField] private Image charactorImage1P;
    [SerializeField] private Sprite[] charactorImageArray1P;
    [SerializeField] private GameObject[] charactorButtonWhite1P;

    private int _prev1P = 0;

    // キャラクタータイプ
    public enum CharaType1P
    {
        Cow,
        Mouse,
        Rabbit,
        Tiger
    }
    public static CharaType1P charaType1P = CharaType1P.Cow;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化処理
        charactorImage1P.sprite = charactorImageArray1P[0];
        charactorButtonWhite1P[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        base.SaveKeyValue();
        base.KeyInput();

        Player1CharaMoved();
        _charactorInfo.InfoDraw();
    }

    /// <summary>
    /// 1P 画像処理関数
    /// </summary>
    void Player1CharaMoved()
    {
        // 入力部分
        if ((_DS4_horizontal_value < 0 && last_horizontal_value == 0))
        {
            charaType1P--;

            // Activeしたボタンfalseにする処理
            for (int i = 0; i < charactorButtonWhite1P.Length; i++)
            {
                charactorButtonWhite1P[i].SetActive(false);
            }
        }
        else if ((_DS4_horizontal_value > 0 && last_horizontal_value == 0))
        {
            charaType1P++;

            for (int i = 0; i < charactorButtonWhite1P.Length; i++)
            {
                charactorButtonWhite1P[i].SetActive(false);
            }
        }

        // prev と result 変数の中身(int型)が違った場合描画処理
        if (_prev1P != (int)charaType1P)
        {
            _prev1P = (int)charaType1P;

            if (charaType1P < 0)
            {
                charaType1P = (CharaType1P)(charactorImageArray1P.Length - 1);
            }
            else if ((int)charaType1P >= charactorImageArray1P.Length)
            {
                charaType1P = 0;
            }

            charactorImage1P.sprite = charactorImageArray1P[(int)charaType1P];
            charactorButtonWhite1P[(int)charaType1P].SetActive(true);
        }
    }
}
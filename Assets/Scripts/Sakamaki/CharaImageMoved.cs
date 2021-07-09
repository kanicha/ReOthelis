using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


// 正直クソ実装なので修正しなきゃなと思っている
public class CharaImageMoved : Player1Base
{
    [SerializeField] private Image charactorImage1P;
    [SerializeField] private Sprite[] charactorImageArray1P;
    [SerializeField] private GameObject[] charactorButtonWhite1P;
    [SerializeField] Player1 p1;

    private int _frameCount1P = 0;
    private int _moveSpeed1P = 10;
    private int _back1P = 0;
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
        Player1CharaMoved();
    }

    /// <summary>
    /// 1P 画像処理関数
    /// </summary>
    void Player1CharaMoved()
    {
        _frameCount1P++;
        _frameCount1P %= _moveSpeed1P;

        if (_frameCount1P == 0)
        {
            // 入力部分
            if (p1._horizontal < 0 || Input.GetKeyDown(KeyCode.A))
                charaType1P--;
            else if (p1._horizontal > 0 || Input.GetKeyDown(KeyCode.D))
                charaType1P++;
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

            _back1P = (int)charaType1P;
        }

        // Activeしたボタンfalseにする処理
        if (_back1P >= 1)
        {
            _back1P--;
            charactorButtonWhite1P[_back1P].SetActive(false);
        }
        else if ((int)charaType1P <= _back1P)
        {
            charactorButtonWhite1P[3].SetActive(false);
        }
        else if ((int)charaType1P >= _back1P)
        {
            charactorButtonWhite1P[0].SetActive(false);
        }
    }
}
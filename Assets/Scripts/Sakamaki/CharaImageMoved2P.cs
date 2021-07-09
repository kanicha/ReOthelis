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

    private int _back2P = 0;
    private int _prev2P = 0;

    // キャラクタータイプ
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
        // 初期化処理
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
    /// 1P 画像処理関数
    /// </summary>
    void Player2CharaMoved()
    {
        // 入力部分
        if ((_DS4_horizontal_value < 0 && last_horizontal_value == 0))
            charaType2P--;
        else if ((_DS4_horizontal_value > 0 && last_horizontal_value == 0))
            charaType2P++;


        // prev と result 変数の中身(int型)が違った場合描画処理
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

            _back2P = (int)charaType2P;
        }

        // Activeしたボタンfalseにする処理
        if (_back2P >= 1)
        {
            _back2P--;
            charactorButtonWhite2P[_back2P].SetActive(false);
        }
        else if ((int)charaType2P <= _back2P)
        {
            charactorButtonWhite2P[3].SetActive(false);
        }
        else if ((int)charaType2P >= _back2P)
        {
            charactorButtonWhite2P[0].SetActive(false);
        }
    }
}
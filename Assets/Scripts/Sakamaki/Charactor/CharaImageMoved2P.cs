using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CharaImageMoved2P : Player2Base
{
    [SerializeField] private CharactorInfo _charactorInfo;
    [SerializeField] private Image charactorImage2P;
    [SerializeField] private Sprite[] charactorImageArray2P;
    [SerializeField] private GameObject[] charactorButtonWhite2P;

    private int _prev2P = 0;
    public bool isConfirm = false;

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
        charaType2P = CharaType2P.Cow;
        charactorImage2P.sprite = charactorImageArray2P[0];
        charactorButtonWhite2P[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        base.SaveKeyValue();
        base.KeyInput();

        Player2CharaMoved();
        _charactorInfo.InfoDraw();
    }

    /// <summary>
    /// 1P 画像処理関数
    /// </summary>
    void Player2CharaMoved()
    {
        if (CharacterSelectSceneChange.Instance.isLoading)
            return;

        if (isConfirm)
        {
            // キャラ決定解除
            if (_DS4_cross_value || Input.GetKeyDown(KeyCode.U))
            {
                SoundManager.Instance.PlaySE(6);
                _charactorInfo.OKButtonAnimControl();
                
                isConfirm = false;
            }
            else
                return;
        }

        // 入力部分
        if ((_DS4_horizontal_value < 0 && last_horizontal_value == 0))
        {
            SoundManager.Instance.PlaySE(3);
            
            charaType2P--;

            // Activeしたボタンfalseにする処理
            for (int i = 0; i < charactorButtonWhite2P.Length; i++)
            {
                charactorButtonWhite2P[i].SetActive(false);
            }
        }
        else if ((_DS4_horizontal_value > 0 && last_horizontal_value == 0))
        {
            SoundManager.Instance.PlaySE(3);
            
            charaType2P++;

            for (int i = 0; i < charactorButtonWhite2P.Length; i++)
            {
                charactorButtonWhite2P[i].SetActive(false);
            }
        }
        else if (_DS4_circle_value || Input.GetKeyDown(KeyCode.O))
        {
            SoundManager.Instance.PlaySE(7);
            _charactorInfo.OKButtonAnimControl();
            
            // キャラ決定
            isConfirm = true;
            SoundManager.Instance.CharacterConfirmVoice2P(charaType2P);
            SoundManager.Instance.PlayVoice2P(SoundManager.VoiceType.CharaSelect);
        }

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
        }
    }
}
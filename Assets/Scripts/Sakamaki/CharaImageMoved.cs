using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


// 正直クソ実装なので修正しなきゃなと思っている
public class CharaImageMoved : MonoBehaviour
{
    [SerializeField] private Image charactorImage1P;
    [SerializeField] private Image charactorImage2P;
    [SerializeField] private Sprite[] CharactorImageArray1P;
    [SerializeField] private Sprite[] CharactorImageArray2P;
    [SerializeField] private Player1 p1;
    [SerializeField] private Player2 p2;

    [SerializeField] private GameObject[] charactorButtonWhite1P;
    [SerializeField] private GameObject[] charactorButtonWhite2P;


    private int _frameCount1P = 0;
    private int _frameCount2P = 0;
    private int _moveSpeed1P = 10;
    private int _moveSpeed2P = 10;
    private int _next1P = 0;
    private int _back1P = 0;
    private int _back2P = 0;
    private int _next2P = 0;
    private int _prev1P = 0;
    private int _prev2P = 0;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化処理
        charactorImage1P.sprite = CharactorImageArray1P[0];
        charactorImage2P.sprite = CharactorImageArray2P[0];
        charactorButtonWhite1P[0].SetActive(true);
        charactorButtonWhite2P[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Player1CharaMoved();
        Player2CharaMoved();
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
                _next1P--;
            else if (p1._horizontal > 0 || Input.GetKeyDown(KeyCode.D))
                _next1P++;
        }

        // prev と result 変数の中身(int型)が違った場合描画処理
        if (_prev1P != _next1P)
        {
            _prev1P = _next1P;

            if (_next1P < 0)
            {
                _next1P = CharactorImageArray1P.Length - 1;
            }
            else if (_next1P >= CharactorImageArray1P.Length)
            {
                _next1P = 0;
            }

            charactorImage1P.sprite = CharactorImageArray1P[_next1P];
            charactorButtonWhite1P[_next1P].SetActive(true);

            _back1P = _next1P;
        }

        // Activeしたボタンfalseにする処理
        if (_back1P >= 1)
        {
            _back1P--;
            charactorButtonWhite1P[_back1P].SetActive(false);
        }
        else if (_next1P <= _back1P)
        {
            charactorButtonWhite1P[3].SetActive(false);
        }
        else if (_next1P >= _back1P)
        {
            charactorButtonWhite1P[0].SetActive(false);
        }
    }

    /// <summary>
    /// 2P画像処理関数
    /// </summary>
    void Player2CharaMoved()
    {
        _frameCount2P++;
        _frameCount2P %= _moveSpeed2P;

        if (_frameCount2P == 0)
        {
            // 入力部分
            if (p2._horizontal < 0 || Input.GetKeyDown(KeyCode.J))
                _next2P--;
            else if (p2._horizontal > 0 || Input.GetKeyDown(KeyCode.L))
                _next2P++;
        }

        // prev と result 変数の中身(int型)が違った場合描画処理
        if (_prev2P != _next2P)
        {
            _prev2P = _next2P;

            if (_next2P < 0)
            {
                _next2P = CharactorImageArray1P.Length - 1;
            }
            else if (_next2P >= CharactorImageArray1P.Length)
            {
                _next2P = 0;
            }

            charactorImage2P.sprite = CharactorImageArray2P[_next2P];
            charactorButtonWhite1P[_next1P].SetActive(true);

            _back1P = _next1P;
        }

        // Activeしたボタンfalseにする処理
        if (_back2P >= 1)
        {
            _back1P--;
            charactorButtonWhite2P[_back2P].SetActive(false);
        }
        else if (_next2P >= _back2P)
        {
            charactorButtonWhite2P[0].SetActive(false);
        }
        else if (_next2P <= _back2P)
        {
            charactorButtonWhite2P[3].SetActive(false);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Judgment : MonoBehaviour
{
    [Header("判定画像")]
    [SerializeField] private Image _judgeImage1P = null;
    [SerializeField] private Image _judgeImage2P = null;
    [SerializeField] private Sprite[] _judgeImageArray = new Sprite[2];
    [Header("表情差分")]
    [SerializeField,Header("0が1P 1が2P")] private Image[] _playerImage = new Image[2];
    [SerializeField] private Sprite[] _winPlayerImageArray1P = new Sprite[4];
    [SerializeField] private Sprite[] _losePlayerImageArray1P = new Sprite[4];
    [SerializeField] private Sprite[] _winPlayerImageArray2P = new Sprite[4];
    [SerializeField] private Sprite[] _losePlayerImageArray2P = new Sprite[4];
    private float _imageColor = 0;
    private bool _isAppear = false;
    //勝敗判定のテキストは2倍で出現し、0.8倍にまで縮小し、最後に1倍に戻る。
    private RectTransform _p1Appear;
    private RectTransform _p2Appear;
    //2倍の時のサイズ
    private int _firstWidth_X = 800;
    private int _firstHeight_Y = 500;
    //0.8private倍の時のサイズ
    private int _middleWidth_X = 320;
    private int _middleHeight_Y = 200;
    //1倍の時のサイズ
    private int _finalWidth_X = 400;
    private int _finalHeight_Y = 250;
    private int _appearSpeed = 5;
    private bool _isDecreased = false;
    private bool _isIncreased = true;

    // Start is called before the first frame update
    void Start()
    {
        _isAppear = false;
        _isDecreased = false;
        _isIncreased = true;
        _imageColor = 0;
        _judgeImage1P.color = new Color(255,255,255,0);
        _judgeImage2P.color = new Color(255,255,255,0);
        _p1Appear = GameObject.Find("JudgeImage1P").GetComponent<RectTransform>();
        _p2Appear = GameObject.Find("JudgeImage2P").GetComponent<RectTransform>();
        _p1Appear.sizeDelta = new Vector2(_firstWidth_X,_firstHeight_Y);
        _p2Appear.sizeDelta = new Vector2(_firstWidth_X,_firstHeight_Y);
        
        // 1P勝利
        if (Player_1.displayScore > Player_2.displayScore)
        {
            _playerImage[0].sprite = _winPlayerImageArray1P[(int) CharaImageMoved.charaType1P];
            _playerImage[1].sprite = _losePlayerImageArray2P[(int) CharaImageMoved2P.charaType2P];
            _judgeImage1P.sprite = _judgeImageArray[0];
            _judgeImage2P.sprite = _judgeImageArray[1];
        }
        // 2P勝利
        else if (Player_1.displayScore < Player_2.displayScore)
        {
            _playerImage[0].sprite = _losePlayerImageArray1P[(int) CharaImageMoved.charaType1P];
            _playerImage[1].sprite = _winPlayerImageArray2P[(int) CharaImageMoved2P.charaType2P];
            _judgeImage1P.sprite = _judgeImageArray[1];
            _judgeImage2P.sprite = _judgeImageArray[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        //画像を透明から不透明に変更
        if (_isAppear == false && ScoreDisplay.IsScoreAppear == true)
        {
            _judgeImage1P.color = new Color(255,255,255,_imageColor);
            _judgeImage2P.color = new Color(255,255,255,_imageColor);
            _imageColor += Time.deltaTime;
            if (_imageColor >= 1)
            {
                _imageColor = 1;
                _isAppear = true;
            }
        }

        //画像のサイズを変更
        if (_isDecreased == false && _isIncreased == true && ScoreDisplay.IsScoreAppear == true)
        {
            _p1Appear.sizeDelta = new Vector2(_firstWidth_X,_firstHeight_Y);
            _p2Appear.sizeDelta = new Vector2(_firstWidth_X,_firstHeight_Y);
            _firstWidth_X -= _appearSpeed;
            _firstHeight_Y -= _appearSpeed;
            if (_firstWidth_X <= _middleWidth_X && _firstHeight_Y <= _middleHeight_Y)
            {
                _firstWidth_X = _middleWidth_X;
                _firstHeight_Y = _middleHeight_Y;
                _isDecreased = true;
                _isIncreased = false;
            }
        }
        else if (_isDecreased == true && _isIncreased == false && ScoreDisplay.IsScoreAppear == true)
        {
            _p1Appear.sizeDelta = new Vector2(_firstWidth_X,_firstHeight_Y);
            _p2Appear.sizeDelta = new Vector2(_firstWidth_X,_firstHeight_Y);
            _firstWidth_X += _appearSpeed;
            _firstHeight_Y += _appearSpeed;
            if (_firstWidth_X >= _finalWidth_X && _firstHeight_Y >= _finalHeight_Y)
            {
                _firstWidth_X = _finalWidth_X;
                _firstHeight_Y = _finalHeight_Y;
                _isDecreased = false;
                _isIncreased = false;
            }
        }
    }
}

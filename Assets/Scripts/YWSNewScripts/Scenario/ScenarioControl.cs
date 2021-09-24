using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioControl : Player1Base
{
    private GameSceneManager _gameSceneManager;
    //セリフ・キャラ名テキスト用変数
    [SerializeField] private Text _word;
    [SerializeField] private Text _charaName;
    public string[] _mainStory_Text; //基本シナリオのセリフの配列
    public string[] _mainStory_Name; //基本シナリオのキャラ名の配列
    //public string[] KurotoStory;
    //public string[] SeasteyStory;
    //public string[] LuiceStory;
    //public string[] LuminaStory;
    public static int _textNum = 0; //シナリオの進み具合
    private string _displayText; //表示するセリフを入れる変数
    private int _textCharNum = 0; //セリフを一個ずつ追加するための変数
    private int _displayTextSpeed = 0; //全体のフレームレートを落とす変数
    public int _interval = 5; //Inspector上でセリフの速さを調節できる変数
    public static bool _click = false;
    private bool _repeatHit = false;
    public static bool _isScenarioEnd = false; //シナリオが終了したかどうかのフラグ
    //キャラ立ち絵用変数
    [SerializeField] private Image _leftCharacter;
    [SerializeField] private Image _rightCharacter;
    [SerializeField] private Sprite[] _leftCharacterImage; //左側のキャラ立ち絵の配列
    [SerializeField] private Sprite[] _rightCharacterImage; //右側のキャラ立ち絵の配列
    public int[] _whoIsTalking; //どっちのキャラが喋ってるのかを決める配列
    //背景用変数
    [SerializeField] private Image _background;
    [SerializeField] private Sprite[] _backgroundImage; //背景を入れる配列

    // Start is called before the first frame update
    void Start()
    {
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
        _leftCharacter.sprite = _leftCharacterImage[0];
        _rightCharacter.sprite = _rightCharacterImage[0];
        _leftCharacter.color = new Color(255,255,255,1);
        _rightCharacter.color = new Color(255,255,255,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameSceneManager.IsChanged == true && _isScenarioEnd == false)
        {
            //キャラ名を出す
            _charaName.text = _mainStory_Name[_textNum];
            //立ち絵を出す
            _leftCharacter.sprite = _leftCharacterImage[_textNum];
            _rightCharacter.sprite = _rightCharacterImage[_textNum];
            //キャラが一人しか居ない（＝もう片方の配列が空欄）の場合、そのもう片方を透明にする
            if (_rightCharacterImage[_textNum] == null)
            {
                _rightCharacter.color = new Color(255,255,255,0);
            }
            else
            {
                _rightCharacter.color = new Color(255,255,255,1);
            }
            //左側のキャラが喋ってる場合、右側のキャラを暗くする
            if (_whoIsTalking[_textNum] == 1)
            {
                if (_rightCharacterImage[_textNum] != null)
                {
                    _rightCharacter.color = new Color(0.5f,0.5f,0.5f,1);
                }
                _leftCharacter.color = new Color(255,255,255,1);
            }
            //右側のキャラが喋ってる場合、左側のキャラを暗くする
            else if (_whoIsTalking[_textNum] == 2)
            {
                if (_leftCharacterImage[_textNum] != null)
                {
                    _leftCharacter.color = new Color(0.5f,0.5f,0.5f,1);
                }
                _rightCharacter.color = new Color(255,255,255,1);
            }
            //セリフの表示
            _displayTextSpeed++;
            //_interval回に一回行う
            if (_displayTextSpeed % _interval == 0)
            {
                //一文字ずつセリフを足していく
                if (_textCharNum != _mainStory_Text[_textNum].Length)
                {
                    _displayText = _displayText + _mainStory_Text[_textNum][_textCharNum];
                    _textCharNum += 1;
                    //セリフが25文字以上の場合、改行させる
                    if (_textCharNum == 25)
                    {
                        _displayText = _displayText + "\n";
                    }
                }
                else
                {
                    //最後のセリフにたどり着いていない場合、ボタン入力に応じて次に進む
                    if (_textNum != _mainStory_Text.Length - 1)
                    {
                        if (_click == true)
                        {
                            _displayText = "";
                            _textCharNum = 0;
                            _textNum += 1;
                        }
                    }
                    else
                    { 
                        //最後のセリフが全部表示し切ったら、シナリオ終了判定を出す
                        if (_textCharNum == _mainStory_Text[_textNum].Length)
                        { 
                            _isScenarioEnd = true;
                        } 
                    } 
                }
                _word.text = _displayText;
                _repeatHit = false;
                _click = false;
            }

            if (_repeatHit == true)
            {
                return;
            }
            else if (_DS4_circle_value || Input.GetKeyDown(KeyCode.Space))
            {
                _repeatHit = true;
                _click = true;
            }
        } 
    }
}

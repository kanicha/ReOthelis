using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartA : Player1Base
{
    private GameSceneManager _gameSceneManager;
    //セリフ・キャラ名テキスト用変数
    [SerializeField] private Text _word;
    [SerializeField] private Text _charaName;
    public string[] _kurotoStory_Text; //クロトシナリオのセリフの配列
    public string[] _kurotoStory_Name; //クロトシナリオのキャラ名の配列
    public string[] _seasteyStory_Text; //シースティシナリオのセリフの配列
    public string[] _seasteyStory_Name; //シースティシナリオのキャラ名の配列
    public string[] _luiceStory_Text; //ルイスシナリオのセリフの配列
    public string[] _luiceStory_Name; //ルイスシナリオのキャラ名の配列
    public string[] _luminaStory_Text; //ルミナシナリオのセリフの配列
    public string[] _luminaStory_Name; //ルミナシナリオのキャラ名の配列
    private int _textNum = 0; //シナリオの進み具合
    private string _displayText; //表示するセリフを入れる変数
    private int _textCharNum = 0; //セリフを一個ずつ追加するための変数
    private int _displayTextSpeed = 0; //全体のフレームレートを落とす変数
    public int _interval = 5; //Inspector上でセリフの速さを調節できる変数
    private bool _click = false;
    private bool _repeatHit = false;
    public static bool _isScenarioEnd = false; //シナリオが終了したかどうかのフラグ
    //キャラ立ち絵用変数
    [SerializeField] private Image _leftCharacter;
    [SerializeField] private Image _rightCharacter;
    //左側のキャラ立ち絵の配列
    [SerializeField] private Sprite[] _kuroto_LeftCharacterImage;
    [SerializeField] private Sprite[] _seastey_LeftCharacterImage;
    [SerializeField] private Sprite[] _luice_LeftCharacterImage;
    [SerializeField] private Sprite[] _lumina_LeftCharacterImage;
    //右側のキャラ立ち絵の配列
    [SerializeField] private Sprite[] _kuroto_RightCharacterImage;
    [SerializeField] private Sprite[] _seastey_RightCharacterImage;
    [SerializeField] private Sprite[] _luice_RightCharacterImage;
    [SerializeField] private Sprite[] _lumina_RightCharacterImage;
    //どっちのキャラが喋ってるのかを決める配列
    public int[] _kuroto_WhoIsTalking;
    public int[] _seastey_WhoIsTalking;
    public int[] _luice_WhoIsTalking;
    public int[] _lumina_WhoIsTalking;
    //背景用変数
    [SerializeField] private Image _background;
    //背景を入れる配列
    [SerializeField] private Sprite[] _kuroto_BackgroundImage;
    [SerializeField] private Sprite[] _seastey_BackgroundImage;
    [SerializeField] private Sprite[] _luice_BackgroundImage;
    [SerializeField] private Sprite[] _lumina_BackgroundImage;
    private int _part = 0;
    private bool _isPartEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
        if (CharaImageMoved.charaType1P == CharaImageMoved.CharaType1P.Tiger)
        {
            _leftCharacter.sprite = _kuroto_LeftCharacterImage[0];
            _rightCharacter.sprite = _kuroto_RightCharacterImage[0];
            _background.sprite = _kuroto_BackgroundImage[0];
        }
        else if (CharaImageMoved.charaType1P == CharaImageMoved.CharaType1P.Cow)
        {
            _leftCharacter.sprite = _seastey_LeftCharacterImage[0];
            _rightCharacter.sprite = _seastey_RightCharacterImage[0];
            _background.sprite = _seastey_BackgroundImage[0];
        }
        else if (CharaImageMoved.charaType1P == CharaImageMoved.CharaType1P.Rabbit)
        {
            _leftCharacter.sprite = _luice_LeftCharacterImage[0];
            _rightCharacter.sprite = _luice_RightCharacterImage[0];
            _background.sprite = _luice_BackgroundImage[0];
        }
        else if (CharaImageMoved.charaType1P == CharaImageMoved.CharaType1P.Mouse)
        {
            _leftCharacter.sprite = _lumina_LeftCharacterImage[0];
            _rightCharacter.sprite = _lumina_RightCharacterImage[0];
            _background.sprite = _lumina_BackgroundImage[0];
        }
        _leftCharacter.color = new Color(255,255,255,0);
        _rightCharacter.color = new Color(255,255,255,0);
        _textNum = 0;
        _isScenarioEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameSceneManager.IsChanged == true && _isScenarioEnd == false)
        {
            if (_isPartEnd == true)
            {
                if (CharaImageMoved.charaType1P == CharaImageMoved.CharaType1P.Tiger)
                {
                    _background.sprite = _kuroto_BackgroundImage[_part];
                }
                else if (CharaImageMoved.charaType1P == CharaImageMoved.CharaType1P.Mouse)
                {
                    _background.sprite = _luice_BackgroundImage[_part];
                }
                _isPartEnd = false;
            }
            else
            {
                if (CharaImageMoved.charaType1P == CharaImageMoved.CharaType1P.Tiger)
                {
                    //キャラ名を出す
                    _charaName.text = _kurotoStory_Name[_textNum];
                    //立ち絵を出す
                    _leftCharacter.sprite = _kuroto_LeftCharacterImage[_textNum];
                    _rightCharacter.sprite = _kuroto_RightCharacterImage[_textNum];
                    //キャラが一人しか居ない（＝もう片方の配列が空欄）の場合、そのもう片方を透明にする
                    if (_kuroto_LeftCharacterImage[_textNum] == null)
                    {
                        _leftCharacter.color = new Color(255,255,255,0);
                    }
                    else
                    {
                        _leftCharacter.color = new Color(255,255,255,1);
                    }
                    if (_kuroto_RightCharacterImage[_textNum] == null)
                    {
                        _rightCharacter.color = new Color(255,255,255,0);
                    }
                    else
                    {
                        _rightCharacter.color = new Color(255,255,255,1);
                    }
                    //左側のキャラが喋ってる場合、右側のキャラを暗くする
                    if (_kuroto_WhoIsTalking[_textNum] == 1)
                    {
                        if (_kuroto_RightCharacterImage[_textNum] != null)
                        {
                            _rightCharacter.color = new Color(0.5f,0.5f,0.5f,1);
                        }
                        _leftCharacter.color = new Color(255,255,255,1);
                    }
                    //右側のキャラが喋ってる場合、左側のキャラを暗くする
                    else if (_kuroto_WhoIsTalking[_textNum] == 2)
                    {
                        if (_kuroto_LeftCharacterImage[_textNum] != null)
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
                        if (_textCharNum != _kurotoStory_Text[_textNum].Length)
                        {
                            _displayText = _displayText + _kurotoStory_Text[_textNum][_textCharNum];
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
                            if (_textNum != _kurotoStory_Text.Length - 1)
                            {
                                if (_click == true)
                                {
                                    _displayText = "";
                                    _textCharNum = 0;
                                    _textNum += 1;
                                    if (_textNum == 8)
                                    {
                                        _isPartEnd = true;
                                        _part = 1;
                                    }
                                }
                            }
                            else
                            {
                                //最後のセリフが全部表示し切ったら、シナリオ終了判定を出す
                                if (_textCharNum == _kurotoStory_Text[_textNum].Length)
                                { 
                                    _isScenarioEnd = true;
                                } 
                            }
                        }
                        _word.text = _displayText;
                        _repeatHit = false;
                        _click = false;
                    }
                }
                else if (CharaImageMoved.charaType1P == CharaImageMoved.CharaType1P.Cow)
                {
                    //キャラ名を出す
                    _charaName.text = _seasteyStory_Name[_textNum];
                    //立ち絵を出す
                    _leftCharacter.sprite = _seastey_LeftCharacterImage[_textNum];
                    _rightCharacter.sprite = _seastey_RightCharacterImage[_textNum];
                    //キャラが一人しか居ない（＝もう片方の配列が空欄）の場合、そのもう片方を透明にする
                    if (_seastey_LeftCharacterImage[_textNum] == null)
                    {
                        _leftCharacter.color = new Color(255,255,255,0);
                    }
                    else
                    {
                        _leftCharacter.color = new Color(255,255,255,1);
                    }
                    if (_seastey_RightCharacterImage[_textNum] == null)
                    {
                        _rightCharacter.color = new Color(255,255,255,0);
                    }
                    else
                    {
                        _rightCharacter.color = new Color(255,255,255,1);
                    }
                    //左側のキャラが喋ってる場合、右側のキャラを暗くする
                    if (_seastey_WhoIsTalking[_textNum] == 1)
                    {
                        if (_seastey_RightCharacterImage[_textNum] != null)
                        {
                            _rightCharacter.color = new Color(0.5f,0.5f,0.5f,1);
                        }
                        _leftCharacter.color = new Color(255,255,255,1);
                    }
                    //右側のキャラが喋ってる場合、左側のキャラを暗くする
                    else if (_seastey_WhoIsTalking[_textNum] == 2)
                    {
                        if (_seastey_LeftCharacterImage[_textNum] != null)
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
                        if (_textCharNum != _seasteyStory_Text[_textNum].Length)
                        {
                            _displayText = _displayText + _seasteyStory_Text[_textNum][_textCharNum];
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
                            if (_textNum != _seasteyStory_Text.Length - 1)
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
                                if (_textCharNum == _seasteyStory_Text[_textNum].Length)
                                { 
                                    _isScenarioEnd = true;
                                } 
                            }
                        }
                        _word.text = _displayText;
                        _repeatHit = false;
                        _click = false;
                    }
                }
                else if (CharaImageMoved.charaType1P == CharaImageMoved.CharaType1P.Rabbit)
                {
                    //キャラ名を出す
                    _charaName.text = _luminaStory_Name[_textNum];
                    //立ち絵を出す
                    _leftCharacter.sprite = _lumina_LeftCharacterImage[_textNum];
                    _rightCharacter.sprite = _lumina_RightCharacterImage[_textNum];
                    //キャラが一人しか居ない（＝もう片方の配列が空欄）の場合、そのもう片方を透明にする
                    if (_lumina_LeftCharacterImage[_textNum] == null)
                    {
                        _leftCharacter.color = new Color(255,255,255,0);
                    }
                    else
                    {
                        _leftCharacter.color = new Color(255,255,255,1);
                    }
                    if (_lumina_RightCharacterImage[_textNum] == null)
                    {
                        _rightCharacter.color = new Color(255,255,255,0);
                    }
                    else
                    {
                        _rightCharacter.color = new Color(255,255,255,1);
                    }
                    //左側のキャラが喋ってる場合、右側のキャラを暗くする
                    if (_lumina_WhoIsTalking[_textNum] == 1)
                    {
                        if (_lumina_RightCharacterImage[_textNum] != null)
                        {
                            _rightCharacter.color = new Color(0.5f,0.5f,0.5f,1);
                        }
                        _leftCharacter.color = new Color(255,255,255,1);
                    }
                    //右側のキャラが喋ってる場合、左側のキャラを暗くする
                    else if (_lumina_WhoIsTalking[_textNum] == 2)
                    {
                        if (_lumina_LeftCharacterImage[_textNum] != null)
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
                        if (_textCharNum != _luminaStory_Text[_textNum].Length)
                        {
                            _displayText = _displayText + _luminaStory_Text[_textNum][_textCharNum];
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
                            if (_textNum != _luminaStory_Text.Length - 1)
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
                                if (_textCharNum == _luminaStory_Text[_textNum].Length)
                                { 
                                    _isScenarioEnd = true;
                                } 
                            }
                        }
                        _word.text = _displayText;
                        _repeatHit = false;
                        _click = false;
                    }
                }
                else if (CharaImageMoved.charaType1P == CharaImageMoved.CharaType1P.Mouse)
                {
                    //キャラ名を出す
                    _charaName.text = _luiceStory_Name[_textNum];
                    //立ち絵を出す
                    _leftCharacter.sprite = _luice_LeftCharacterImage[_textNum];
                    _rightCharacter.sprite = _luice_RightCharacterImage[_textNum];
                    //キャラが一人しか居ない（＝もう片方の配列が空欄）の場合、そのもう片方を透明にする
                    if (_luice_LeftCharacterImage[_textNum] == null)
                    {
                        _leftCharacter.color = new Color(255,255,255,0);
                    }
                    else
                    {
                        _leftCharacter.color = new Color(255,255,255,1);
                    }
                    if (_luice_RightCharacterImage[_textNum] == null)
                    {
                        _rightCharacter.color = new Color(255,255,255,0);
                    }
                    else
                    {
                        _rightCharacter.color = new Color(255,255,255,1);
                    }
                    //左側のキャラが喋ってる場合、右側のキャラを暗くする
                    if (_luice_WhoIsTalking[_textNum] == 1)
                    {
                        if (_luice_RightCharacterImage[_textNum] != null)
                        {
                            _rightCharacter.color = new Color(0.5f,0.5f,0.5f,1);
                        }
                        _leftCharacter.color = new Color(255,255,255,1);
                    }
                    //右側のキャラが喋ってる場合、左側のキャラを暗くする
                    else if (_luice_WhoIsTalking[_textNum] == 2)
                    {
                        if (_luice_LeftCharacterImage[_textNum] != null)
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
                        if (_textCharNum != _luiceStory_Text[_textNum].Length)
                        {
                            _displayText = _displayText + _luiceStory_Text[_textNum][_textCharNum];
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
                            if (_textNum != _luiceStory_Text.Length - 1)
                            {
                                if (_click == true)
                                {
                                    _displayText = "";
                                    _textCharNum = 0;
                                    _textNum += 1;
                                    if (_textNum == 2)
                                    {
                                        _isPartEnd = true;
                                        _part = 1;
                                    }
                                }
                            }
                            else
                            {
                                //最後のセリフが全部表示し切ったら、シナリオ終了判定を出す
                                if (_textCharNum == _luiceStory_Text[_textNum].Length)
                                { 
                                    _isScenarioEnd = true;
                                } 
                            }
                        }
                        _word.text = _displayText;
                        _repeatHit = false;
                        _click = false;
                    }
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
}
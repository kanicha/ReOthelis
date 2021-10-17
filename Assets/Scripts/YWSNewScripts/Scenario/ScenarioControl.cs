using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScenarioControl : Player1Base
{
    //csvファイル用変数
    public TextAsset _csvFile;
    List<string[]> _scenarioData = new List<string[]>();
    //並び順
    //｜番号｜背景｜左立ち絵｜右立ち絵｜キャラ名｜喋っているキャラ｜セリフ｜
    private GameSceneManager _gameSceneManager;
    //セリフ・キャラ名テキスト用変数
    [SerializeField] private Text _word;
    [SerializeField] private Text _charaName;
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
    [SerializeField] private Sprite[] _leftCharacterImage; //左側のキャラ立ち絵の配列
    [SerializeField] private Sprite[] _rightCharacterImage; //右側のキャラ立ち絵の配列
    //背景用変数
    [SerializeField] private Image _background;
    [SerializeField] private Sprite[] _backgroundImage; //背景を入れる配列
    [SerializeField] private Image _pageFeed;
    public RectTransform _feedMove;
    private int counter = 0;
    private float move = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        base.SaveKeyValue();
        base.KeyInput();
        PageFeedMove();
        
        if (_gameSceneManager.IsChanged == true && _isScenarioEnd == false)
        {
            //背景の表示
            int _bgNum = int.Parse(_scenarioData[_textNum][1]);
            _background.sprite = _backgroundImage[_bgNum];

            //キャラクターの表示
            ShowCharacter();

            //セリフの表示
            ShowText();

            if (_repeatHit == true)
            {
                return;
            }
                
            if (_DS4_circle_value || Input.GetKeyDown(KeyCode.Space))
            {
                _repeatHit = true;
                _click = true;
            }
        } 
    }

    private void Init()
    {
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
        _leftCharacter.sprite = _leftCharacterImage[0];
        _rightCharacter.sprite = _rightCharacterImage[0];
        _leftCharacter.color = new Color(255,255,255,0);
        _rightCharacter.color = new Color(255,255,255,0);
        _background.sprite = _backgroundImage[0];
        _textNum = 0;
        _isScenarioEnd = false;
        _click = false;
        _pageFeed.color = new Color(255,255,255,0);

        //_csvFile = Resources.Load("csv/Oseris_01") as TextAsset;
        StringReader reader = new StringReader(_csvFile.text);

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            _scenarioData.Add(line.Split(','));
        }
    }

    private void ShowCharacter()
    {
        //キャラ名を出す
        _charaName.text = _scenarioData[_textNum][4];
        //立ち絵を出す
        //左側のキャラが喋ってる場合
        if (_scenarioData[_textNum][5] == "1")
        {
            int _leftCharacterNum = int.Parse(_scenarioData[_textNum][2]);
            _leftCharacter.sprite = _leftCharacterImage[_leftCharacterNum];
            _leftCharacter.color = new Color(255,255,255,1);
            //キャラが一人しか居ない場合、右側のキャラを透明にする
            if (_scenarioData[_textNum][3] == "0")
            {
                _rightCharacter.color = new Color(255,255,255,0);
            }
            //右側にキャラが存在する場合、右側のキャラを暗くする
            else
            {
                _rightCharacter.color = new Color(0.5f,0.5f,0.5f,1);
            }
        }
        //右側のキャラが喋ってる場合
        else if (_scenarioData[_textNum][5] == "2")
        {
            int _rightCharacterNum = int.Parse(_scenarioData[_textNum][3]);
            _rightCharacter.sprite = _rightCharacterImage[_rightCharacterNum];
            _rightCharacter.color = new Color(255,255,255,1);
            //キャラが一人しか居ない場合、左側のキャラを透明にする
            if (_scenarioData[_textNum][2] == "0")
            {
                _leftCharacter.color = new Color(255,255,255,0);
            }
            //左側にキャラが存在する場合、左側のキャラを暗くする
            else
            {
                _leftCharacter.color = new Color(0.5f,0.5f,0.5f,1);
            }
        }
    }

    private void ShowText()
    {
        int _itemNum = _scenarioData.Count;
        _displayTextSpeed++;
        //_interval回に一回行う
        if (_displayTextSpeed % _interval == 0)
        {
            //一文字ずつセリフを足していく
            if (_textCharNum != _scenarioData[_textNum][6].Length)
            { 
                _displayText = _displayText + _scenarioData[_textNum][6][_textCharNum];
                _textCharNum += 1;
                //セリフが25文字以上の場合、改行させる
                if (_textCharNum == 25)
                {
                    _displayText = _displayText + "\n";
                }
            }
            else
            {
                _pageFeed.color = new Color(255,255,255,1);
                //最後のセリフにたどり着いていない場合、ボタン入力に応じて次に進む
                if (_textNum != _itemNum - 1)
                {
                    if (_click == true)
                    {
                        SoundManager.Instance.PlaySE(10);
                                
                        _pageFeed.color = new Color(255,255,255,0);
                        _displayText = "";
                        _textCharNum = 0;
                        _textNum += 1;
                    }
                }
                else
                {
                    //最後のセリフが全部表示し切ったら、シナリオ終了判定を出す
                    if (_textCharNum == _scenarioData[_textNum][6].Length)
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

    private void PageFeedMove()
    {
        _feedMove.position += new Vector3(0,move,0);
        counter++;
        if (counter == 100)
        {
            counter = 0;
            move *= -1;
        }
    }
}
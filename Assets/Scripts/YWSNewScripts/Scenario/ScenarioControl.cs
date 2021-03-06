using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScenarioControl : Player1Base
{
    //csvファイル用変数
    public TextAsset _csvFile;
    public List<string[]> _scenarioData = new List<string[]>();
    //並び順
    //｜番号｜背景｜左立ち絵｜右立ち絵｜キャラ名｜喋っているキャラ｜セリフ｜
    protected GameSceneManager _gameSceneManager;
    //セリフ・キャラ名テキスト用変数
    [SerializeField] private Text _word;
    [SerializeField] private Text _charaName;
    public int _textNum = 1; //シナリオの進み具合
    private string _displayText; //表示するセリフを入れる変数
    private int _textCharNum = 0; //セリフを一個ずつ追加するための変数
    private int _displayTextSpeed = 0; //全体のフレームレートを落とす変数
    [SerializeField, Header("セリフ送りの速さ")] int _interval = 5; 
    protected bool _click = false;
    protected bool _repeatHit = false;
    public static bool _isScenarioEnd = false; //シナリオが終了したかどうかのフラグ
    //キャラ立ち絵用変数
    [SerializeField] private Image _leftCharacter;
    [SerializeField] private Image _rightCharacter;
    [SerializeField] private Sprite[] _characterImage; //キャラ立ち絵の配列
    private int _leftCharacterNum = 0;
    private int _rightCharacterNum = 0;
    //背景用変数
    [SerializeField] private Image _background;
    [SerializeField] private Sprite[] _backgroundImage; //背景を入れる配列
    [SerializeField] private Image _pageFeed;
    private int _bgNum = 0;
    public RectTransform _feedMove;
    private int counter = 0;
    private float move = 0.5f;
    [SerializeField] public Image _fadeImage = null; //画面フェードのためのイメージ
    private bool _isFadeInFin = false;
    private bool _isFadeOutFin = false;
    private float _color = 0;
    public bool _isVoicePlayed = false;

    // Start is called before the first frame update
    private void Start()
    {
        SetCsv();
        Init();
        SoundManager.Instance.SetCommonStory();
    }

    // Update is called once per frame
    private void Update()
    {
        base.SaveKeyValue();
        base.KeyInput();
        PageFeedMove();
        
        if (_gameSceneManager.IsChanged == true && _isScenarioEnd == false)
        {
            if (_scenarioData[_textNum][7] != "")
            {
                //背景の表示
                ShowBackground();

                //キャラクターの表示
                ShowCharacter();

                if (_isVoicePlayed == false)
                {
                    //セリフボイスを流す
                    SoundManager.Instance.PlayStoryVoice(_textNum - 1);
                    _isVoicePlayed = true;
                }

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
            else
            {
                _fadeImage.color = new Color(0, 0, 0, _color);
                if (_isFadeInFin == false)
                {
                    _color += Time.deltaTime;
                    if (_color >= 1)
                    {
                        _color = 1;

                        //背景の表示
                        ShowBackground();

                        //キャラクターの表示
                        ShowCharacter();

                        _isFadeInFin = true;
                    }
                }
                else if (_isFadeInFin == true)
                {
                    _color -= Time.deltaTime;
                    if (_color <= 0)
                    {
                        _color = 0;
                        _isFadeOutFin = true;
                        _textNum++;
                    }
                }
            }
        } 
    }

    private void SetCsv()
    {
        //_csvFile = Resources.Load("csv/Oseris_01") as TextAsset;
        StringReader reader = new StringReader(_csvFile.text);

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            _scenarioData.Add(line.Split(','));
        }
    }

    public void Init()
    {
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
        _textNum = 1;
        _leftCharacterNum = int.Parse(_scenarioData[_textNum][2]);
        _leftCharacter.sprite = _characterImage[_leftCharacterNum];
        _rightCharacterNum = int.Parse(_scenarioData[_textNum][3]);
        _rightCharacter.sprite = _characterImage[_rightCharacterNum];
        _leftCharacter.color = new Color(255,255,255,0);
        _rightCharacter.color = new Color(255,255,255,0);
        _bgNum = int.Parse(_scenarioData[_textNum][1]);
        _background.sprite = _backgroundImage[_bgNum];
        _isScenarioEnd = false;
        _click = false;
        _pageFeed.color = new Color(255,255,255,0);
        _fadeImage.color = new Color(0, 0, 0, 0);
        _isFadeInFin = false;
        _isFadeOutFin = false;
    }

    public void ShowBackground()
    {
        _bgNum = int.Parse(_scenarioData[_textNum][1]);
        _background.sprite = _backgroundImage[_bgNum];
    }

    public void ShowCharacter()
    {
        //キャラ名を出す
        _charaName.text = _scenarioData[_textNum][4];
        //立ち絵を出す
        _leftCharacterNum = int.Parse(_scenarioData[_textNum][2]);
        _leftCharacter.sprite = _characterImage[_leftCharacterNum];
        _rightCharacterNum = int.Parse(_scenarioData[_textNum][3]);
        _rightCharacter.sprite = _characterImage[_rightCharacterNum];

        //立ち絵のないキャラを透明にする
        if (_leftCharacterNum == 0)
        {
            _leftCharacter.color = new Color(255,255,255,0);
        }
        if (_rightCharacterNum == 0)
        {
            _rightCharacter.color = new Color(255,255,255,0);
        }

        //左側のキャラが喋ってる場合
        if (_scenarioData[_textNum][5] == "1")
        {
            if (_leftCharacterNum != 0)
            {
                _leftCharacter.color = new Color(255,255,255,1);
            }
            //右側にキャラが存在する場合、右側のキャラを暗くする
            if (_rightCharacterNum != 0)
            {
                _rightCharacter.color = new Color(0.5f,0.5f,0.5f,1);
            }
        }
        //右側のキャラが喋ってる場合
        else if (_scenarioData[_textNum][5] == "2")
        {
            if (_rightCharacterNum != 0)
            {
                _rightCharacter.color = new Color(255,255,255,1);
            }
            //左側にキャラが存在する場合、左側のキャラを暗くする
            if (_leftCharacterNum != 0)
            {
                _leftCharacter.color = new Color(0.5f,0.5f,0.5f,1);
            }
        }
    }

    public void ShowText()
    {
        int _itemNum = _scenarioData.Count;
        _displayTextSpeed++;
        //_interval回に一回行う
        if (_displayTextSpeed % _interval == 0)
        {
            //一文字ずつセリフを足していく
            if (_textCharNum != _scenarioData[_textNum][7].Length)
            {
                if (_scenarioData[_textNum][7][_textCharNum] == 'n')
                {
                    _displayText += "\n";
                    _textCharNum += 1;
                }
                else
                {
                    _displayText += _scenarioData[_textNum][7][_textCharNum];
                    _textCharNum += 1;
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
                        SoundManager.Instance.StopStoryVoice();
                        _isVoicePlayed = false;
                    }
                }
                else
                {
                    //最後のセリフが全部表示し切ったら、シナリオ終了判定を出す
                    if (_textCharNum == _scenarioData[_textNum][7].Length)
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

    public void PageFeedMove()
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
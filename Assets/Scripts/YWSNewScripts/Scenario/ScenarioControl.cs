using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioControl : Player1Base
{
    private GameSceneManager _gameSceneManager;
    [SerializeField] private Text _word;
    [SerializeField] private Text _charaName;
    public string[] _mainStory_Text;
    public string[] _mainStory_Name;
    //public string[] KurotoStory;
    //public string[] SeasteyStory;
    //public string[] LuiceStory;
    //public string[] LuminaStory;
    public static int _textNum = 0;
    private string _displayText;
    private int _textCharNum = 0;
    private int _displayTextSpeed = 0; //全体のフレームレートを落とす変数
    public int _interval = 5;
    public static bool _click = false;
    private bool _repeatHit = false;
    public static bool _isScenarioEnd = false;
    [SerializeField] private Image _leftCharacter;
    [SerializeField] private Image _rightCharacter;
    [SerializeField] private Sprite[] _leftCharacterImage;
    [SerializeField] private Sprite[] _rightCharacterImage;
    public int[] _whoIsTalking;

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
            _charaName.text = _mainStory_Name[_textNum];

            _leftCharacter.sprite = _leftCharacterImage[_textNum];
            _rightCharacter.sprite = _rightCharacterImage[_textNum];

            if (_rightCharacterImage[_textNum] == null)
            {
                _rightCharacter.color = new Color(255,255,255,0);
            }
            else
            {
                _rightCharacter.color = new Color(255,255,255,1);
            }
            if (_whoIsTalking[_textNum] == 1)
            {
                if (_rightCharacterImage[_textNum] != null)
                {
                    _rightCharacter.color = new Color(0.5f,0.5f,0.5f,1);
                }
                _leftCharacter.color = new Color(255,255,255,1);
            }
            else if (_whoIsTalking[_textNum] == 2)
            {
                if (_leftCharacterImage[_textNum] != null)
                {
                    _leftCharacter.color = new Color(0.5f,0.5f,0.5f,1);
                }
                _rightCharacter.color = new Color(255,255,255,1);
            }

            _displayTextSpeed++;
            if (_displayTextSpeed % _interval == 0)
            {
                if (_textCharNum != _mainStory_Text[_textNum].Length)
                {
                    _displayText = _displayText + _mainStory_Text[_textNum][_textCharNum];
                    _textCharNum += 1;
                    if (_textCharNum == 25)
                    {
                        _displayText = _displayText + "\n";
                    }
                }
                else
                {
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

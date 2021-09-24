using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : Player1Base
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
    public static bool _isScenarioEnd = false;
    void Start()
    {
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
    }
    void Update()
    {
        if (_gameSceneManager.IsChanged == true && _isScenarioEnd == false)
        {
            _charaName.text = _mainStory_Name[_textNum];
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
                _click = false;
            }
            if (_DS4_circle_value || Input.GetKeyDown(KeyCode.Space))
            {
                _click = true;
            }
        } 
    }
}
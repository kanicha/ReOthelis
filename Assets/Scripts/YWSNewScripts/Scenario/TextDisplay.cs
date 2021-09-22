using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : Player1Base
{
    private GameSceneManager _gameSceneManager;
    public string[] MainStory;
    public string[] KurotoStory;
    public string[] SeasteyStory;
    public string[] LuiceStory;
    public string[] LuminaStory;
    private int TextNumber;//何番目のtexts[]を表示させるか
    private string DisplayText;//表示させるstring
    private int TextCharNumber;//何文字目をdisplayTextに追加するか
    public int DisplayTextSpeed; //全体のフレームレートを落とす変数
    private bool Click = false;
    public static bool IsScenarioEnd = false;
    void Start()
    {
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
    }
    void Update()
    {
        if (_gameSceneManager.IsChanged == true && IsScenarioEnd == false)
        { 
            DisplayTextSpeed++;
            if (DisplayTextSpeed % 5 == 0)
            {
                if (TextCharNumber != MainStory[TextNumber].Length)//もしtext[textNumber]の文字列の文字が最後の文字じゃなければ
                {
                    DisplayText = DisplayText + MainStory[TextNumber][TextCharNumber];//displayTextに文字を追加していく
                    TextCharNumber = TextCharNumber + 1;//次の文字にする
                }
                else//もしtext[textNumber]の文字列の文字が最後の文字だったら
                {
                    if (TextNumber != MainStory.Length - 1)//もしtexts[]が最後のセリフじゃないときは
                    {
                        if (Click == true)//クリックされた判定
                        {
                            DisplayText = "";//表示させる文字列を消す
                            TextCharNumber = 0;//文字の番号を最初にする
                            TextNumber = TextNumber + 1;//次のセリフにする
                        }
                    }
                    else //もしtexts[]が最後のセリフになったら
                    { 
                        if (TextCharNumber == MainStory[TextNumber].Length) //クリックされた判定
                        { 
                            IsScenarioEnd = true;
                        } 
                    } 
                }

                this.GetComponent<Text>().text = DisplayText;
                Click = false;
            }
            if (_DS4_circle_value || Input.GetKeyDown(KeyCode.Space))
            {
                Click = true;
            }
        } 
    }
}
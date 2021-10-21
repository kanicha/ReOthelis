using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CharacterScenario : ScenarioControl
{
    //csvファイル用変数
    public TextAsset _csv_Kuroto;
    public TextAsset _csv_Seastey;
    public TextAsset _csv_Luice;
    public TextAsset _csv_Lumina;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayBGM(4);
        SetCsv();
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
            ShowBackground();

            //キャラクターの表示
            ShowCharacter();

            //セリフの表示
            ShowText();

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

    private void SetCsv()
    {
        StringReader reader = null;
        //_csvFile = Resources.Load("csv/Oseris_01") as TextAsset;
        if (CharaImageMoved.charaType1P == CharaImageMoved.CharaType1P.Tiger)
        {
            reader = new StringReader(_csv_Kuroto.text);
        }
        else if (CharaImageMoved.charaType1P == CharaImageMoved.CharaType1P.Cow)
        {
            reader = new StringReader(_csv_Seastey.text);
        }
        else if (CharaImageMoved.charaType1P == CharaImageMoved.CharaType1P.Rabbit)
        {
            reader = new StringReader(_csv_Lumina.text);
        }
        else if (CharaImageMoved.charaType1P == CharaImageMoved.CharaType1P.Mouse)
        {
            reader = new StringReader(_csv_Luice.text);
        }

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            _scenarioData.Add(line.Split(','));
        }
    }
}
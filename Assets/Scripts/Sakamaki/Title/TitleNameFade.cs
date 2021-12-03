using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleNameFade : MonoBehaviour
{
    [SerializeField, Header("点滅させるオブジェクト")]
    private Text[] _modeSelectTextArray = new Text[3];

    [SerializeField, Header("点滅させるスピード")] private float _blinkSpeed = 0.0f;

    // 時間計測変数
    private float _sceneTime = 0.0f;
    int _lastnum = 0;
    
    private void Start()
    {
        // 値を初期化
        _modeSelectTextArray[0].color = GetAlphaColor(_modeSelectTextArray[0].color);
    }

    // Update is called once per frame
    void Update()
    {
        // モードセレクトの値に応じてフェードさせるオブジェクトを変更
        _modeSelectTextArray[ModeSelect._selectCount].color = GetAlphaColor(_modeSelectTextArray[ModeSelect._selectCount].color);

        // 前の値とセレクトの値が違った場合色を初期化
        if (_lastnum != ModeSelect._selectCount)
            _modeSelectTextArray[_lastnum].color = new Color(255, 255, 255, 255);
        
        // モードの値におうじてフェード
        if (ModeSelect._selectCount == 0)
        {
            _modeSelectTextArray[0].color = GetAlphaColor(_modeSelectTextArray[0].color);
            _lastnum = 0;
        }
        else if (ModeSelect._selectCount == 1)
        {
            _modeSelectTextArray[1].color = GetAlphaColor(_modeSelectTextArray[1].color);
            _lastnum = 1;
        }
        else if (ModeSelect._selectCount == 2)
        {
            _modeSelectTextArray[2].color = GetAlphaColor(_modeSelectTextArray[2].color);
            _lastnum = 2;
        }
    }

    //Alpha値を更新してColorを返す
    Color GetAlphaColor(Color color)
    {
        _sceneTime += Time.deltaTime * 5.0f * _blinkSpeed;
        color.a = Mathf.Sin(_sceneTime) * 0.5f + 0.5f;

        return color;
    }
}
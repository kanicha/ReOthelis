using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoTextFade : MonoBehaviour
{
    [SerializeField, Header("点滅させるオブジェクト")] private Text _demoPlayText;
    [SerializeField, Header("点滅させるスピード")] private float _blinkSpeed = 0.0f;

    // 時間計測変数
    private float _sceneTime = 0.0f;

    private void Start()
    {
        _demoPlayText.GetComponent<Text>();
        
        // 値を初期化
        _demoPlayText.color = new Color(255, 255, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {
        _demoPlayText.color = GetAlphaColor(_demoPlayText.color);
    }
    
    //Alpha値を更新してColorを返す
    Color GetAlphaColor(Color color) {
        _sceneTime += Time.deltaTime * 5.0f * _blinkSpeed;
        color.a = Mathf.Sin(_sceneTime) * 0.5f + 0.5f;

        return color;
    }
}

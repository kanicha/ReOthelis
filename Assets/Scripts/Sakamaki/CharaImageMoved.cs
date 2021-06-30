using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaImageMoved : MonoBehaviour
{
    [SerializeField] private Image charactorImage;
    [SerializeField] private Sprite[] CharactorImageArray;
    [SerializeField] private Player1 p1;

    private int _frameCount = 0;
    private int _moveSpeed = 10;
    private int _next = 0;
    private int _prev = 0;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化処理
        charactorImage.sprite = CharactorImageArray[0];
    }

    // Update is called once per frame
    void Update()
    {
        _frameCount++;
        _frameCount %= _moveSpeed;

        if (_frameCount == 0)
        {
            // 入力部分
            if (p1._horizontal < 0 || Input.GetKeyDown(KeyCode.A))
                _next--;
            else if (p1._horizontal > 0 || Input.GetKeyDown(KeyCode.D))
                _next++;
        }

        // prev と result 変数の中身(int型)が違った場合描画処理
        if (_prev != _next)
        {
            _prev = _next;

            if (_next < 0)
            {
                _next = CharactorImageArray.Length - 1;
            }
            else if (_next >= CharactorImageArray.Length)
            {
                _next = 0;
            }

            charactorImage.sprite = CharactorImageArray[_next];
        }
    }
}
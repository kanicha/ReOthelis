using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : Player1Base
{
    private BlackOutControl _blackOutControl;
    public static bool _isIntroEnd = false;
    public Text _text;
    private string _showText;
    public Image _image;
    [SerializeField] private Sprite[] _showImage;

    // Start is called before the first frame update
    void Start()
    {
        _blackOutControl.FadeIn();
        _text.text = _showText;
        _image.sprite = _showImage[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || _DS4_circle_value || Input.GetKeyDown(KeyCode.X) || _DS4_cross_value)
        {
            _blackOutControl.FadeOut();
            _isIntroEnd = true;
        }
    }
}

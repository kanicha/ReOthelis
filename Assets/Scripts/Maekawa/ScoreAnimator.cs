using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAnimator : SingletonMonoBehaviour<ScoreAnimator>
{
    [SerializeField]
    private float _duration = 1 / 60 * 50;// 1/60fpsÇ≈50fÇ©ÇØÇƒè„Ç…à⁄ìÆ
    [SerializeField]
    private float _distance = 80;
    [SerializeField]
    private Text _text = null;
    [SerializeField]
    private Animator _animator = null;
    private bool _isActive = false;
    private float _passedDistance = 0;
    private const float _PARENT_WIDTH = 720;
    private const float _PARENT_HEIGHT = 900;
    private const float _CELL_NUM_X = 8;
    private const float _CELL_NUM_Y = 10;

    private void Update()
    {
        if(_isActive)
        {
            float moveY = _distance * Time.deltaTime * _duration;
            _passedDistance += moveY;
            _text.rectTransform.localPosition += Vector3.up * moveY;

            if (_distance < _passedDistance)
                _isActive = false;
        }
    }

    public void OnAddScore(CharaImageMoved.CharaType1P type, Vector3 pos, int point)
    {
        _text.text = point.ToString();
        _text.rectTransform.anchoredPosition = new Vector2(_PARENT_WIDTH / _CELL_NUM_X * (pos.x - 1), _PARENT_HEIGHT / _CELL_NUM_Y * pos.z);
        Debug.Log(_text.rectTransform.localPosition);
        _isActive = true;
        _passedDistance = 0;
        _animator.Play("Score", 0, 0);
    }
}

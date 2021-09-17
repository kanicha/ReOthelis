using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWindowControl : PlayerBase
{
    [SerializeField] GameObject _skillWindow = null;
    [SerializeField] GameObject _skillWindowTarget = null;
    [SerializeField, Range(0, 10)] float _moveSpeed = 1f;
    private Vector3 _windowPos;
    private Vector3 _targetPos;
    private Vector3 _lastPos;

    private float _startTime;
    private float rate;
    private bool isCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化処理
        _windowPos = _skillWindow.transform.position;
        _targetPos = _skillWindowTarget.transform.position;

        if (_moveSpeed <= 0)
        {
            _windowPos = _targetPos;
        }
    }

    public void ShowSkillWindow()
    {
        StartCoroutine(ShowSkillWindowCoroutine());
    }

    IEnumerator ShowSkillWindowCoroutine()
    {
        // シーンがロードされた時からの時間 - 初期値
        var diff = Time.timeSinceLevelLoad - _startTime;

        if (diff > _moveSpeed)
        {
            _lastPos = _targetPos;
            _skillWindow.transform.position = _lastPos;
        }

        rate = diff / _moveSpeed;

        switch (isCheck)
        {
            case false:
                isCheck = true;
                _skillWindow.transform.position = Vector3.Lerp(_windowPos, _targetPos, rate);
                break;
            case true:
                isCheck = false;
                _skillWindow.transform.position = Vector3.Lerp(_targetPos, _windowPos, rate);
                break;
            default:
                break;
        }


        yield return null;
    }
}
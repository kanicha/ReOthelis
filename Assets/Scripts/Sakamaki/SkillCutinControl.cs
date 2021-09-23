using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCutinControl : MonoBehaviour
{
    [SerializeField, Header("スキルカットインオブジェクト")] private GameObject _SkillCutinObj = null;
    [SerializeField, Header("スキルカットイン画像")] private Sprite[] _skillcutinImage = new Sprite[4]; 
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化
        _animator = _SkillCutinObj.GetComponent<Animator>();
        _SkillCutinObj.GetComponent<Image>().sprite = _skillcutinImage[0];
    }
    

    /// <summary>
    /// スキルカットイン処理
    /// </summary>
    /// <param name="charanum">キャラクター番号 0.シースティ 1. ルイス ....</param>
    public void ShowSkillCutin(int charanum)
    {
        switch (charanum)
        {
            case 0:
                _SkillCutinObj.GetComponent<Image>().sprite = _skillcutinImage[0];
                break;
            case 1:
                _SkillCutinObj.GetComponent<Image>().sprite = _skillcutinImage[1];
                break;
            case 2:
                _SkillCutinObj.GetComponent<Image>().sprite = _skillcutinImage[2];
                break;
            case 3:
                _SkillCutinObj.GetComponent<Image>().sprite = _skillcutinImage[3];
                break;
            default:
                break;
        }
        
        _animator.SetTrigger("skillMotion");
    }
}
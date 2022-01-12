using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGageEffect : MonoBehaviour
{
    private PlayerBase _playerBase;
    [SerializeField, Header("スキルゲージエフェクト")] private GameObject[] _skillEffect = new GameObject[3];

    private void Update()
    {
        
    }

    /// <summary>
    /// スキルゲージエフェクト起動関数
    /// </summary>
    public void  StartSkillGageEffect()
    {
        if (_playerBase.SkillActiveChecker(PlayerBase._skillNumber.skill_1))
        {
            _skillEffect[0].SetActive(true);
        }
        else if (_playerBase.SkillActiveChecker(PlayerBase._skillNumber.skill_2))
        {
            _skillEffect[1].SetActive(true);
        }
        else if (_playerBase.SkillActiveChecker(PlayerBase._skillNumber.skill_3))
        {
            _skillEffect[2].SetActive(true);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillGageEffect : MonoBehaviour
{
    [SerializeField] private PlayerBase _playerBase;

    [SerializeField, Header("スキルゲージエフェクト")]
    private GameObject[] _skillEffect = new GameObject[3];

    [SerializeField, Header("スキルゲージ横のアイコン")]
    private SpriteRenderer[] _skillGageIcon = new SpriteRenderer[3];

    [SerializeField, Header("スキルウィンドウのアイコン")]
    private Image[] _skillWindowIcon = new Image[3];

    private void Start()
    {
        // 初期化
        for (int i = 0; i < _skillGageIcon.Length; i++)
        {
            _skillGageIcon[i].color = new Color32(150, 150, 150, 255);
            _skillWindowIcon[i].color = new Color32(150, 150, 150, 255);
        }
    }

    /// <summary>
    /// スキルゲージエフェクト起動関数
    /// </summary>
    public void StartSkillGageEffect()
    {
        if (_playerBase.SkillActiveChecker(PlayerBase._skillNumber.skill_3))
        {
            _skillGageIcon[2].color = new Color32(255, 255, 255, 255);
            _skillWindowIcon[2].color = new Color32(255, 255, 255, 255);
        }

        if (_playerBase.SkillActiveChecker(PlayerBase._skillNumber.skill_2))
        {
            _skillGageIcon[1].color = new Color32(255, 255, 255, 255);
            _skillWindowIcon[1].color = new Color32(255, 255, 255, 255);
        }

        if (_playerBase.SkillActiveChecker(PlayerBase._skillNumber.skill_1))
        {
            _skillGageIcon[0].color = new Color32(255, 255, 255, 255);
            _skillWindowIcon[0].color = new Color32(255, 255, 255, 255);
        }
    }

    /// <summary>
    /// スキルゲージエフェクトのチェック関数 光る条件を満たしてなかったら消す
    /// </summary>
    public void CheckSkillGageEffect()
    {
        if (!_playerBase.SkillActiveChecker(PlayerBase._skillNumber.skill_3))
        {
            _skillGageIcon[2].color = new Color32(150, 150, 150, 255);
            _skillWindowIcon[2].color = new Color32(150, 150, 150, 255);
        }

        if (!_playerBase.SkillActiveChecker(PlayerBase._skillNumber.skill_2))
        {
            _skillGageIcon[1].color = new Color32(150, 150, 150, 255);
            _skillWindowIcon[1].color = new Color32(150, 150, 150, 255);
        }

        if (!_playerBase.SkillActiveChecker(PlayerBase._skillNumber.skill_1))
        {
            _skillGageIcon[0].color = new Color32(150, 150, 150, 255);
            _skillWindowIcon[0].color = new Color32(150, 150, 150, 255);
        }
    }
}
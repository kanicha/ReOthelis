using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillGageEffect : MonoBehaviour
{
    [SerializeField] private PlayerBase _playerBase;

    [SerializeField, Header("スキルゲージエフェクト")]
    private ParticleSystem[] _skillEffect = new ParticleSystem[3];

    [SerializeField, Header("スキルゲージ横のアイコン")]
    private SpriteRenderer[] _skillGageIcon = new SpriteRenderer[3];

    [SerializeField, Header("スキルウィンドウのアイコン")]
    private Image[] _skillWindowIcon = new Image[3];
    
    private bool[] _isEffect = new bool[3]
    {
        false, false, false
    };
    
    private void Start()
    {
        // 初期化
        for (int i = 0; i < _skillGageIcon.Length; i++)
        {
            _skillEffect[i].Stop();
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
            if (!_isEffect[2])
            {
                _skillEffect[2].Play();
                _isEffect[2] = true;
            }
           
            _skillGageIcon[2].color = new Color32(255, 255, 255, 255);
            _skillWindowIcon[2].color = new Color32(255, 255, 255, 255);
        }

        if (_playerBase.SkillActiveChecker(PlayerBase._skillNumber.skill_2))
        {
            if (!_isEffect[1])
            {
                _skillEffect[1].Play();
                _isEffect[1] = true;
            }
            
            _skillGageIcon[1].color = new Color32(255, 255, 255, 255);
            _skillWindowIcon[1].color = new Color32(255, 255, 255, 255);
        }

        if (_playerBase.SkillActiveChecker(PlayerBase._skillNumber.skill_1))
        {
            if (!_isEffect[0])
            {
                _skillEffect[0].Play();
                _isEffect[0] = true;
            }
            
            _skillGageIcon[0].color = new Color32(255, 255, 255, 255);
            _skillWindowIcon[0].color = new Color32(255, 255, 255, 255);
        }

        if (GameDirector.Instance.gameState == GameDirector.GameState.falled)
        {
            for (int i = 0; i < _isEffect.Length; i++)
            {
                _isEffect[i] = false;
            }
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
    
    // ソースコードマジで汚いのでどうにかしたい
}
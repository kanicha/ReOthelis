using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWindowControl : MonoBehaviour
{
    [SerializeField, Header("スキルウィンドウオブジェクト")] private GameObject _skillWindowObj = null;
    
    [SerializeField, Header("スキルウィンドウの文字オブジェクト")] private GameObject _skillText = null;

    private Animator _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = _skillWindowObj.GetComponent<Animator>();
    }

    public void ShowSkillWindow()
    {
        _animator.SetTrigger("skillWindow");
        if (ModeSelect._selectCount == 3)
        {
            TutorialDirector.Instance.tutorialPhase = TutorialDirector.TutorialPhase.SkillActive;
        }
    }
}
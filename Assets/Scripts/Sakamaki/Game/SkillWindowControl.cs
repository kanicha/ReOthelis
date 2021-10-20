using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWindowControl : MonoBehaviour
{
    [SerializeField, Header("スキルウィンドウオブジェクト")] private GameObject _skillWindowObj = null;

    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = _skillWindowObj.GetComponent<Animator>();
    }

    public void ShowSkillWindow()
    {
        _animator.SetTrigger("skillWindow");
    }
    
}
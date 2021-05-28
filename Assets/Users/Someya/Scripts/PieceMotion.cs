using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PieceMotion : MonoBehaviour
{
    private Animator anim;
    private BlockControl blockControl;
    // Start is called before the first frame update
    void Start()
    {
        //変数animに、Animatorコンポーネントを設定する
        anim = gameObject.GetComponent<Animator>();
        blockControl = GetComponent<BlockControl>();
        Anmt();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Anmt()
    {
        //Bool型のパラメーターであるTurnをtrueにする
        anim.SetBool("Turn", true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerKomaRundom : MonoBehaviour
{
    // Jokerコマ
    [SerializeField] private GameObject Joker;
    // 白と黒のコマObject
    [SerializeField] private GameObject[] BWKoma;

    // 宣言
    private BlockController blockController;
    
    void Start()
    {
        // 実態はあるけど中身はないのでGetComponentをする
        blockController = GetComponent<BlockController>();
    }

    // Update is called once per frame
    void Update()
    {
        JokerKoma();
    }

    /// <summary>
    /// ジョーカーコマ生成関数
    /// </summary>
    public void JokerKoma()
    {
        // Jokerコマのフラグがtrueになったら
        if (blockController.komaLanding == true)
        {
            // 内部処理
            Debug.Log(blockController.komaLanding);
            
            // falseにする
            blockController.komaLanding = false;
        }
    }
}

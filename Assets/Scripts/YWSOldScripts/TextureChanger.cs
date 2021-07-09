using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureChanger : MonoBehaviour
{
    public Material[] _material;
    [SerializeField]
    GameObject black;
    [SerializeField]
    GameObject white;

    // Use this for initialization
    void Start()
    {
        Init(1,0);
    }

    /*
    選択されたキャラクターに合わせて駒のマテリアルを初期化する。
    0 牛黒
    1 鼠黒
    2 兎黒
    3 虎黒
    4 牛白
    5 鼠白
    6 兎白
    7 虎白
    */
    public void Init(int P1,int P2)
    {
        black.GetComponent<Renderer>().sharedMaterial = _material[P1];
        white.GetComponent<Renderer>().sharedMaterial = _material[P2+4];
    }
}
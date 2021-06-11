using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnKoma : MonoBehaviour
{
    // コマのオブジェクトアタッチ用 (配列)
    [SerializeField] private GameObject[] Koma;

    // コマ管理用List
    private List<GameObject> KomaList = new List<GameObject>();
    
    void Start()
    {
        // Komaの中身分をKomaListに追加
        KomaList.AddRange(Koma);
        
        KomaCreate();
    }

    /// <summary>
    /// コマ生成関数
    /// </summary>
    public void KomaCreate()
    {
        Instantiate(KomaList[Random.Range(0, KomaList.Count)], transform.position, Quaternion.identity);
    }
}
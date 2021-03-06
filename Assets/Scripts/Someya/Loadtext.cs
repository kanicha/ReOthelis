using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Loadtext : MonoBehaviour
{
    [SerializeField]
    private Text maintext;
    // CSVファイル
    private TextAsset csvFile;
    // CSVの中身を入れるリスト
    List<string[]> csvDatas = new List<string[]>();
    //一文字一文字の表示する速さ
    //[SerializeField]
    //float novelSpeed;
    // CSVの行数
    public int height;
    private void Start()
    {
        // Resouces下のCSV読み込み
        csvFile = Resources.Load("Datas/oseris_01") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        // reader.Peaekが-1になるまで
        while (reader.Peek() != -1)
        {
            // 一行ずつ読み込み
            string line = reader.ReadLine();
            // , 区切りでリストに追加
            csvDatas.Add(line.Split(','));
            // 行数加算
            height++;
        }
        // csvDatas[行][列]を指定して値を自由に取り出せる
        Debug.Log(csvDatas[9][0]);
    }
}

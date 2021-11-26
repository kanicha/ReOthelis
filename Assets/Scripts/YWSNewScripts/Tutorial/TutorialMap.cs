using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMap : MonoBehaviour
{
    public string[,] map = new string[11, 10] // z, x座標で指定
    {
        {"■", "□", "□", "□", "□", "□", "□", "□", "□", "■"},
        {"■", "□", "□", "□", "□", "□", "□", "□", "□", "■"},
        {"■", "□", "□", "□", "□", "□", "□", "□", "□", "■"},
        {"■", "□", "□", "□", "□", "□", "□", "□", "□", "■"},
        {"■", "□", "□", "□", "□", "□", "□", "□", "□", "■"},
        {"■", "□", "□", "□", "□", "□", "□", "□", "□", "■"},
        {"■", "□", "□", "□", "□", "□", "□", "□", "□", "■"},
        {"■", "□", "□", "□", "□", "□", "□", "□", "□", "■"},
        {"■", "□", "□", "□", "□", "□", "□", "〇", "●", "■"},
        {"■", "〇", "●", "〇", "●", "〇", "●", "〇", "●", "■"},
        {"■", "■", "■", "■", "■", "■", "■", "■", "■", "■"}
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
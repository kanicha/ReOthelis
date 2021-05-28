using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DS4Controller : MonoBehaviour
{
    /// <summary>
    /// 物理ボタン各種
    /// </summary>
    // 丸ボタン
    public string DS4o = "Fire_2";
    // バツボタン
    public string DS4x = "Fire_1";
    // 四角ボタン
    public string DS4sq = "Fire_0";
    // 三角ボタン
    public string DS4tri = "Fire_3";
    // L1ボタン
    public string DS4L1 = "Fire_L1";
    // R1ボタン
    public string DS4R1 = "Fire_R1";
    // Optionボタン
    public string DS4Option = "Fire_Option";

    /// <summary>
    /// 十字キー
    /// </summary>
    // 十字キー右.左
    public string DS4Horizontal = "Horizontal D-Pad";
    //public float fDS4Horizontal = Input.GetAxisRaw("Horizontal D-Pad");
    // 十字キー上.下
    public string DS4Vertical = "Vertical D-Pad";
    //public float fDS4Vertical = Input.GetAxisRaw("Vertical D-Pad");

    /// <summary>
    /// スティック
    /// </summary>
    // 左スティック右.左
    public string DS4LStickHorizontal = "Horizontal Stick-L";
    // 左スティック上.下
    public string DS4LStickVertical = "Vertical Stick-L";
    // 右スティック右.左
    public string DS4RStickHorizontal = "Horizontal Stick-R";
    // 右スティック上,下
    public string DS4RStickVertical = "Vertical Stick-R";
}

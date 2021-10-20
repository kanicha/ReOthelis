using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DS4ControllerP1
{
    /// <summary>
    /// 物理ボタン各種
    /// </summary>
    // 丸ボタン
    public static readonly string DS4_O = "Fire_2";
    // バツボタン
    public static readonly string DS4_X = "Fire_1";
    // 四角ボタン
    public static readonly string DS4_SQ = "Fire_0";
    // 三角ボタン
    public static readonly string DS4_TRI = "Fire_3";
    // L1ボタン
    public static readonly string DS4_L1 = "Fire_L1";
    // R1ボタン
    public static readonly string DS4_R1 = "Fire_R1";
    // Optionボタン
    public static readonly string DS4_OPTION = "Fire_Option";

    /// <summary>
    /// 十字キー
    /// </summary>
    // 十字キー右.左
    public static readonly string DS4_HORIZONTAL = "Horizontal D-Pad";
    //public static float FLOAT_DS4_HORIZONTAL = Input.GetAxisRaw("Horizontal D-Pad");
    // 十字キー上.下
    public static readonly string DS4_VERTICAL = "Vertical D-Pad";
    //public static float FLOAT_DS4_VERTICAL = Input.GetAxisRaw("Vertical D-Pad");

    /// <summary>
    /// スティック
    /// </summary>
    // 左スティック右.左
    public static readonly string DS4L_STICK_HORIZONTAL = "Horizontal Stick-L";
    // 左スティック上.下
    public static readonly string DS4L_STICK_VERTICAL = "Vertical Stick-L";
    // 右スティック右.左
    public static readonly string DS4R_STICK_HORIZONTAL = "Horizontal Stick-R";
    // 右スティック上,下
    public static readonly string DS4R_STICK_VERTICAL = "Vertical Stick-R";
}

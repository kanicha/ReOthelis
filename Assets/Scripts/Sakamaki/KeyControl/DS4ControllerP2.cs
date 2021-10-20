using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DS4ControllerP2
{
    /// <summary>
    /// 物理ボタン各種
    /// </summary>
    // 丸ボタン
    public static readonly string DS4_O = "Fire_2_2";
    // バツボタン
    public static readonly string DS4_X = "Fire_1_2";
    // 四角ボタン
    public static readonly string DS4_SQ = "Fire_0_2";
    // 三角ボタン
    public static readonly string DS4_TRI = "Fire_3_2";
    // L1ボタン
    public static readonly string DS4_L1 = "Fire_L1_2";
    // R1ボタン
    public static readonly string DS4_R1 = "Fire_R1_2";
    // Optionボタン
    public static readonly string DS4_OPTION = "Fire_Option_2";

    /// <summary>
    /// 十字キー
    /// </summary>
    // 十字キー右.左
    public static readonly string DS4_HORIZONTAL = "Horizontal D-Pad_2";
    //public float fDS4Horizontal = Input.GetAxisRaw("Horizontal D-Pad");
    // 十字キー上.下
    public static readonly string DS4_VERTICAL = "Vertical D-Pad_2";
    //public float fDS4Vertical = Input.GetAxisRaw("Vertical D-Pad");

    /// <summary>
    /// スティック
    /// </summary>
    // 左スティック右.左
    public static readonly string DS4L_STICK_HORIZONTAL = "Horizontal Stick-L_2";
    // 左スティック上.下
    public static readonly string DS4L_STICK_VERTICAL = "Vertical Stick-L_2";
    // 右スティック右.左
    public static readonly string DS4R_STICK_HORIZONTAL = "Horizontal Stick-R_2";
    // 右スティック上,下
    public static readonly string DS4R_STICK_VERTICAL = "Vertical Stick-R_2";
}

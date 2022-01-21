using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Base : PlayerBase
{
    private const string _PLAYER2_CIRCLE_NAME = "Fire_2_2";
    private const string _PLAYER2_CROSS_NAME = "Fire_1_2";
    private const string _PLAYER2_SQUARE_NAME = "Fire_0_2";
    private const string _PLAYER2_TRIANGLE_NAME = "Fire_3_2";
    private const string _PLAYER2_L1_NAME = "Fire_L1_2";
    private const string _PLAYER2_L2_NAME = "Fire_L2_2";
    private const string _PLAYER2_R1_NAME = "Fire_R1_2";
    private const string _PLAYER2_R2_NAME = "Fire_R2_2";
    private const string _PLAYER2_OPTION_NAME = "Fire_Option_2";
    private const string _PLAYER2_HORIZONTAL_NAME = "Horizontal D-Pad_2";
    private const string _PLAYER2_VERTICAL_NAME = "Vertical D-Pad_2";
    private const string _PLAYER2_LSTICK_HORIZONTAL_NAME = "Horizontal Stick-L_2";
    private const string _PLAYER2_LSTICK_VERTICAL_NAME = "Vertical Stick-L_2";
    private const string _PLAYER2_RSTICK_HORIZONTAL_NAME = "Horizontal Stick-R_2";
    private const string _PLAYER2_RSTICK_VERTICAL_NAME = "Vertical Stick-R_2";

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        
        base.DS4_circle_name = _PLAYER2_CIRCLE_NAME;
        base.DS4_cross_name = _PLAYER2_CROSS_NAME;
        base.DS4_square_name = _PLAYER2_SQUARE_NAME;
        base.DS4_triangle_name = _PLAYER2_TRIANGLE_NAME;
        base.DS4_L1_name = _PLAYER2_L1_NAME;
        base.DS4_L2_name = _PLAYER2_L2_NAME;
        base.DS4_R1_name = _PLAYER2_R1_NAME;
        base.DS4_R2_name = _PLAYER2_R2_NAME;
        base.DS4_option_name = _PLAYER2_OPTION_NAME;
        base.DS4_horizontal_name = _PLAYER2_HORIZONTAL_NAME;
        base.DS4_vertical_name = _PLAYER2_VERTICAL_NAME;
        base.DS4_Lstick_horizontal_name = _PLAYER2_LSTICK_HORIZONTAL_NAME;
        base.DS4_Lstick_vertical_name = _PLAYER2_LSTICK_VERTICAL_NAME;
        base.DS4_Rstick_horizontal_name = _PLAYER2_RSTICK_HORIZONTAL_NAME;
        base.DS4_Rstick_vertical_name = _PLAYER2_RSTICK_VERTICAL_NAME;
        //
        base.key_board_horizontal_name = "Horizontal_2";
        base.key_board_vertical_name = "Vertical_2";
    }
}

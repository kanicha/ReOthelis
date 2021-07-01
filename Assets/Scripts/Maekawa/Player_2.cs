using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_2 : PlayerBase
{
    private const string _PLAYER2_CIRCLE_NAME = "Fire_2_2";
    private const string _PLAYER2_CROSS_NAME = "Fire_1_2";
    private const string _PLAYER2_SQUARE_NAME = "Fire_0_2";
    private const string _PLAYER2_TRIANGLE_NAME = "Fire_3_2";
    private const string _PLAYER2_L1_NAME = "Fire_L1_2";
    private const string _PLAYER2_R1_NAME = "Fire_R1_2";
    private const string _PLAYER2_OPTION_NAME = "Fire_Option_2";
    private const string _PLAYER2_HORIZONTAL_NAME = "Horizontal D-Pad_2";
    private const string _PLAYER2_VERTICAL_NAME = "Vertical D-Pad_2";
    private const string _PLAYER2_LSTICK_HORIZONTAL_NAME = "Horizontal Stick-L_2";
    private const string _PLAYER2_LSTICK_VERTICAL_NAME = "Vertical Stick-L_2";
    private const string _PLAYER2_RSTICK_HORIZONTAL_NAME = "Horizontal Stick-R_2";
    private const string _PLAYER2_RSTICK_VERTICAL_NAME = "Vertical Stick-R_2";

    void Start()
    {
        base.DS4_circle_name = _PLAYER2_CIRCLE_NAME;
        base.DS4_cross_name = _PLAYER2_CROSS_NAME;
        base.DS4_square_name = _PLAYER2_SQUARE_NAME;
        base.DS4_triangle_name = _PLAYER2_TRIANGLE_NAME;
        base.DS4_L1_name = _PLAYER2_L1_NAME;
        base.DS4_R1_name = _PLAYER2_R1_NAME;
        base.DS4_option_name = _PLAYER2_OPTION_NAME;
        base.DS4_horizontal_name = _PLAYER2_HORIZONTAL_NAME;
        base.DS4_vertical_name = _PLAYER2_VERTICAL_NAME;
        base.DS4_Lstick_horizontal_name = _PLAYER2_LSTICK_HORIZONTAL_NAME;
        base.DS4_Lstick_vertical_name = _PLAYER2_LSTICK_VERTICAL_NAME;
        base.DS4_Rstick_horizontal_name = _PLAYER2_RSTICK_HORIZONTAL_NAME;
        base.DS4_Rstick_vertical_name = _PLAYER2_RSTICK_VERTICAL_NAME;
    }

    void Update()
    {
        base.KeyInput();
        base.SaveKeyValue();

        if (base.isMyTurn)
        {
            base.PieceMove();
            base.PieceRotate();

            if (base.map.CheckLanding(base.controllPiece1.transform.position))
            {
                base.map.FallPiece(controllPiece1);
                base.map.FallPiece(controllPiece2);
                GameDirector.isLanding = true;
            }
            else if (base.map.CheckLanding(base.controllPiece2.transform.position))
            {
                // ��]�����ڒn�������]�����痎�Ƃ�
                base.map.FallPiece(controllPiece2);
                base.map.FallPiece(controllPiece1);
                GameDirector.isLanding = true;
            }
        }
        else
        {

        }
    }
}
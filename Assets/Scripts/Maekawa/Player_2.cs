using UnityEngine;

public class Player_2 : PlayerBase
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

    public static int displayReverseScore = 0;
    public static int displayPreScore = 0;
    public static int displayScore = 0;
    public static int displayPieceAmount = 0;

    void Start()
    {
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

        // ???????????????????????????????????????????????????
        base.playerType = Piece.PieceType.white;
        base.myColor = Map.Instance.white;
        base.myColorfixity = Map.Instance.fixityWhite;
        base.enemyColor = Map.Instance.black;
        base.enemyColorfixity = Map.Instance.fixityBlack;
        SetSkills((int) CharaImageMoved2P.charaType2P);
    }

    void Update()
    {
        // ????????????????????????????????????????????????
        if (ServerManager._isConnect)
        {
            return;
        }
        
        if (base.reversedCount > MAX_REVERSE_COUNT)
            base.reversedCount = MAX_REVERSE_COUNT;

        base.gaugeController.DrawGauge(reversedCount);
        base.scoreText.text = string.Format("{0:00000}", base.reverseScore);

        displayReverseScore = base.reverseScore;
        displayPreScore = base.preScore;
        displayScore = base.reverseScore + base.preScore;
        displayPieceAmount = base.myPieceCount;

        base.SaveKeyValue();
        base.KeyInput();

        base.ShowSkillWindow(KeyCode.M);

        if (isMyTurn)
        {
            base.InputSkill();
            
            base._skillGageEffect.CheckSkillGageEffect();
            base._skillGageEffect.StartSkillGageEffect();
            
            base.charactorImage.color = new UnityEngine.Color(1, 1, 1);
            if (GameDirector.Instance.gameState == GameDirector.GameState.active)
            {
                base.PieceMove();
                base.PieceRotate();
            }
            else if (GameDirector.Instance.gameState == GameDirector.GameState.preActive)
            {
                base.PrePieceMove();
                base.PrePieceRotate();
            }
        }
        else
            base.charactorImage.color = new UnityEngine.Color(0.5f, 0.5f, 0.5f);
    }
}
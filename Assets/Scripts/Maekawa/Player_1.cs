using UnityEngine;

public class Player_1 : PlayerBase
{
    private const string _PLAYER1_CIRCLE_NAME = "Fire_2";
    private const string _PLAYER1_CROSS_NAME = "Fire_1";
    private const string _PLAYER1_SQUARE_NAME = "Fire_0";
    private const string _PLAYER1_TRIANGLE_NAME = "Fire_3";
    private const string _PLAYER1_L1_NAME = "Fire_L1";
    private const string _PLAYER1_L2_NAME = "Fire_L2";
    private const string _PLAYER1_R1_NAME = "Fire_R1";
    private const string _PLAYER1_R2_NAME = "Fire_R2";
    private const string _PLAYER1_OPTION_NAME = "Fire_Option";
    private const string _PLAYER1_HORIZONTAL_NAME = "Horizontal D-Pad";
    private const string _PLAYER1_VERTICAL_NAME = "Vertical D-Pad";
    private const string _PLAYER1_LSTICK_HORIZONTAL_NAME = "Horizontal Stick-L";
    private const string _PLAYER1_LSTICK_VERTICAL_NAME = "Vertical Stick-L";
    private const string _PLAYER1_RSTICK_HORIZONTAL_NAME = "Horizontal Stick-R";
    private const string _PLAYER1_RSTICK_VERTICAL_NAME = "Vertical Stick-R";

    public static int displayReverseScore = 0;
    public static int displayPreScore = 0;
    public static int displayScore = 0;
    public static int displayPieceAmount = 0;

    void Start()
    {
        base.DS4_circle_name = _PLAYER1_CIRCLE_NAME;
        base.DS4_cross_name = _PLAYER1_CROSS_NAME;
        base.DS4_square_name = _PLAYER1_SQUARE_NAME;
        base.DS4_triangle_name = _PLAYER1_TRIANGLE_NAME;
        base.DS4_L1_name = _PLAYER1_L1_NAME;
        base.DS4_L2_name = _PLAYER1_L2_NAME;
        base.DS4_R1_name = _PLAYER1_R1_NAME;
        base.DS4_R2_name = _PLAYER1_R2_NAME;
        base.DS4_option_name = _PLAYER1_OPTION_NAME;
        base.DS4_horizontal_name = _PLAYER1_HORIZONTAL_NAME;
        base.DS4_vertical_name = _PLAYER1_VERTICAL_NAME;
        base.DS4_Lstick_horizontal_name = _PLAYER1_LSTICK_HORIZONTAL_NAME;
        base.DS4_Lstick_vertical_name = _PLAYER1_LSTICK_VERTICAL_NAME;
        base.DS4_Rstick_horizontal_name = _PLAYER1_RSTICK_HORIZONTAL_NAME;
        base.DS4_Rstick_vertical_name = _PLAYER1_RSTICK_VERTICAL_NAME;
        //
        base.key_board_horizontal_name = "Horizontal";
        base.key_board_vertical_name = "Vertical";

        // ???????????????????????????????????????????????????
        base.playerType = Piece.PieceType.black;
        base.myColor = Map.Instance.black;
        base.myColorfixity = Map.Instance.fixityBlack;
        base.enemyColor = Map.Instance.white;
        base.enemyColorfixity = Map.Instance.fixityWhite;
        SetSkills((int) CharaImageMoved.charaType1P);
    }

    private void Update()
    {
        if (base.reversedCount > MAX_REVERSE_COUNT)
            base.reversedCount = MAX_REVERSE_COUNT;

        base.gaugeController.DrawGauge(reversedCount);
        base.scoreText.text = string.Format("{0:00000}", base.reverseScore);


        // ????????????????????????????????????
        displayReverseScore = base.reverseScore;
        // ???????????????
        displayPreScore = base.preScore;
        // ???????????????
        displayScore = base.reverseScore + base.preScore;
        displayPieceAmount = base.myPieceCount;

        base.SaveKeyValue();
        base.KeyInput();
        
        DebugGameEnd();
        base.ShowSkillWindow(KeyCode.N);

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
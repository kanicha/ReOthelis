public class Player_1 : PlayerBase
{
    private const string _PLAYER1_CIRCLE_NAME = "Fire_2";
    private const string _PLAYER1_CROSS_NAME = "Fire_1";
    private const string _PLAYER1_SQUARE_NAME = "Fire_0";
    private const string _PLAYER1_TRIANGLE_NAME = "Fire_3";
    private const string _PLAYER1_L1_NAME = "Fire_L1";
    private const string _PLAYER1_R1_NAME = "Fire_R1";
    private const string _PLAYER1_OPTION_NAME = "Fire_Option";
    private const string _PLAYER1_HORIZONTAL_NAME = "Horizontal D-Pad";
    private const string _PLAYER1_VERTICAL_NAME = "Vertical D-Pad";
    private const string _PLAYER1_LSTICK_HORIZONTAL_NAME = "Horizontal Stick-L";
    private const string _PLAYER1_LSTICK_VERTICAL_NAME = "Vertical Stick-L";
    private const string _PLAYER1_RSTICK_HORIZONTAL_NAME = "Horizontal Stick-R";
    private const string _PLAYER1_RSTICK_VERTICAL_NAME = "Vertical Stick-R";

    void Start()
    {
        base.DS4_circle_name = _PLAYER1_CIRCLE_NAME;
        base.DS4_cross_name = _PLAYER1_CROSS_NAME;
        base.DS4_square_name = _PLAYER1_SQUARE_NAME;
        base.DS4_triangle_name = _PLAYER1_TRIANGLE_NAME;
        base.DS4_L1_name = _PLAYER1_L1_NAME;
        base.DS4_R1_name = _PLAYER1_R1_NAME;
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


        // キャラクターに応じてスキルをセット
        base.myColor = Map.Instance.black;
        base.skill_1 = base.Lock;
        //base.skill_2 = base.Lock;
        //base.skill_3 =
    }

    private void Update()
    {
        if (base.reversedCount > MAX_REVERSE_COUNT)
            base.reversedCount = MAX_REVERSE_COUNT;
        base.reversedCountText.text = base.reversedCount.ToString();
        base.scoreText.text = string.Format("{0:00000}", base.score);
        base.myPieceCountText.text = "駒数" + base.myPieceCount.ToString();

        base.SaveKeyValue();
        base.KeyInput();

        if (isMyTurn)
        {
            base.SkillActivate();

            base.charactorImage.color = new UnityEngine.Color(1, 1, 1);
            if (GameDirector.Instance.gameState == GameDirector.GameState.active)
            {
                base.PieceMove();
                base.PieceRotate();
            }
            else if (GameDirector.Instance.gameState == GameDirector.GameState.preActive)
            {
                base.PrePieceMove();
                base.PieceRotate();
            }
        }
        else
            base.charactorImage.color = new UnityEngine.Color(0.5f, 0.5f, 0.5f);
    }
}
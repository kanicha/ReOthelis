using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBase : MonoBehaviour
{
    // キーネーム
    protected string DS4_circle_name = "";
    protected string DS4_cross_name = "";
    protected string DS4_square_name = "";
    protected string DS4_triangle_name = "";
    protected string DS4_L1_name = "";
    protected string DS4_R1_name = "";
    protected string DS4_option_name = "";
    protected string DS4_horizontal_name = "";
    protected string DS4_vertical_name = "";
    protected string DS4_Lstick_horizontal_name = "";
    protected string DS4_Lstick_vertical_name = "";
    protected string DS4_Rstick_horizontal_name = "";
    protected string DS4_Rstick_vertical_name = "";
    // キーボード操作用キーネーム
    protected string key_board_horizontal_name = "";
    protected string key_board_vertical_name = "";
    // キーバリュー
    protected bool _DS4_circle_value = false;
    protected bool _DS4_cross_value = false;
    protected bool _DS4_square_value = false;
    protected bool _DS4_triangle_value = false;
    protected bool _DS4_L1_value = false;
    protected bool _DS4_R1_value = false;
    protected bool _DS4_option_value = false;
    protected float _DS4_horizontal_value = 0.0f;
    protected float _DS4_vertical_value = 0.0f;
    protected float _DS4_Lstick_horizontal_value = 0.0f;
    protected float _DS4_Lstick_vertical_value = 0.0f;
    protected float _DS4_Rstick_horizontal_value = 0.0f;
    protected float _DS4_Rstick_vertical_value = 0.0f;
    // 前フレームのキーバリュー
    protected float last_horizontal_value = 0.0f;
    protected float last_vertical_value = 0.0f;
    protected float lastLstick_horizontal_value = 0.0f;
    protected float last_Lstick_vertical_value = 0.0f;
    protected float last_Rstick_horizontal_value = 0.0f;
    protected float last_Rstick_vertical_value = 0.0f;
    // キーボード用
    //private float _keyBoardHorizontal = 0.0f;
    //private float _keyBoardVertical = 0.0f;
    private bool _keyBoardLeft = false;
    private bool _keyBoardRight = false;

    //
    [SerializeField, Header("1マス落下する時間")]
    private float _fallTime = 0.0f;
    [SerializeField]
    protected Text scoreText = null;
    [SerializeField]
    protected Text myPieceCountText = null;
    [SerializeField]
    protected Image charactorImage = null;
    [SerializeField]
    protected GaugeController gaugeController = null;
    private float _timeCount = 0.0f;
    public bool isMyTurn = false;
    public bool isPreurn = false;
    public int score = 0;
    public int reversedCount = 0;
    protected const int MAX_REVERSE_COUNT = 20;
    public int myPieceCount = 0;
    public GameObject controllPiece1 = null;
    public GameObject controllPiece2 = null;
    public int rotationNum = 0;
    //
    private const int _SKILL_1_COST = 3;
    private const int _SKILL_2_COST = 5;
    private const int _SKILL_3_COST = 10;
    protected Piece.PieceType playerType = Piece.PieceType.none;
    protected string myColor = "";
    protected string enemyColor = "";

    protected delegate void Skill_1(int cost);
    protected delegate void Skill_2(int cost);
    protected delegate void Skill_3(int cost);
    protected Skill_1 skill_1;
    protected Skill_2 skill_2;
    protected Skill_3 skill_3;

    protected readonly Vector3[] rotationPos = new Vector3[]
    {
        new Vector3(0,  0, 1),
        new Vector3(-1, 0, 0),
        new Vector3(0,  0, -1),
        new Vector3(1,  0, 0)
    };

    protected void KeyInput()
    {
        _DS4_circle_value = Input.GetButtonDown(DS4_circle_name);
        _DS4_cross_value = Input.GetButtonDown(DS4_cross_name);
        _DS4_square_value = Input.GetButtonDown(DS4_square_name);
        _DS4_triangle_value = Input.GetButtonDown(DS4_triangle_name);
        _DS4_L1_value = Input.GetButtonDown(DS4_L1_name);
        _DS4_R1_value = Input.GetButtonDown(DS4_R1_name);
        _DS4_option_value = Input.GetButtonDown(DS4_option_name);
        _DS4_horizontal_value = Input.GetAxis(DS4_horizontal_name);
        _DS4_vertical_value = Input.GetAxis(DS4_vertical_name);
        _DS4_Lstick_horizontal_value = Input.GetAxis(DS4_Lstick_horizontal_name);
        _DS4_Lstick_vertical_value = Input.GetAxis(DS4_Lstick_vertical_name);
        _DS4_Rstick_horizontal_value = Input.GetAxis(DS4_Rstick_horizontal_name);
        _DS4_Rstick_vertical_value = Input.GetAxis(DS4_Rstick_vertical_name);

        if (0 != Input.GetAxis(key_board_horizontal_name))
            _DS4_horizontal_value = Input.GetAxis(key_board_horizontal_name);
        if (0 != Input.GetAxis(key_board_vertical_name))
            _DS4_vertical_value = Input.GetAxis(key_board_vertical_name);
        _keyBoardLeft = Input.GetKeyDown(KeyCode.Q);
        _keyBoardRight = Input.GetKeyDown(KeyCode.E);
    }

    /// <summary>
    /// 前フレームの入力を保存
    /// </summary>
    protected void SaveKeyValue()
    {
        last_horizontal_value = _DS4_horizontal_value;
        last_vertical_value = _DS4_vertical_value;
        lastLstick_horizontal_value = _DS4_Lstick_horizontal_value;
        last_Lstick_vertical_value = _DS4_Lstick_vertical_value;
        last_Rstick_horizontal_value = _DS4_Rstick_horizontal_value;
        last_Rstick_vertical_value = _DS4_Rstick_vertical_value;
    }

    protected void InputSkill()
    {
        // スキル1...× スキル2...△ スキル3...□
        if(Input.GetKeyDown(KeyCode.Z) || _DS4_cross_value)
        {
            skill_1(_SKILL_1_COST);
        }
        else if(Input.GetKeyDown(KeyCode.X) || _DS4_triangle_value)
        {
            skill_2(_SKILL_2_COST);
        }
        //else if(Input.GetKeyDown(KeyCode.C) || _DS4_square_value)
        //{
        //    skill_3(_SKILL_3_COST);
        //}
    }

    protected void PieceMove()
    {
        Vector3 move = Vector3.zero;
        bool isDown = false;

        _timeCount += Time.deltaTime;

        // 移動
        if ((_DS4_horizontal_value < 0 && last_horizontal_value == 0) || (_DS4_Lstick_horizontal_value < 0 && lastLstick_horizontal_value == 0))
            move.x = -1;
        else if ((_DS4_horizontal_value > 0 && last_horizontal_value == 0) || (_DS4_Lstick_horizontal_value > 0 && lastLstick_horizontal_value == 0))
            move.x = 1;
        else if ((_DS4_vertical_value < 0 && last_vertical_value == 0) || (_DS4_Lstick_vertical_value < 0 && last_Lstick_vertical_value == 0))
        {
            isDown = true;
            move.z = -1;
        }
        else if (_timeCount >= _fallTime)// 時間落下
        {
            _timeCount = 0;
            move.z = -1;
        }

        // 移動後の座標を計算
        Vector3 movedPos = controllPiece1.transform.position + move;
        Vector3 rotMovedPos = movedPos + rotationPos[rotationNum];

        // 移動後の座標に障害物がなければ
        if (Map.Instance.CheckWall(movedPos) && Map.Instance.CheckWall(rotMovedPos))
        {
            controllPiece1.transform.position = movedPos;
            controllPiece2.transform.position = rotMovedPos;
        }
        else if (isDown)
            GameDirector.Instance.gameState = GameDirector.GameState.confirmed;// 下入力をし、障害物があるなら確定
    }

    protected void PieceRotate()
    {
        int lastNum = rotationNum;

        if (_DS4_L1_value || _keyBoardLeft)
            rotationNum++;// 左回転
        else if (_DS4_R1_value || _keyBoardRight)
            rotationNum += 3;// 右回転(=左に3回転)

        rotationNum %= 4;
        Vector3 rotatedPos = controllPiece1.transform.position + rotationPos[rotationNum];
        Vector3 rotatedUnderPos = rotatedPos + Vector3.back;


        if (Map.Instance.CheckWall(rotatedPos))
        {
            if ((int)rotatedPos.z == -1 && !Map.Instance.CheckWall(rotatedUnderPos))
                rotationNum = lastNum;  
            else
            {
                if (rotationNum != lastNum)
                    SoundManager.Instance.PlaySE(2);
                controllPiece2.transform.position = rotatedPos;
            }
        }
        else
            rotationNum = lastNum;
    }

    protected void PrePieceMove()
    {
        Vector3 move = Vector3.zero;

        // 左右移動
        if ((_DS4_horizontal_value < 0 && last_horizontal_value == 0) || (_DS4_Lstick_horizontal_value < 0 && lastLstick_horizontal_value == 0))
            move.x = -1;
        else if ((_DS4_horizontal_value > 0 && last_horizontal_value == 0) || (_DS4_Lstick_horizontal_value > 0 && lastLstick_horizontal_value == 0))
            move.x = 1;

        // 左右に入力したなら移動
        if (move != Vector3.zero)
        {
            Vector3 movedPos = controllPiece1.transform.position;
            while (true)
            {
                movedPos += move;
                Vector3 movedUnderPos = movedPos + Vector3.back;
                Vector3 rotMovedPos = movedUnderPos + rotationPos[rotationNum];

                // 壁まで行ったらスルー
                if ((int)movedPos.x < 1 || (int)movedPos.x > 8)
                    break;

                // 移動後の座標の1つ下に障害物がなければ
                if (Map.Instance.CheckWall(movedUnderPos) && Map.Instance.CheckWall(rotMovedPos))
                {
                    controllPiece1.transform.position = movedPos;
                    controllPiece2.transform.position = movedPos + rotationPos[rotationNum];
                    break;
                }
            }
        }

        // ↓入力したら本操作開始
        if ((_DS4_vertical_value < 0 && last_vertical_value == 0) || (_DS4_Lstick_vertical_value < 0 && last_Lstick_vertical_value == 0))
        {
            GameDirector.Instance.intervalTime = 0;
            GameDirector.Instance.nextStateCue = GameDirector.GameState.active;
            GameDirector.Instance.gameState = GameDirector.GameState.interval;
            controllPiece1.transform.position += Vector3.back;
            controllPiece2.transform.position += Vector3.back;
        }
    }


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void SetSkills(int charaType)
    {
        // 同じ意味のenumが1Pと2Pで2つあるのでenum→int→enumにキャスト 
        CharaImageMoved.CharaType1P type = (CharaImageMoved.CharaType1P)charaType;

        switch (type)
        {
            case CharaImageMoved.CharaType1P.Cow:
                skill_1 = Cancellation;
                skill_2 = MyPieceLock;
                break;
            case CharaImageMoved.CharaType1P.Mouse:
                skill_1 = RandomLock;
                skill_2 = Cancellation;
                break;
            case CharaImageMoved.CharaType1P.Rabbit:
                skill_1 = TakeAway;
                skill_2 = RandomLock;
                break;
            case CharaImageMoved.CharaType1P.Tiger:
                skill_1 = MyPieceLock;
                skill_2 = TakeAway;
                break;
            default:
                break;
        }
    }

    private bool CheckColor(string type)
    { 
        bool isThere = false;
        // 下一行を除いたコマが置かれる可能性のあるマスを探索
        for (int i = 2; i < 9; i++)
            for (int j = 1; j < 9; j++)
            {
                if (Map.Instance.map[i, j] == type)
                    isThere = true;
            }

        return isThere;
    }

    private bool ActivateCheck(GameDirector.GameState targetState, int cost)
    {
        if (GameDirector.Instance.gameState == targetState && reversedCount >= cost)
            return true;
        else
            return false;
    }

    //  強引
    public void TakeAway(int cost)
    {
        if (!ActivateCheck(GameDirector.GameState.preActive, cost))
            return;

        // 最下段を除くマップに相手の色があるなら(固定コマは対象外)
        if (CheckColor(enemyColor))
        {
            Debug.Log("強引");
            reversedCount -= cost;
            while (true)
            {
                // 適当にランダムな座標をとり、それが自分の色なら変換
                int z = Random.Range(2, 9);
                int x = Random.Range(1, 9);
                if (Map.Instance.map[z, x] == enemyColor)
                {
                    Map.Instance.map[z, x] = myColor;
                    Map.Instance.pieceMap[z, x].GetComponent<Piece>().SkillReverse();
                    // 検索、リバース処理を行う
                    Map.Instance.TagClear();
                    Map.Instance.isSkillActivate = true;
                    GameDirector.Instance.gameState = GameDirector.GameState.idle;
                    StartCoroutine(Map.Instance.CheckReverse(Map.Instance.pieceMap[z, x]));
                    break;
                }
            }
        }
        else
            Debug.Log("相手の色のコマがありません");
    }

    // 固定
    public void RandomLock(int cost)
    {
        if (!ActivateCheck(GameDirector.GameState.preActive, cost) && !ActivateCheck(GameDirector.GameState.active, cost))
            return;

        // 最下段を除くマップに自分の色があるなら(固定コマは対象外)
        if (CheckColor(myColor))
        {
            Debug.Log("固定");
            reversedCount -= cost;

            string type;
            if (myColor == Map.Instance.black)
                type = Map.Instance.fixityBlack;
            else
                type = Map.Instance.fixityWhite;

            while (true)
            {
                // 適当にランダムな座標をとり、それが自分の色なら固定コマに変換
                int z = Random.Range(2, 9);
                int x = Random.Range(1, 9);
                if (Map.Instance.map[z, x] == myColor)
                {
                    Map.Instance.map[z, x] = type;
                    Map.Instance.pieceMap[z, x].GetComponent<Piece>().ChangeIsFixity();
                    break;
                }
            }
        }
        else
            Debug.Log("自分の色のコマがありません");
    }

    // 残影
    public void MyPieceLock(int cost)
    {
        if (!ActivateCheck(GameDirector.GameState.preActive, cost) && !ActivateCheck(GameDirector.GameState.active, cost))
            return;

        Piece piece1 = controllPiece1.GetComponent<Piece>();
        Piece piece2 = controllPiece2.GetComponent<Piece>();

        // 自分の色があれば処理
        if (piece1.pieceType == playerType || piece2.pieceType == playerType)
        {
            Debug.Log("残影");
            reversedCount -= cost;
            // 自分の色を固定化(2つとも自分の色なら両方)
            if (piece1.pieceType == playerType)
                piece1.ChangeIsFixity();
            if (piece2.pieceType == playerType)
                piece2.ChangeIsFixity();
        }
        else
            Debug.Log("自分の色のコマを操作していません");
    }

    // 打消し
    public void Cancellation(int cost)
    {
        // 自分の色のコマを操作していなくても発動できる(意味はない)ので要相談

        if (!ActivateCheck(GameDirector.GameState.preActive, cost))
            return;

        // 相手色の固定コマを探す
        string targetColor;
        if (myColor == Map.Instance.black)
            targetColor = Map.Instance.fixityWhite;
        else
            targetColor = Map.Instance.fixityBlack;

        if (CheckColor(targetColor))
        {
            Debug.Log("打消し");
            reversedCount -= cost;
            Map.Instance.ignoreFixityPiece = targetColor;// 相手の固定コマをひっくり返せるようになる
        }
        else
            Debug.Log("相手色の固定コマがありません");
    }
}
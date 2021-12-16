using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

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
    [SerializeField, Header("1マス落下する時間")] private float _fallTime = 0.0f;
    [SerializeField] protected Text scoreText = null;
    [SerializeField] protected Text myPieceCountText = null;
    [SerializeField] protected Image charactorImage = null;
    [SerializeField] protected GaugeController gaugeController = null;
    [SerializeField] protected SkillCutinControl skillCutinControl = null;
    [SerializeField] protected SkillWindowControl skillWindowControl = null;
    private float _moveTimeCount = 0.0f;
    public bool isMyTurn = false;
    public int reverseScore = 0;
    public int preScore = 0;
    public int reversedCount = 0;
    protected const int MAX_REVERSE_COUNT = 20;
    public int myPieceCount = 0;
    public GameObject controllPiece1 = null;
    public GameObject controllPiece2 = null;

    public int rotationNum = 0;

    //
    private const int _SKILL_1_COST = 3;
    private const int _SKILL_2_COST = 5;
    private const int _SKILL_3_COST = 15;
    protected Piece.PieceType playerType = Piece.PieceType.none;
    private bool _isSkillBlack;
    private bool _isSkillWhite;
    protected string myColor = "";
    protected string myColorfixity = "";
    protected string enemyColor = "";
    protected string enemyColorfixity = "";

    protected delegate void Skill_1(int cost);

    protected delegate void Skill_2(int cost);

    protected delegate void Skill_3(int cost);

    protected Skill_1 skill_1;
    protected Skill_2 skill_2;
    protected Skill_3 skill_3;

    protected readonly Vector3[] rotationPos = new Vector3[]
    {
        // 0, 1, 2, 3
        new Vector3(0, 0, 1),
        new Vector3(-1, 0, 0),
        new Vector3(0, 0, -1),
        new Vector3(1, 0, 0)
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
        if (Input.GetKeyDown(KeyCode.Z) || _DS4_cross_value)
        {
            skill_1(_SKILL_1_COST);
        }
        else if (Input.GetKeyDown(KeyCode.X) || _DS4_triangle_value)
        {
            skill_2(_SKILL_2_COST);
        }
        else if (Input.GetKeyDown(KeyCode.C) || _DS4_square_value)
        {
            skill_3(_SKILL_3_COST);
        }
    }

    /// <summary>
    /// state Active時のコマ移動関数
    /// </summary>
    protected void PieceMove()
    {
        Vector3 move = Vector3.zero;
        bool isDown = false;

        _moveTimeCount += Time.deltaTime;

        // 移動
        if ((_DS4_horizontal_value < 0 && last_horizontal_value == 0) ||
            (_DS4_Lstick_horizontal_value < 0 && lastLstick_horizontal_value == 0))
            move.x = -1;
        else if ((_DS4_horizontal_value > 0 && last_horizontal_value == 0) ||
                 (_DS4_Lstick_horizontal_value > 0 && lastLstick_horizontal_value == 0))
            move.x = 1;
        else if ((_DS4_vertical_value < 0 && last_vertical_value == 0) ||
                 (_DS4_Lstick_vertical_value < 0 && last_Lstick_vertical_value == 0))
        {
            // カウントを初期化
            _moveTimeCount = 0f;
            isDown = true;
            move.z = -1;
        }
        else if (_moveTimeCount >= _fallTime) // 時間落下
        {
            _moveTimeCount = 0f;
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
        {
            GameDirector.Instance.gameState = GameDirector.GameState.confirmed;
        }
    }

    /// <summary>
    /// コマ回転関数
    /// </summary>
    protected void PieceRotate()
    {
        int lastNum = rotationNum;

        if (_DS4_L1_value || _keyBoardLeft)
        {
            rotationNum++; // 左回転
        }
        else if (_DS4_R1_value || _keyBoardRight)
        {
            rotationNum += 3; // 右回転(=左に3回転)
        }

        // 初期値0 左から 1,2,3
        rotationNum %= 4;

        // 軸のコマ + 回転後の座標 変数
        Vector3 rotatedPos = controllPiece1.transform.position + rotationPos[rotationNum];
        // 回転後の座標の一つ下の座標
        Vector3 rotatedUnderPos = rotatedPos + Vector3.back;
        // 回転後の座標の一つ右の座標
        Vector3 rotatedRightPos = controllPiece1.transform.position + rotationPos[3];
        // 回転後の座標の一つ左の座標
        Vector3 rotatedLeftPos = controllPiece1.transform.position + rotationPos[1];

        // 壁にあたってない時
        if (Map.Instance.CheckWall(rotatedPos))
        {
            // 回転Posが-1かつ下のコマが壁にあたっている時
            if ((int)rotatedPos.z == -1 && !Map.Instance.CheckWall(rotatedUnderPos))
                rotationNum = lastNum;
            else
            {
                // 回転番号と前の値が違う時 
                if (rotationNum != lastNum)
                    SoundManager.Instance.PlaySE(2);
                controllPiece2.transform.position = rotatedPos;
            }
        }
        // 壁にあたってる時
        else
        {
            /*Debug.Log("WallHit");*/
            rotationNum = lastNum;

            // 壁にあたってる時 かつ 右の壁と左の壁に挟まれている時
            if (!Map.Instance.CheckWall(rotatedLeftPos) && !Map.Instance.CheckWall(rotatedRightPos))
                // 回転入力でクイックローテート
                QuickRotate();
            else
                // 壁にあたってる時 かつ 片方の壁に当たっている時 回転ボタン押しで回転
                AnotherTurn(rotatedLeftPos, rotatedRightPos);
        }
    }

    /// <summary>
    /// 2回素早く回転を押したときにコマの色を変える関数
    /// </summary>
    private void QuickRotate()
    {
        // 処理面
        // コマの色情報をもってくる
        Piece piece1 = controllPiece1.GetComponent<Piece>();
        Piece piece2 = controllPiece2.GetComponent<Piece>();

        // ピース1が黒 and ピース2が白の時 or ピース1が白 and ピース1が黒の時
        if (piece1.pieceType == Piece.PieceType.black && piece2.pieceType == Piece.PieceType.white ||
            piece1.pieceType == Piece.PieceType.white && piece2.pieceType == Piece.PieceType.black)
        {
            EffectController.Instance._isTurn = true;
            SoundManager.Instance.PlaySE(2);

            // ピースの情報をいれかえる
            piece1.SkillReverse(false);
            piece2.SkillReverse(false);
        }
    }

    /// <summary>
    /// 壁にあたっている時の回転処理関数
    /// </summary>
    private void AnotherTurn(Vector3 rotatedLeftPos, Vector3 rotatedRightPos)
    {
        Vector3 move = Vector3.zero;

        // 回転番号が0 or 2(コマが縦の状態) かつ 左側に壁がある時
        if ((rotationNum == 0 || rotationNum == 2) && !Map.Instance.CheckWall(rotatedLeftPos))
        {
            Debug.LogWarning("LeftWall");
            rotationNum = 1;
            move.x = 1;
        }
        // 回転番号が0 or 2(コマが縦の状態) かつ 右側に壁がある時
        else if ((rotationNum == 0 || rotationNum == 2) && !Map.Instance.CheckWall(rotatedRightPos))
        {
            Debug.LogWarning("RightWall");
            rotationNum = 3;
            move.x = -1;
        }
        else
            return;

        // 移動させたあとの座標
        Vector3 movedPos = controllPiece1.transform.position + move;
        Vector3 rotMovedPos = movedPos + rotationPos[rotationNum];

        // 移動後座標に壁がなければ代入
        if (Map.Instance.CheckWall(movedPos) && Map.Instance.CheckWall(rotMovedPos))
        {
            SoundManager.Instance.PlaySE(2);

            controllPiece1.transform.position = movedPos;
            controllPiece2.transform.position = rotMovedPos;
        }
    }

    /// <summary>
    /// 待機中の移動関数
    /// </summary>
    protected void PrePieceMove()
    {
        Vector3 move = Vector3.zero;

        // 左右移動
        if ((_DS4_horizontal_value < 0 && last_horizontal_value == 0) ||
            (_DS4_Lstick_horizontal_value < 0 && lastLstick_horizontal_value == 0))
            move.x = -1;
        else if ((_DS4_horizontal_value > 0 && last_horizontal_value == 0) ||
                 (_DS4_Lstick_horizontal_value > 0 && lastLstick_horizontal_value == 0))
            move.x = 1;
        else if ((_DS4_vertical_value < 0 && last_vertical_value == 0) ||
                 (_DS4_Lstick_vertical_value < 0 && last_Lstick_vertical_value == 0))
        {
            move.z = -1;

            // した入力時ステート変更
            GameDirector.Instance.intervalTime = 0;
            GameDirector.Instance.nextStateCue = GameDirector.GameState.active;
            GameDirector.Instance.gameState = GameDirector.GameState.interval;
        }

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
    }

    /// <summary>
    /// スキルウィンドウ関数呼び出し
    /// </summary>
    /// <param name="inputKey"></param>
    protected void ShowSkillWindow(KeyCode inputKey)
    {
        if (Input.GetKeyDown(inputKey) || _DS4_option_value)
            skillWindowControl.ShowSkillWindow();
    }

    /// <summary>
    /// ゲーム終了デバッグ用関数 VB
    /// </summary>
    protected void DebugGameEnd()
    {
        if (Input.GetKeyDown(KeyCode.V) && Input.GetKey(KeyCode.B))
        {
            Debug.Log("GameEnd");
            GameDirector.Instance.gameState = GameDirector.GameState.end;
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
                skill_3 = RobberyMoment;
                break;
            case CharaImageMoved.CharaType1P.Mouse:
                skill_1 = RandomLock;
                skill_2 = Cancellation;
                skill_3 = OneRowSet;
                break;
            case CharaImageMoved.CharaType1P.Rabbit:
                skill_1 = TakeAway;
                skill_2 = RandomLock;
                skill_3 = PriorityGet;
                break;
            case CharaImageMoved.CharaType1P.Tiger:
                skill_1 = MyPieceLock;
                skill_2 = TakeAway;
                skill_3 = ForceConvertion;
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

    /// <summary>
    /// そのスキルをすでに使用しているかどうかのチェック関数
    /// </summary>
    /// <returns></returns>
    private bool SpacialSkillCheck()
    {
        if (_isSkillBlack == true || _isSkillWhite == true)
            return true;
        else
            return false;
    }

    private bool NormalSkillCheck()
    {
        if (GameDirector.Instance._isSkillBlack || GameDirector.Instance._isSkillWhite)
            return true;
        else
            return false;
    }

    /// <summary>
    /// プレイヤーが1Pか2P判別 黒 = 1P
    /// </summary>
    /// <returns>true = black</returns>
    private bool PlayerCheck()
    {
        if (myColor == Map.Instance.black)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 必殺技スキルでスコアを追加する処理関数(スキル使用フラグも処理)
    /// </summary>
    /// <param name="multiplyNum">追加するスコアの倍数</param>
    private void AddSkillScore(int multiplyNum, int colorCount)
    {
        int addAns = 0;

        addAns = colorCount * multiplyNum;

        if (PlayerCheck())
        {
            GameDirector.Instance.AddScore(true, addAns);
            _isSkillBlack = true;
        }
        else if (!PlayerCheck())
        {
            GameDirector.Instance.AddScore(false, addAns);
            _isSkillWhite = true;
        }
    }

    /// <summary>
    /// 通常スキルを使用したかどうか管理関数
    /// </summary>
    private void NormalSkillFlag()
    {
        if (PlayerCheck())
        {
            GameDirector.Instance._isSkillBlack = true;
        }
        else if (!PlayerCheck())
        {
            GameDirector.Instance._isSkillWhite = true;
        }
    }


    //  強引
    public void TakeAway(int cost)
    {
        // スキルが発動できない時
        if (!ActivateCheck(GameDirector.GameState.preActive, cost) ||
            Map.Instance.isSkillCheck ||
            NormalSkillCheck())
        {
            return;
        }

        // 最下段を除くマップに相手の色があるなら(固定コマは対象外)
        if (CheckColor(enemyColor))
        {
            Debug.Log("強引");
            SoundManager.Instance.PlaySE(5);
            reversedCount -= cost;
            while (true)
            {
                // 適当にランダムな座標をとり、それが自分の色なら変換
                int z = Random.Range(2, 9);
                int x = Random.Range(1, 9);
                if (Map.Instance.map[z, x] == enemyColor)
                {
                    Map.Instance.map[z, x] = myColor;
                    Map.Instance.pieceMap[z, x].GetComponent<Piece>().SkillReverse(true);
                    // 検索、リバース処理を行う
                    Map.Instance.TagClear();
                    Map.Instance.isSkillActivate = true;
                    GameDirector.Instance.gameState = GameDirector.GameState.idle;
                    StartCoroutine(Map.Instance.CheckReverse(Map.Instance.pieceMap[z, x], true));

                    NormalSkillFlag();
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
        if (!ActivateCheck(GameDirector.GameState.preActive, cost) &&
            !ActivateCheck(GameDirector.GameState.active, cost) ||
            NormalSkillCheck())
        {
            return;
        }


        // 最下段を除くマップに自分の色があるなら(固定コマは対象外)
        if (CheckColor(myColor))
        {

            Debug.Log("固定");
            SoundManager.Instance.PlaySE(5);
            reversedCount -= cost;

            while (true)
            {
                // 適当にランダムな座標をとり、それが自分の色なら固定コマに変換
                int z = Random.Range(2, 9);
                int x = Random.Range(1, 9);
                if (Map.Instance.map[z, x] == myColor)
                {
                    Map.Instance.map[z, x] = myColorfixity;
                    Map.Instance.pieceMap[z, x].GetComponent<Piece>().ChangeIsFixity();

                    NormalSkillFlag();
                    break;
                }
            }
        }
        else
            Debug.Log("自分の色のコマがフィールド上にありません");
    }

    // 残影
    public void MyPieceLock(int cost)
    {
        if (!ActivateCheck(GameDirector.GameState.preActive, cost) &&
            !ActivateCheck(GameDirector.GameState.active, cost) ||
            NormalSkillCheck())
        {
            return;
        }

        Piece piece1 = controllPiece1.GetComponent<Piece>();
        Piece piece2 = controllPiece2.GetComponent<Piece>();

        // 自分の色があれば処理
        if (piece1.pieceType == playerType || piece2.pieceType == playerType)
        {
            Debug.Log("残影");
            SoundManager.Instance.PlaySE(5);
            reversedCount -= cost;

            // 自分の色を固定化(2つとも自分の色なら両方)
            if (piece1.pieceType == playerType)
                piece1.ChangeIsFixity();
            if (piece2.pieceType == playerType)
                piece2.ChangeIsFixity();

            NormalSkillFlag();
        }
        else
            Debug.Log("自分の色のコマを操作していません");
    }

    // 打消し
    public void Cancellation(int cost)
    {
        // 自分の色のコマを操作していなくても発動できる(意味はない)ので要相談
        if (!ActivateCheck(GameDirector.GameState.preActive, cost) ||
            NormalSkillCheck())
        {
            return;
        }

        // フィールドに相手の色の固定こまがあった時発動
        if (CheckColor(enemyColorfixity))
        {

            Debug.Log("打ち消し");
            SoundManager.Instance.PlaySE(5);
            reversedCount -= cost;

            bool isCheck = false;

            // 探索
            for (int z = 2; z < 9; z++)
            {
                for (int x = 1; x < 9; x++)
                {
                    // 相手の固定こまがあったら
                    if (Map.Instance.map[z, x] == enemyColorfixity)
                    {
                        if (!isCheck)
                        {
                            isCheck = true;
                            // 色を戻す
                            Map.Instance.map[z, x] = enemyColor;
                            Map.Instance.pieceMap[z, x].GetComponent<Piece>().ChangeIsFixity();

                            NormalSkillFlag();
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("相手の固定駒がフィールド上にありません");
        }
    }

    /*
       キャラクター限定スキル定義
     */

    // 強制変換
    // 配置した駒と周りの駒をすべて自分の色に置き換える
    public void ForceConvertion(int cost)
    {
        // ゲームステートがpreActive(自動落下前) と active(操作中)の時コストがある時 発動可能
        if ((!ActivateCheck(GameDirector.GameState.preActive, cost) &&
             !ActivateCheck(GameDirector.GameState.active, cost)) ||
            SpacialSkillCheck())
        {
            return;
        }

        Debug.Log("強制変換");
        skillCutinControl.ShowSkillCutin(3);
        reversedCount -= cost;

        StartCoroutine(ForceConvertionCoroutine());
    }

    /// <summary>
    /// 強制変換のコルーチン処理関数
    /// </summary>
    /// <returns></returns>
    IEnumerator ForceConvertionCoroutine()
    {
        while (!GameDirector.Instance._isLanding)
        {
            yield return new WaitForEndOfFrame();
        }

        // コマの座標を習得
        Piece piece1 = controllPiece1.GetComponent<Piece>();
        Piece piece2 = controllPiece2.GetComponent<Piece>();
        int piece1z = (int)piece1.transform.position.z * -1;
        int piece2z = (int)piece2.transform.position.z * -1;
        int piece1x = (int)piece1.transform.position.x;
        int piece2x = (int)piece2.transform.position.x;

        GameDirector.Instance.gameState = GameDirector.GameState.idle;

        // 一番上におかれたときに除外
        Map.Instance.CheckHeightOver(controllPiece1, true);
        Map.Instance.CheckHeightOver(controllPiece2, true);

        // 関数呼び出し
        ForceConvertionReverse(piece1x, piece1z);
        ForceConvertionReverse(piece2x, piece2z);

        GameDirector.Instance.gameState = GameDirector.GameState.reversed;
        yield return null;
    }

    /// <summary>
    /// 強制変換の処理関数
    /// </summary>
    /// <param name="px">pieceX座標 (横)</param>
    /// <param name="pz">pieceZ座標 (縦)</param>
    private void ForceConvertionReverse(int px, int pz)
    {
        int myColorCount = 0;

        // 落ちたコマの座標 x,z - 1をして3回ネストで回す
        for (int z = pz; z < pz + 3; z++)
        {
            for (int x = px; x < px + 3; x++)
            {
                // 一番下変更できないコマが来た場合スルー
                if (z > 9)
                {
                    continue;
                }

                // 固定コマを解除
                if (Map.Instance.map[z - 1, x - 1] == enemyColorfixity)
                {
                    Map.Instance.map[z - 1, x - 1] = enemyColor;
                    Map.Instance.pieceMap[z - 1, x - 1].GetComponent<Piece>().ChangeIsFixity();
                }

                // その座標に相手の駒があった場合、自分の色に変更
                if (Map.Instance.map[z - 1, x - 1] == enemyColor)
                {
                    Map.Instance.map[z - 1, x - 1] = myColor;
                    Map.Instance.pieceMap[z - 1, x - 1].GetComponent<Piece>().SkillReverse(false);
                    myColorCount++;
                }
            }
        }

        // スコア追加
        AddSkillScore(100, myColorCount);
    }


    // 一列一式
    // 横一列をすべて自分の色に変える(固定駒も適用)
    public void OneRowSet(int cost)
    {
        if ((!ActivateCheck(GameDirector.GameState.preActive, cost) &&
             !ActivateCheck(GameDirector.GameState.active, cost)) ||
            SpacialSkillCheck())
        {
            return;
        }
        
        Debug.Log("一列一式");
        skillCutinControl.ShowSkillCutin(1);
        reversedCount -= cost;

        // コマが着地したら処理を行う
        // Boolでフラグ管理をし、それがtrueになったら処理
        StartCoroutine(OneRawWaitCoroutine());
    }

    /// <summary>
    /// 一列一式のコルーチン処理関数
    /// </summary>
    /// <returns></returns>
    IEnumerator OneRawWaitCoroutine()
    {
        while (!GameDirector.Instance._isLanding)
        {
            yield return new WaitForEndOfFrame();
        }

        // スキル変数
        // コマの座標を習得
        Piece piece1 = controllPiece1.GetComponent<Piece>();
        Piece piece2 = controllPiece2.GetComponent<Piece>();
        int piece1z = (int)piece1.transform.position.z * -1;
        int piece2z = (int)piece2.transform.position.z * -1;

        Map.Instance.CheckHeightOver(controllPiece1, true);
        Map.Instance.CheckHeightOver(controllPiece2, true);

        // コマの座標を比較して1個目と2個目の座標が同じだった場合
        if (piece1z == piece2z)
        {
            // piece2のx座標を使用して、for文で回す
            OneRawReverse(piece2z);
        }
        // 比較して片方が落ちた場合落ちたほうの横列を変更
        else if (piece1z != piece2z && piece1z < piece2z)
        {
            OneRawReverse(piece2z);
        }
        else
        {
            OneRawReverse(piece1z);
        }

        Map.Instance.TagClear();
        yield return null;
    }

    /// <summary>
    /// 一列一式のリバース処理
    /// </summary>
    /// <param name="z"></param>
    private void OneRawReverse(int z)
    {
        // スコア変数
        int myColorCount = 0;

        GameDirector.Instance.gameState = GameDirector.GameState.idle;

        // 値で縦軸を判別 (引数で)
        // 横軸分ループを回す
        for (int x = 0; x < 9; x++)
        {
            // 固定駒を一回解除する
            if (Map.Instance.map[z, x] == enemyColorfixity)
            {
                Map.Instance.map[z, x] = enemyColor;
                Map.Instance.pieceMap[z, x].GetComponent<Piece>().ChangeIsFixity();
            }

            // 横軸を変える
            if (Map.Instance.map[z, x] == enemyColor)
            {
                Map.Instance.map[z, x] = myColor;
                Map.Instance.pieceMap[z, x].GetComponent<Piece>().SkillReverse(false);
                myColorCount++;
            }
            else
            {
                continue;
            }
        }

        // スコア追加
        AddSkillScore(100, myColorCount);

        // ゲームステートを変更
        GameDirector.Instance.gameState = GameDirector.GameState.reversed;
    }


    // 優先頂戴
    // 下一番端の自分の色の駒からナナメに全て自分の色に置き換える
    public void PriorityGet(int cost)
    {
        if ((!ActivateCheck(GameDirector.GameState.preActive, cost) &&
             !ActivateCheck(GameDirector.GameState.active, cost)) ||
            SpacialSkillCheck())
        {
            return;
        }
        
        Debug.Log("優先頂戴");
        skillCutinControl.ShowSkillCutin(2);
        reversedCount -= cost;

        // プレイヤーが黒プレイヤーか白か判別
        if (myColor == Map.Instance.black)
        {
            PriorityGetReverse(true);
        }
        else if (myColor == Map.Instance.white)
        {
            PriorityGetReverse(false);
        }
    }

    /// <summary>
    /// 優先頂戴の処理記述
    /// </summary>
    private void PriorityGetReverse(bool black)
    {
        // 判別したら座標右端と左端スタートを区分
        // 黒なら右端 白なら左端
        int x;
        int myColorCount = 0;
        switch (black)
        {
            case true:
                x = 1;
                break;
            case false:
                x = 10;
                break;
            default:
                return;
        }

        for (int z = 0; z < 10; z++)
        {
            // 固定コマ解除
            if (Map.Instance.map[x, z] == enemyColorfixity)
            {
                Map.Instance.map[x, z] = enemyColor;
                Map.Instance.pieceMap[x, z].GetComponent<Piece>().ChangeIsFixity();
            }

            if (Map.Instance.map[x, z] == enemyColor)
            {
                Map.Instance.map[x, z] = myColor;
                Map.Instance.pieceMap[x, z].GetComponent<Piece>().SkillReverse(false);
                myColorCount++;
            }

            switch (black)
            {
                case true:
                    x++;
                    break;
                case false:
                    x--;
                    break;
                default:
                    return;
            }
        }

        AddSkillScore(150, myColorCount);
    }


    // 強奪一瞬
    // 盤面のコマを自分の駒と相手の駒を入れ替える
    public void RobberyMoment(int cost)
    {
        if ((!ActivateCheck(GameDirector.GameState.preActive, cost) &&
             !ActivateCheck(GameDirector.GameState.active, cost)) ||
            SpacialSkillCheck())
        {
            return;
        }

        int myColorCount = 0;
        
        Debug.Log("強奪一瞬");
        skillCutinControl.ShowSkillCutin(0);
        reversedCount -= cost;
        /*SoundManager.Instance.PlaySE(5);*/

        // forでループで探索
        for (int i = 2; i < 9; i++)
        {
            for (int j = 1; j < 9; j++)
            {
                // マップを探索して自分の色があった場合それを相手の色に置き換え
                if (Map.Instance.map[i, j] == myColor)
                {
                    Map.Instance.map[i, j] = enemyColor;
                    Map.Instance.pieceMap[i, j].GetComponent<Piece>().SkillReverse(false);
                }
                else if (Map.Instance.map[i, j] == enemyColor)
                {
                    Map.Instance.map[i, j] = myColor;
                    Map.Instance.pieceMap[i, j].GetComponent<Piece>().SkillReverse(false);
                    // 自分の色 -> 相手の色 になったコマをカウント
                    myColorCount++;
                }
            }
        }

        AddSkillScore(25, myColorCount);
    }
}
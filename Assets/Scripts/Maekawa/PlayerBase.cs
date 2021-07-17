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
    protected Text reversedCountText = null;
    [SerializeField]
    protected Text myPieceCountText = null;
    [SerializeField]
    protected Image charactorImage = null;
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
    protected string myColor = "";

    protected delegate void Skill_1();
    protected delegate void Skill_2();
    protected delegate void Skill_3();
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

    protected void SkillActivate()
    {
        // スキル1...× スキル2...△ スキル3...□
        if(Input.GetKeyDown(KeyCode.Z) || _DS4_cross_value)
        {
            Debug.Log("skill_1");
            skill_1();
        }
        else if(Input.GetKeyDown(KeyCode.X) || _DS4_triangle_value)
        {
            Debug.Log("skill_2");
            skill_2();
        }
        else if(Input.GetKeyDown(KeyCode.C) || _DS4_square_value)
        {
            Debug.Log("skill_3");
            skill_3();
        }
    }

    protected void PieceMove()
    {
        Vector3 move = Vector3.zero;
        bool isDown = false;

        _timeCount += Time.deltaTime;

        // 左右移動
        if ((_DS4_horizontal_value < 0 && last_horizontal_value == 0) || (_DS4_Lstick_horizontal_value < 0 && lastLstick_horizontal_value == 0))
            move.x = -1;
        else if ((_DS4_horizontal_value > 0 && last_horizontal_value == 0) || (_DS4_Lstick_horizontal_value > 0 && lastLstick_horizontal_value == 0))
            move.x = 1;

        // 下移動
        if ((_DS4_vertical_value < 0 && last_vertical_value == 0) || (_DS4_Lstick_vertical_value < 0 && last_Lstick_vertical_value == 0))
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
        // 左回転
        if (_DS4_L1_value || _keyBoardLeft)
        {
            rotationNum++;
            SoundManager.Instance.PlaySE(2);
        }
        // 右回転(=左に3回転)
        else if (_DS4_R1_value || _keyBoardRight)
        {
            rotationNum += 3;
            SoundManager.Instance.PlaySE(2);
        }

        // 疑似回転(移動がややこしくなるのでRotationはいじらない)
        rotationNum %= 4;
        Vector3 rotatedPos = controllPiece1.transform.position + rotationPos[rotationNum];

        if (Map.Instance.CheckWall(rotatedPos))
            controllPiece2.transform.position = rotatedPos;
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
            GameDirector.Instance.gameState = GameDirector.GameState.active;
    }


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private bool CheckColor(bool isMycolor)
    {
        string targetColor;
        
        // 自分の色
        if (isMycolor)
            targetColor = myColor;
        else// 相手の色
        {
            if (myColor == Map.Instance.black)
                targetColor = Map.Instance.white;
            else
                targetColor = Map.Instance.black;
        }

        bool isThere = false;
        // 下一行を除いたコマが置かれる可能性のあるマスを探索
        for (int i = 2; i < 9; i++)
            for (int j = 1; j < 9; j++)
            {
                if (Map.Instance.map[i, j] == targetColor)
                    isThere = true;
            }

        return isThere;
    }

    //  強引
    public void Forcibly()
    {
        // 最下段を除くマップに相手の色がなければリターン(固定コマは対象外)
        if (!CheckColor(false))
        {
            Debug.Log("相手の色がありません");
            return;
        }

        if (GameDirector.Instance.gameState == GameDirector.GameState.preActive)
            GameDirector.Instance.gameState = GameDirector.GameState.idle;
        else
            return;

        // 相手の色を決定
        string enemyColor;
        if (myColor == Map.Instance.black)
            enemyColor = Map.Instance.white;
        else
            enemyColor = Map.Instance.black;

        while (true)
        {
            // 適当にランダムな座標をとり、それが自分の色なら変換
            int z = Random.Range(2, 9);
            int x = Random.Range(1, 9);
            if (Map.Instance.map[z, x] == enemyColor)
            {
                Map.Instance.map[z, x] = myColor;
                Map.Instance.pieceMap[z, x].GetComponent<Piece>().SkillReverse();
                Map.Instance.isSkillActivate = true;
                // 検索、リバース処理を行う
                Map.Instance.TagClear();
                StartCoroutine(Map.Instance.CheckReverse(Map.Instance.pieceMap[z, x]));
                break;
            }
        }
    }

    // 固定
    public void Lock()
    {
        // 最下段を除くマップに自分の色がなければリターン(固定コマは対象外)
        if (!CheckColor(true))
            return;

        GameDirector.Instance.gameState = GameDirector.GameState.idle;
        while (true)
        {
            // 適当にランダムな座標をとり、それが自分の色なら変換
            int z = Random.Range(2, 9);
            int x = Random.Range(1, 9);
            if (Map.Instance.map[z, x] == myColor)
            {
                Map.Instance.pieceMap[z, x].GetComponent<Piece>().pieceType = Piece.PieceType.fixity;
                if (myColor == Map.Instance.black)
                    Map.Instance.map[z, x] = Map.Instance.fixed_black;
                else
                    Map.Instance.map[z, x] = Map.Instance.fixed_black;
                break;
            }
        }

        GameDirector.Instance.gameState = GameDirector.GameState.active;
    }

    // 残影
    public void BeLeftShadow()
    {
        Piece.PieceType playerType;
        if (myColor == Map.Instance.black)
            playerType = Piece.PieceType.black;
        else
            playerType = Piece.PieceType.white;

        // 自分の色があれば固定コマに
        if (controllPiece1.GetComponent<Piece>().pieceType == playerType)
            controllPiece1.GetComponent<Piece>().pieceType = Piece.PieceType.fixity;
        if (controllPiece2.GetComponent<Piece>().pieceType == playerType)
            controllPiece2.GetComponent<Piece>().pieceType = Piece.PieceType.fixity;
    }
}
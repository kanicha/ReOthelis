using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private const byte _WIDTH = 10;
    private const byte _HEIGHT = 11;
    private const byte _EMPTY_AREAS_HEIGHT = 2;// 上２ラインに置かれたコマは消滅する
    public string[,] _map = new string[_HEIGHT, _WIDTH]// z, x座標で指定
    {
        { "■", "□", "□", "□", "□", "□", "□", "□", "□", "■" },
        { "■", "□", "□", "□", "□", "□", "□", "□", "□", "■" },
        { "■", "□", "□", "□", "□", "□", "□", "□", "□", "■" },
        { "■", "□", "□", "□", "□", "□", "□", "□", "□", "■" },
        { "■", "□", "□", "□", "□", "□", "□", "□", "□", "■" },
        { "■", "□", "□", "□", "□", "□", "□", "□", "□", "■" },
        { "■", "□", "□", "□", "□", "□", "□", "□", "□", "■" },
        { "■", "□", "□", "□", "□", "□", "□", "□", "□", "■" },
        { "■", "□", "□", "□", "□", "□", "□", "□", "□", "■" },
        { "■", "〇", "□", "□", "□", "□", "□", "□", "●", "■" },
        { "■", "■", "■", "■", "■", "■", "■", "■", "■", "■" }
    };

    public static readonly string wall = "■";
    public static readonly string empty = "□";
    public static readonly string white = "〇";
    public static readonly string black = "●";
    private static Map instance = null;
    public static Map Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (Map)FindObjectOfType(typeof(Map));

                if (instance == null)
                {
                    Debug.LogError("Not Found Instance");
                }
            }
            return instance;
        }
    }
    /// <summary>
    /// 移動後のコマが障害物に当たるかを調べる
    /// </summary>
    /// <param name="movedPos">コマの移動後座標</param>
    /// <returns></returns>
    public bool CheckWall(Vector3 movedPos)
    {
        bool isBlank = false;
        // ミノの移動後x, y座標
        int z = (int)movedPos.z * -1;// zは基本0以下になるので符号を反転させ配列を指定する
        int x = (int)movedPos.x;

        // 2つの駒の移動後座標に何もなければ移動を通す
        if (_map[z, x] == empty)
            isBlank = true;

        return isBlank;
    }

    public void InputMap(GameObject piece, string Type)
    {
        int z = (int)piece.transform.position.z * -1;
        int x = (int)piece.transform.position.x;

        _map[z, x] = Type;
        _pieceMap[z, x] = piece;
    }

    // ひっくり返す処理
    private List<GameObject> _reversePiece = new List<GameObject>();// ひっくり返すコマを格納
    private GameObject[,] _pieceMap = new GameObject[_HEIGHT, _WIDTH];
    private int _setPosX = 0;
    private int _setPosZ = 0;
    private string _myColor = string.Empty;
    private bool isChecking = false;
    private int numOfReversed = 0;

    [SerializeField]
    private GameDirector director = null;
    public Piece.PieceType turnPlayerColor = Piece.PieceType.none;
    /// <summary>
    /// 実際にオブジェクトをひっくり返す関数
    /// </summary>
    private IEnumerator PieceReverse()
    {
        foreach (GameObject piece in _reversePiece)
        {
            if (turnPlayerColor == Piece.PieceType.black)
                Player_1.score += director.point;
            else
                Player_2.score += director.point;

            piece.GetComponent<Piece>().Reverse();
            yield return new WaitForSeconds(.3f);
        }
        _reversePiece.Clear();
        isChecking = false;
    }

    /// <summary>
    /// 探索の準備と各方向に探索する関数を呼ぶ関数
    /// </summary>
    /// <param name="piece">今置いたコマ</param>
    public IEnumerator CheckReverse(GameObject piece)
    {
        while(isChecking)
            yield return null;

        isChecking = true;

        // 自分の色と相手の色を決定
        if (Piece.PieceType.black == piece.GetComponent<Piece>().pieceType)
            _myColor = black;
        else
            _myColor = white;

        // 置いたマスの座標を取得
        _setPosX = (int)piece.transform.position.x;
        _setPosZ = (int)piece.transform.position.z * -1;

        // 7方向にチェック(zは符号が逆転する)
        CheckInTheDirection(new Vector3(-1, 0, 0));  // ←
        CheckInTheDirection(new Vector3(1, 0, 0));   // →
        CheckInTheDirection(new Vector3(0, 0, 1));   // ↓
        //CheckInTheDirection(new Vector3(0, 0, -1));// ↑
        CheckInTheDirection(new Vector3(-1, 0, 1));  // ↙
        CheckInTheDirection(new Vector3(1, 0, 1));   // ↘
        CheckInTheDirection(new Vector3(-1, 0, -1)); // ↖
        CheckInTheDirection(new Vector3(1, 0, -1));  // ↗

        for (int a = _EMPTY_AREAS_HEIGHT; a < _HEIGHT; a++)
        {
            string s = "";
            for (int b = 0; b < _WIDTH; b++)
            {
                s += _map[a, b];
            }
            Debug.Log(s);
        }
        StartCoroutine(PieceReverse());
    }

    /// <summary>
    /// dirの方向に探索
    /// </summary>
    /// <param name="dir">x, 0, -z</param>
    private void CheckInTheDirection(Vector3 dir)
    {
        // 調べる座標
        int checkPosX = _setPosX;
        int checkPosZ = _setPosZ;
        // 調べたい方向
        int dirX = (int)dir.x;
        int dirZ = (int)dir.z;

        bool isReverse = false;
        int moveCount = 0;

        // dirの方向に「ひっくり返せるか」探索
        while(true)
        {
            // 調べたい方向に進んでいく
            checkPosX += dirX;
            checkPosZ += dirZ;
            // 壁 or 空白 or なら終了
            if (_map[checkPosZ, checkPosX] == wall || _map[checkPosZ, checkPosX] == empty)
            {
                break;
            }
            else if (_map[checkPosZ, checkPosX] == _myColor)
            {
                // 進んだ先に自分の色があれば終了して裏返せる
                isReverse = true;
                break;
            }
            moveCount++;
        }

        // 裏返しが発生するなら処理(厳密にはmoveCountが0でも処理)
        if (isReverse)
        {
            checkPosX = _setPosX;
            checkPosZ = _setPosZ;

            // ひっくり返せることが確定しているので色の判定はしない
            for(int i = 0; i < moveCount; i++)
            {
                checkPosX += dirX;
                checkPosZ += dirZ;
                _map[checkPosZ, checkPosX] = _myColor;// ←の都合で探索を分割しなければならない
                _reversePiece.Add(_pieceMap[checkPosZ, checkPosX]);
            }
        }
    }

    /// <summary>
    /// マップの各コマの数をチェック、ゲーム終了判定も行う
    /// </summary>
    /// <returns>ゲーム終了かどうか</returns>
    public bool CheckMap()
    {
        bool isEnd = true;
        int black = 0;
        int white = 0;

        for (int i = _EMPTY_AREAS_HEIGHT; i < _HEIGHT; i++)
        {
            for (int j = 0; j < _WIDTH; j++)
            {
                string cell = _map[i, j];

                if (cell == empty)
                    isEnd = false;
                else if (cell == Map.black)
                    black++;
                else if (cell == Map.white)
                    white++;
            }
        }

        if (isEnd)
        {
            Player_1.score += director.point * black;
            Player_2.score += director.point * white;

            if (Player_1.score > Player_2.score)
                Debug.Log("<color=red>1Pの勝ち</color>");
            else if (Player_1.score == Player_2.score)
                Debug.Log("<color=orange>引き分け</color>");
            else
                Debug.Log("<color=blue>2Pの勝ち</color>");
        }

        return isEnd;
    }

    /// <summary>
    /// 上2ラインに置かれたコマを無効にする
    /// </summary>
    /// <param name="piece"></param>
    /// <returns>有効なコマか</returns>
    public bool CheckHeightOver(GameObject piece)
    {
        bool isSafeLine = true;
        if ((int)piece.transform.position.z * -1 < _EMPTY_AREAS_HEIGHT)
        {
            _map[(int)piece.transform.position.z * -1, (int)piece.transform.position.x] = empty;
            piece.transform.position = new Vector3(999, 999, 999);
            isSafeLine = false;
        }
        return isSafeLine;
    }
}

// map確認用
//for (int a = _EMPTY_AREAS_HEIGHT; a<_HEIGHT; a++)
//{
//    string s = "";
//    for (int b = 0; b<_WIDTH; b++)
//    {
//        s += _map[a, b];
//    }
//    Debug.Log(s);
//}
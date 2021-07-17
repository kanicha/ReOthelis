using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : SingletonMonoBehaviour<Map>
{
    private const byte _WIDTH = 10;
    private const byte _HEIGHT = 11;
    private const byte _EMPTY_AREAS_HEIGHT = 2;// 上２ラインに置かれたコマは消滅する
    public string[,] map = new string[_HEIGHT, _WIDTH]// z, x座標で指定
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
        { "■", "〇", "●", "〇", "●", "〇", "●", "〇", "●", "■" },
        { "■", "■", "■", "■", "■", "■", "■", "■", "■", "■" }
    };

    public readonly string wall = "■";
    public readonly string empty = "□";
    public readonly string black = "●";
    public readonly string white = "〇";
    public readonly string fixed_black = "★";
    public readonly string fixed_white = "☆";

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
        if (map[z, x] == empty)
            isBlank = true;

        return isBlank;
    }

    /// <summary>
    /// コマの１つ下のマスが空いていないかを調べる
    /// </summary>
    /// <param name="piecePos"></param>
    /// <returns></returns>
    public bool CheckLanding(Vector3 piecePos)
    {
        bool isGrounded = false;

        // ミノの移動後座標
        int z = (int)piecePos.z * -1;
        int x = (int)piecePos.x;

        if (map[z + 1, x] != empty)
            isGrounded = true;

        return isGrounded;
    }

    /// <summary>
    /// 着地後判定処理関数
    /// </summary>
    public void FallPiece(GameObject piece)
    {
        // 配列指定子用のコマの座標         
        int x = (int)piece.transform.position.x;
        int z = (int)piece.transform.position.z * -1;// zはマイナス方向に進むので符号を反転させる

        int i = 0;
        int dz = 0;

        while (true)
        {
            i++;
            dz = z + i;// iの分だけ下の座標を調べる

            // 設置したマスからi個下のマスが空白なら下に落とす
            if (map[dz, x] == empty)
                piece.transform.position = new Vector3(x, 0, dz * -1);// 反転させたyをマイナスに戻す
            else
            {
                dz--;
                break;
            }
            // これを空白以外に当たるまで繰り返す
        }

        Piece p = piece.GetComponent<Piece>();

        if (p.pieceType == Piece.PieceType.black)
            map[dz, x] = black;
        else if (p.pieceType == Piece.PieceType.white)
            map[dz, x] = white;

        pieceMap[dz, x] = piece;
    }

    // ひっくり返す処理
    private List<GameObject> _reversePiece = new List<GameObject>();// ひっくり返すコマを格納
    public  GameObject[,] pieceMap = new GameObject[_HEIGHT, _WIDTH];
    private int _setPosX = 0;
    private int _setPosZ = 0;
    private string _myColor = string.Empty;
    private bool _isChecking = false;
    private const string _REVERSED_TAG = "Reversed";
    private bool _isSecondCheck = false;
    public Piece.PieceType turnPlayerColor = Piece.PieceType.none;
    public bool isSkillActivate = false;
    /// <summary>
    /// 実際にオブジェクトをひっくり返す関数
    /// </summary>
    private IEnumerator PieceReverse()
    {
        foreach (GameObject piece in _reversePiece)
        {
            if (turnPlayerColor == Piece.PieceType.black)
            {
                GameDirector.Instance.AddScore(true, GameDirector.Instance.point);
                GameDirector.Instance.AddReversedCount(true);
            }
            else
            {
                GameDirector.Instance.AddScore(false, GameDirector.Instance.point);
                GameDirector.Instance.AddReversedCount(false);
            }

            piece.GetComponent<Piece>().Reverse();
            yield return new WaitForSeconds(.3f);
        }

        _reversePiece.Clear();

        // スキル効果なら準備時間に戻る
        if (isSkillActivate)
        {
            GameDirector.Instance.gameState = GameDirector.GameState.preActive;
            isSkillActivate = false;
            _isSecondCheck = false;
        }
        if(_isSecondCheck)// 2回目のチェックならステートを進める
        {
            GameDirector.Instance.gameState = GameDirector.GameState.reversed;
            _isSecondCheck = false;
        }
        else
            _isSecondCheck = true;// 2回目のチェックに入る

        _isChecking = false;
    }

    /// <summary>
    /// 探索の準備と各方向に探索する関数を呼ぶ関数
    /// </summary>
    /// <param name="piece">今置いたコマ</param>
    public IEnumerator CheckReverse(GameObject piece)
    {
        while (_isChecking)
            yield return null;// 2つのコルーチンは片方づつ処理する

        // このターンに置いたコマがリバースしている or このコマが盤面外に置かれているなら
        if (piece.CompareTag(_REVERSED_TAG))
        {
            // 2回目のチェックであることが確定しているのでStateを進める
            GameDirector.Instance.gameState = GameDirector.GameState.reversed;
            _isSecondCheck = false;
            yield break;
        }

        _isChecking = true;

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

        //for (int a = _EMPTY_AREAS_HEIGHT; a < _HEIGHT; a++)
        //{
        //    string s = "";
        //    for (int b = 0; b < _WIDTH; b++)
        //    {
        //        s += map[a, b];
        //    }
        //    Debug.Log(s);
        //}
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
            if (map[checkPosZ, checkPosX] == wall || map[checkPosZ, checkPosX] == empty)
            {
                break;
            }
            else if (map[checkPosZ, checkPosX] == _myColor)
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
                map[checkPosZ, checkPosX] = _myColor;// ←の都合で探索を分割しなければならない
                _reversePiece.Add(pieceMap[checkPosZ, checkPosX]);
                pieceMap[checkPosZ, checkPosX].tag = _REVERSED_TAG;
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
        int blackCount = 0;
        int whiteCount = 0;

        for (int i = _EMPTY_AREAS_HEIGHT; i < _HEIGHT; i++)
        {
            for (int j = 0; j < _WIDTH; j++)
            {
                string cell = map[i, j];

                if (cell == empty)
                    isEnd = false;
                else if (cell == black)
                    blackCount++;
                else if (cell == white)
                    whiteCount++;
            }
        }

        GameDirector.Instance.AddPieceCount(blackCount, whiteCount);

        if (isEnd)
        {
            GameDirector.Instance.AddScore(true, GameDirector.Instance.point * blackCount);
            GameDirector.Instance.AddScore(false, GameDirector.Instance.point * whiteCount);
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
            map[(int)piece.transform.position.z * -1, (int)piece.transform.position.x] = empty;
            piece.transform.position = new Vector3(999, 999, 999);
            isSafeLine = false;

            // 2回目のチェックならステートを進める
            if (_isSecondCheck)
            {
                GameDirector.Instance.gameState = GameDirector.GameState.reversed;
                _isSecondCheck = false;
            }
            else
                _isSecondCheck = true;// 2回目のチェックに入る
        }
        return isSafeLine;
    }

    public void TagClear()
    {
        for (int i = 0; i < _HEIGHT; i++)
        {
            for (int j = 0; j < _WIDTH; j++)
            {
                if (pieceMap[i,j] != null)
                    pieceMap[i, j].tag = "Untagged";
            }
        }
    }
}

// map確認用
//for (int a = _EMPTY_AREAS_HEIGHT; a < _HEIGHT; a++)
//{
//    string s = "";
//    for (int b = 0; b<_WIDTH; b++)
//    {
//        s += _map[a, b];
//    }
//    Debug.Log(s);
//
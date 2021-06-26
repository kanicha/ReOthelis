using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private const byte _WIDTH = 10;
    private const byte _HEIGHT = 10;
    private string[,] _map = new string[_HEIGHT, _WIDTH]// z, x座標で指定
    {
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

    private const string _wall = "■";
    private const string _blank = "□";
    private const string _white = "〇";
    private const string _black = "●";

    /// <summary>
    /// 移動後のコマが障害物に当たるかを調べる
    /// </summary>
    /// <param name="movedPos">コマの移動後座標</param>
    /// <returns></returns>
    public bool CheckWall(Vector3 movedPos)
    {
        bool isBlank = false;
        // ミノの移動後x, y座標
        int z = (int)movedPos.z * -1;// yは基本0以下になるので符号を反転させ配列を指定する
        int x = (int)movedPos.x;

        // 2つの駒の移動後座標に何もなければ移動を通す
        if (_map[z, x] == _blank)
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

        if (_map[z + 1, x] != _blank)
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
            if (_map[dz, x] == _blank)
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
            _map[dz, x] = _black;
        else if (p.pieceType == Piece.PieceType.white)
            _map[dz, x] = _white;

        _pieceMap[dz, x] = piece;
    }

    // ひっくり返す処理
    private List<GameObject> _reversePiece = new List<GameObject>();// ひっくり返すコマを格納
    private GameObject[,] _pieceMap = new GameObject[_HEIGHT, _WIDTH];
    private int _setPosX = 0;
    private int _setPosZ = 0;
    private string _myColor = string.Empty;
    private string _enemyColor = string.Empty;
    private bool isChecking = false;

    /// <summary>
    /// 実際にオブジェクトをひっくり返す関数
    /// </summary>
    private IEnumerator PieceReverse()
    {
        foreach (GameObject piece in _reversePiece)
        {
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
        {
            _myColor = _black;
            _enemyColor = _white;
        }
        else
        {
            _myColor = _white;
            _enemyColor = _black;
        }

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
            if (_map[checkPosZ, checkPosX] == _wall || _map[checkPosZ, checkPosX] == _blank)
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
}

// map確認用
//for(int i = 0; i < _HEIGHT; i++)
//{
//    string s = "";
//    for(int j = 0; j < _WIDTH; j++)
//    {
//        s += _map[i, j];
//    }
//    Debug.Log(s);
//}
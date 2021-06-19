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
        { "■", "□", "□", "□", "□", "□", "□", "□", "□", "■" },
        { "■", "■", "■", "■", "■", "■", "■", "■", "■", "■" }
    };

    private const string _wall = "■";
    private const string _blank = "□";
    private const string _white = "〇";
    private const string _black = "●";

    private void Update()
    {
        for (int i = 0; i < _HEIGHT; i++)
            for (int j = 0; j < _WIDTH; j++)
                isReversed[i, j] = false;
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
    }

    // ひっくり返す処理

    // ひっくり返すやつコマを集める
    List<GameObject> ReversePiece = null;
    private bool[,] isReversed = new bool[_HEIGHT, _WIDTH];

    public void ListClear()
    {
        ReversePiece.Clear();
        for (int i = 0; i < _HEIGHT; i++)
            for (int j = 0; j < _WIDTH; j++)
                isReversed[i, j] = false;
    }

    private void CheckReverse(GameObject piece, int player)
    {
        string myColor;
        string enemyColor;

        if (player == 0)// 黒プレイヤーなら対象は白
        {
            myColor = _black;
            enemyColor = _white;
        }
        else
        {
            myColor = _white;
            enemyColor = _black;
        }

        // 検索での配列指定に使用
        int setPosX = (int)piece.transform.position.x;
        int setPosZ = (int)piece.transform.position.z * -1;

        int checkPosX = setPosX;
        
        // ←方向に探索
        while (true)
        {
            checkPosX--;
            // 壁か空白に当たったら終了
            if (_map[setPosZ, checkPosX] == _wall || _map[setPosZ, checkPosX] == _blank)
                break;
            // 相手色 かつ このターンに裏返ってなければリバース
            else if (_map[setPosZ, checkPosX] == enemyColor && !isReversed[setPosZ, checkPosX])
            {
                isReversed[setPosZ, checkPosX] = true;
                _map[setPosZ, checkPosX] = myColor;
                ReversePiece.Add(piece);
            }
            // そのほかのコマはスルー
        }
        // →方向に検索
        checkPosX = setPosX;
        while (true)
        {
            checkPosX++;
            if (_map[setPosZ, checkPosX] == _wall || _map[setPosZ, checkPosX] == _blank)
                break;
            else if (_map[setPosZ, checkPosX] == enemyColor && !isReversed[setPosZ, checkPosX])
            {
                isReversed[setPosZ, checkPosX] = true;
                _map[setPosZ, checkPosX] = myColor;
                ReversePiece.Add(piece);
            }
        }
        // ↓方向に検索
        int checkPosZ = setPosZ;
        while (true)
        {
            checkPosX++;
            if (_map[setPosZ, checkPosX] == _wall || _map[setPosZ, checkPosX] == _blank)
                break;
            else if (_map[setPosZ, checkPosX] == enemyColor && !isReversed[setPosZ, checkPosX])
            {
                isReversed[setPosZ, checkPosX] = true;
                _map[setPosZ, checkPosX] = myColor;
                ReversePiece.Add(piece);
            }
        }
    }
}
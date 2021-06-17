using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private const byte _WIDTH = 10;
    private const byte _HEIGHT = 10;
    private string[,] map = new string[_HEIGHT, _WIDTH]// x, z座標で指定
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

    private GameObject[,] PieceArray = new GameObject[_HEIGHT, _WIDTH];

    void Start()
    {

    }

    void Update()
    {

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
        if (map[z, x] == "□")
            isBlank = true;

        return isBlank;
    }

    /// <summary>
    /// コマの１つ下のマスが空いていないかを調べる
    /// </summary>
    /// <param name="movedPos"></param>
    /// <returns></returns>
    public bool CheckLanding(Vector3 movedPos)
    {
        bool isGrounded = false;

        // ミノの移動後座標
        int z = (int)movedPos.z * -1;
        int x = (int)movedPos.x;

        if (map[z + 1, x] != "□")
            isGrounded = true;

        return isGrounded;
    }


    /// <summary>
    /// 着地後判定処理関数
    /// </summary>
    public void FallPiece(GameObject piece)
    {
        // 配列指定子用のコマの座標         
        int x = (int)piece.transform.position.z;
        int z = (int)piece.transform.position.z * -1;// zはマイナス方向に進むので符号を反転させる

        int i = 0;     
        while (true)
        {
            i++;
            int dz = z + i;// iの分だけ下の座標を調べる

            // 設置したマスからi個下のマスが空白なら下に落とす
            if (map[dz, x] == "□")
                piece.transform.position = new Vector3(x, dz * -1);// 反転させたyをマイナスに戻す
            else
                break;

            // これを空白以外に当たるまで繰り返す
        }
    }
}

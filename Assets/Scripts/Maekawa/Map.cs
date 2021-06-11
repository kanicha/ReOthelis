using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private const byte _WIDTH = 10;
    private const byte _HEIGHT = 10;
    private string[,] map = new string[_HEIGHT, _WIDTH]
    {
        { "■", "□", "□","□","□","□", "□","□", "□", "■" },
        { "■", "□", "□","□","□","□", "□","□", "□", "■" },
        { "■", "□", "□","□","□","□", "□","□", "□", "■" },
        { "■", "□", "□","□","□","□", "□","□", "□", "■" },
        { "■", "□", "□","□","□","□", "□","□", "□", "■" },
        { "■", "□", "□","□","□","□", "□","□", "□", "■" },
        { "■", "□", "□","□","□","□", "□","□", "□", "■" },
        { "■", "□", "□","□","□","□", "□","□", "□", "■" },
        { "■", "□", "□","□","□","□", "□","□", "□", "■" },
        { "■", "■", "■","■","■","■", "■","■", "■", "■" }
    };

    private int[,] komaArray = new int[_HEIGHT, _WIDTH];


    void Start()
    {
        komaArray = null;
    }

    void Update()
    {

    }

    /// <summary>
    /// 同時に操作する2つの駒が障害物に当たらなければ移動を通す
    /// </summary>
    /// <param name="movedPos">軸ミノの移動後座標</param>
    /// <param name="rotationPos">回転側の移動後座標</param>
    /// <returns></returns>
    public bool CheckWall(Vector3 movedPos, Vector3 rotMovedPos)
    {
        bool pass = false;
        // 軸ミノの移動後x, y座標
        int y = (int)movedPos.y * -1;// yは基本0以下になるので符号を反転させ配列を指定する
        int x = (int)movedPos.x;
        // 回転する方の移動後x, y座標
        int rotY = (int)rotMovedPos.y * -1;
        int rotX = (int)rotMovedPos.x;

        // 2つの駒の移動後座標に何もなければ移動を通す
        if (map[y, x] == "□" && map[rotY, rotX] == "□")
            pass = true;

        return pass;
    }

    /// <summary>
    /// 設置判定関数
    /// </summary>
    public void GroundStack(Vector3 movePos, Vector3 rotMovedPos)
    {
        // ミノの移動後座標
        int y = (int)movePos.y * -1;
        int x = (int)movePos.x;
        // 回転する方の移動後x, y座標
        int rotY = (int)rotMovedPos.y * -1;
        int rotX = (int)rotMovedPos.x;


        if (map[x, y] == "■")
        {
            Debug.Log("Landing");
        }


        //if(pieces)
        //	GameManager.isWaiting = true;
    }
}

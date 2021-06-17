using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    MinoController mc = null;

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


        GameObject[] objArray = new GameObject[2];

        GameObject obj1 = new GameObject("obj");
        GameObject obj2 = new GameObject("obj");

        obj1.transform.position = new Vector3(4, -5);
        obj2.transform.position = new Vector3(0, -1);

        objArray[0] = obj1;
        objArray[1] = obj2;
        Fallkoma(obj1);
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


        if (map[y + 1, x] != "□" || map[rotY + 1, rotX] != "□")
        {
            mc.isLanding = true;
        }

        //if(pieces)
        //	GameManager.isWaiting = true;
    }


    /// <summary>
    /// 着地後判定処理関数
    /// </summary>
    public void Fallkoma(GameObject piece)
    {
        Vector3 piecesVec = piece.transform.position;

        // ミノの移動後座標
        int y = (int)piecesVec.y;
        int x = (int)piecesVec.x;

        int j = 0;
        
        while (true)
        {

            Debug.Log(y * -1 + j);

            if (map[y * -1 + j, x] != "□")
            {
                piece.transform.position = new Vector3(x, (y * -1) - j + 1);

                Debug.Log(piece.transform.position);

                break;
            }
            j--;
        }
    }

}

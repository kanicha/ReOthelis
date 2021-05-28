using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    // ステージの大きさ
    private static int width = 10;
    private static int height = 18;

    // グリッド宣言
    private static Transform[,] grid = new Transform[width, height];


    /// <summary>
    /// コマに当たり判定追加関数 (重ならず上に乗るかどうか)
    /// </summary>
    public void AddGrid()
    {
        foreach (Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);
            grid[roundX, roundY] = children;
        }
    }

    /// <summary>
    /// コマが枠外に出ないためにする関数
    /// </summary>
    public bool ValidMovement()
    {
        foreach (Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);
            // minoがステージよりはみ出さないように制御
            if (roundX <= -1 || roundX >= width || roundY < 0)
            {
                return false;
            }
            if (grid[roundX, roundY] != null)
            {
                return false;
            }
        }

        return true;
    }
    
    /// <summary>
    /// コマが空中に浮いた際地面に落ちる処理関数
    /// </summary>
    public void Drop()
    {
        int nullCount = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y] == null)
                {
                    nullCount++;
                }
                else if (nullCount > 0)
                {
                    grid[x, y].transform.position += new Vector3(0, -nullCount, 0);
                    grid[x, y - nullCount] = grid[x, y];
                    grid[x, y] = null;
                }
            }
            nullCount = 0;
        }
    }
}

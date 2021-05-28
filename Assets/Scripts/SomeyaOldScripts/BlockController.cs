using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{

    public float previousTime;
    // minoが落ちるタイム
    public float fallTime = 1f;
    // mino回転
    public Vector3 rotationPoint;
    // ミノ地面着地判定変数
    public bool komaLanding = false;
    // グリッド制御クラス呼び出し
    private GridManager _gridManager;
    // コントローラー入力制御
    private DS4Controller ds4;

    // ステージの大きさ
    private static int width = 10;
    private static int height = 18;

    // グリッド宣言
    private static Transform[,] grid = new Transform[width, height];



    private void Start()
    {
        _gridManager = GetComponent<GridManager>();
        ds4 = GetComponent<DS4Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        MinoMovement();
    }

    /// <summary>
    /// コマ移動関数
    /// </summary>
    private void MinoMovement()
    {
        // Aキーで左に動く
        if (ds4.fDS4Horizontal < 0 || Input.GetKeyDown(KeyCode.A))
        {
            this.gameObject.transform.position += new Vector3(-1, 0, 0);
            if (!ValidMovement())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        // Dキーで右に動く
        else if (ds4.fDS4Horizontal > 0 || Input.GetKeyDown(KeyCode.D))
        {
            this.gameObject.transform.position += new Vector3(1, 0, 0);
            if (!ValidMovement())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        // 自動で下に移動させつつ、Sキーでも移動する
        else if (Input.GetKeyDown(KeyCode.S) || ds4.fDS4Vertical < 0
                || Time.time - previousTime >= fallTime)
        {
            this.gameObject.transform.position += new Vector3(0, -1, 0);
            if (!ValidMovement())
            {
                this.gameObject.transform.position -= new Vector3(0, 1, 0);
                komaLanding = true;    // コマ着地判定変数 true
                this.enabled = false;
                FindObjectOfType<SpawnKoma>().KomaCreate();
                AddGrid();
                Drop();
            }
            previousTime = Time.time;
        }
        else if (Input.GetButtonDown(ds4.DS4L1) || Input.GetKeyDown(KeyCode.Space))
        {
            // ブロックの右回転
            this.gameObject.transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            foreach (Transform children in transform)
            {
                children.transform.RotateAround(children.transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
            if (!ValidMovement())
            {
                this.gameObject.transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
                foreach (Transform children in transform)
                {
                    children.transform.RotateAround(children.transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                }
            }
        }
        else if (Input.GetButtonDown(ds4.DS4R1))
        {
            // ブロックの左回転
            this.gameObject.transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            foreach (Transform children in transform)
            {
                children.transform.RotateAround(children.transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            }
            if (!ValidMovement())
            {
                this.gameObject.transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                foreach (Transform children in transform)
                {
                    children.transform.RotateAround(children.transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
                }
            }
        }

        /// <summary>
        /// コマに当たり判定追加関数 (重ならず上に乗るかどうか)
        /// </summary>
        void AddGrid()
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
        bool ValidMovement()
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
        void Drop()
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
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDraw : MonoBehaviour
{
	private const byte _WIDTH = 10;
	private const byte _HEIGHT = 10;
	private string[,] map = new string[_HEIGHT, _WIDTH]
	{
		{ "■", "□", "□","□","□","□", "□","□", "□", "■" },
		{ "■", "□", "□","□","□","□", "□","□", "□", "■" },
		{ "■", "□", "□","□","□","□", "□","□", "□", "■" },
		{ "■", "□", "□","□","□","□", "□","□", "□", "■" },
		{ "■", "●", "□","●","〇","●", "〇","□", "〇", "■" },
		{ "■", "●", "〇","〇","〇","●", "●","●", "●", "■" },
		{ "■", "●", "〇","●","〇","●", "〇","●", "〇", "■" },
		{ "■", "●", "〇","〇","〇","●", "●","●", "〇", "■" },
		{ "■", "●", "●","●","〇","●", "〇","〇", "〇", "■" },
		{ "■", "■", "■","■","■","■", "■","■", "■", "■" }
	};

	const string black = "●";
	const string white = "〇";
	const string wall = "■";
	const string empty = "□";
	private string color = "";
	string memory_color = "";

	// Start is called before the first frame update
	void Start()
    {
		MapDebug();
		//MinoCheck(6, 6);
		MinoCheck(3, 6);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	//Player 1 = 黒プレイヤー
	//Player 2 = 白プレイヤー
	//駒が縦で置かれた場合、下方向から上方向の順番で検索を行う
	//駒が横で置かれた場合、左方向から右方向の順番で検索を行う
	//駒が落ちた場合、下方向から上方向の順番で検索を行う
	public void Ordering(int x1, int y1, int x2, int y2, int player)
    {
		//黒プレイヤーの場合、黒の駒を優先して検索を行う
		if (player == 1)
		{
			if (map[y1,x1] == black)
            {
				//二番目の駒の色を記憶
				//二番目の駒が一番目の駒によってひっくり返されたら、その駒のひっくり返り処理を行わない
				memory_color = map[y2, x2];
				MinoCheck(x1, y1);
				if (map[y2,x2] == memory_color)
                {
					MinoCheck(x2, y2);
                }
			}
			else if (map[y1,x1] == white)
            {
				if (map[y2,x2] == black)
                {
					memory_color = map[y1, x1];
					MinoCheck(x2, y2);
					if (map[y1,x1] == memory_color)
                    {
						MinoCheck(x1, y1);
                    }
                }
				else if (map[y2,x2] == white)
                {
					MinoCheck(x1, y1);
					MinoCheck(x2, y2);
                }
            }
		}

		//白プレイヤーの場合、白の駒を優先して検索を行う
		else if (player == 2)
		{
			if (map[y1, x1] == white)
			{
				memory_color = map[y2, x2];
				MinoCheck(x1, y1);
				if (map[y2, x2] == memory_color)
				{
					MinoCheck(x2, y2);
				}
			}
			else if (map[y1, x1] == black)
			{
				if (map[y2, x2] == white)
				{
					memory_color = map[y1, x1];
					MinoCheck(x2, y2);
					if (map[y1, x1] == memory_color)
					{
						MinoCheck(x1, y1);
					}
				}
				else if (map[y2, x2] == white)
				{
					MinoCheck(x1, y1);
					MinoCheck(x2, y2);
				}
			}
		}
    }

	//全方向を検索
	private void MinoCheck(int x, int y)
    {
		color = map[y,x];
		for (int i = -1; i < 2; i++)
        {
			for (int j = -1; j < 2; j++)
            {
				//置かれた駒のマスは検索しない
				if (i == 0 && j == 0)
                {
					continue;
                }
				//壁に当たった、または超えてしまう場合は検索しない
				if (y + i < 0 || x + j < 0 || y + i > 9 || x + j > 9)
                {
					continue;
                }
				//検索の距離を足していく
				for (int s = 2; s < 9; s++)
				{
					if (x + j * s >= 0 && x + j * s < 9 && y + i * s >= 0 && y + i * s < 9)
					{
						//空きに当たった場合、その方向の検索を終了させる
						if (map[y + i * s,x + j * s] == empty)
						{
							break; 
						}
						//同じ色の駒に当たった場合、そのマスにいたるまでのマスの駒をひっくり返す
						if (map[y + i * s,x + j * s] == color)
						{
							for (int n = 1; n < s; n++)
                            {
								map[y + i * n,x + j * n] = color;
							}
							break;
						}
					}
				}
            }
        }
		MapDebug();
    }

	private void MapDebug()
    {
		string printMap = "";
		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 9; j++)
			{
				printMap += map[i, j].ToString() + ":";
			}
			printMap += "\n";
		}

		Debug.Log(printMap);
	}

	
}

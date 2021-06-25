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
		{ "■", "□", "□","□","〇","〇", "□","□", "□", "■" },
		{ "■", "□", "●","□","〇","〇", "□","□", "□", "■" },
		{ "■", "□", "〇","〇","〇","●", "〇","□", "□", "■" },
		{ "■", "□", "〇","●","●","●", "〇","□", "□", "■" },
		{ "■", "□", "〇","〇","●","●", "●","□", "〇", "■" },
		{ "■", "〇", "〇","●","●","●", "●","〇", "●", "■" },
		{ "■", "■", "■","■","■","■", "■","■", "■", "■" }
	};

	const string black = "●";
	const string white = "〇";
	const string wall = "■";
	const string empty = "□";
	private string color = ""; //操作プレイヤーの色
	private string Opponent = ""; //相手プレイヤーの色
	private string memory_color = "";
	private int blackScore = 0;
	private int whiteScore = 0;

	// Start is called before the first frame update
	void Start()
    {
		MapDebug();
		//MinoCheck(6, 6);
		//MinoCheck(3, 6);
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
        {
			map[7, 7] = white;
			map[4, 6] = white;
			Ordering(7, 7, 6, 4, 2);
        }
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
		int scoreCounter = 0;
		//操作プレイヤーと相手プレイヤーの色を取得
		color = map[y,x];
		if (color == black)
		{
			Opponent = white;
		}
		else if (color == white)
        {
			Opponent = black;
        }

		for (int i = -1; i < 2; i++)
        {
			for (int j = -1; j < 2; j++)
            {
				Debug.Log("x方向 = " + j);
				Debug.Log("y方向 = " + i);
				//検索方向変数
				int Direction_X = x + j;
				int Direction_Y = y + i;

				//置かれた駒のマスは検索しない
				if (i == 0 && j == 0)
                {
					continue;
                }
				//検索方向に相手プレイヤーの駒が存在しない場合、その方向の検索を終了させる
				if (map[Direction_Y,Direction_X] != Opponent)
                {
					continue;
                }
				//検索の距離を足していく
				for (int s = 2; s < 9; s++)
				{
					Debug.Log("距離 = " + s);
					//検索マス関数
					int Range_X = x + j * s;
					int Range_Y = y + i * s;

					if (Range_X >= 0 && Range_X < 10 && Range_Y >= 0 && Range_Y < 9)
					{
						//相手の駒を発見したあとに空きに当たった場合、その方向の検索を終了させる
						if (map[Range_Y, Range_X] == empty || map[Range_Y, Range_X] == wall)
						{
							break; 
						}
						//相手の駒を発見したあとに同じ色の駒に当たった場合、そのマスにいたるまでのマスの駒をひっくり返す
						if (map[Range_Y, Range_X] == color)
						{
							for (int n = 1; n < s; n++)
                            {
								int Change_X = x + j * n;
								int Change_Y = y + i * n;
								map[Change_Y, Change_X] = color;
								scoreCounter++;
								Debug.Log("count = " + scoreCounter);
							}
							break;
						}
					}
				}
            }
        }
		MapDebug();
		ScoreCount(scoreCounter, color);
    }

	private void MapDebug()
    {
		string printMap = "";
		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 10; j++)
			{
				printMap += map[i, j].ToString() + ":";
			}
			printMap += "\n";
		}

		Debug.Log(printMap);
	}

	private void ScoreCount(int ChangeAmount, string playerColor)
    {
		if (playerColor == black)
        {
			if (ChangeAmount >= 4)
            {
				blackScore = 150 * ChangeAmount + blackScore;
				Debug.Log("Player1 Score:" + blackScore);
            }
			else
            {
				blackScore = 100 * ChangeAmount + blackScore;
				Debug.Log("Player1 Score:" + blackScore);
			}
        }
		else if (playerColor == white)
        {
			if (ChangeAmount >= 4)
            {
				whiteScore = 150 * ChangeAmount + whiteScore;
				Debug.Log("Player2 Score:" + whiteScore);
			}
			else
            {
				whiteScore = 100 * ChangeAmount + whiteScore;
				Debug.Log("Player2 Score:" + whiteScore);
			}
        }
    }
}

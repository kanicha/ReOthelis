using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIThinking : MonoBehaviour
{
    public readonly string wall = "■";
    public readonly string empty = "□";
    public readonly string black = "●";
    public readonly string white = "〇";
    public readonly string fixityBlack = "★";
    public readonly string fixityWhite = "☆";
    private string _myColor = string.Empty;
    private string _enemyColor = string.Empty;
    private int[,] CheckEmpty = new int [16,4];

    public void CheckVertical()
    {
        int ItemNum = 0;
        for (int x = 1; x < 9; x++)
        {
            for (int z = 9; z > 1; z--)
            {
                if (Map.Instance.map[z, x] == empty && Map.Instance.map[z+1,x] != empty)
                {
                    CheckEmpty[ItemNum,0] = x;
                    CheckEmpty[ItemNum,1] = z;

                    //CheckIfBlack(ItemNum,x,z,new Vector3(-1, 0, 0));  // ←
                    //CheckIfBlack(ItemNum,x,z,new Vector3(1, 0, 0));   // →
                    //CheckIfBlack(ItemNum,x,z,new Vector3(0, 0, 1));   // ↓
                    //CheckIfBlack(ItemNum,x,z,new Vector3(-1, 0, 1));  // ↙
                    //CheckIfBlack(ItemNum,x,z,new Vector3(1, 0, 1));   // ↘
                    //CheckIfBlack(ItemNum,x,z,new Vector3(-1, 0, -1)); // ↖
                    //CheckIfBlack(ItemNum,x,z,new Vector3(1, 0, -1));  // ↗
                    
                    //CheckIfWhite(ItemNum,x,z,new Vector3(-1, 0, 0));  // ←
                    //CheckIfWhite(ItemNum,x,z,new Vector3(1, 0, 0));   // →
                    //CheckIfWhite(ItemNum,x,z,new Vector3(0, 0, 1));   // ↓
                    //CheckIfWhite(ItemNum,x,z,new Vector3(-1, 0, 1));  // ↙
                    //CheckIfWhite(ItemNum,x,z,new Vector3(1, 0, 1));   // ↘
                    //CheckIfWhite(ItemNum,x,z,new Vector3(-1, 0, -1)); // ↖
                    //CheckIfWhite(ItemNum,x,z,new Vector3(1, 0, -1));  // ↗

                    CheckIfWhite(ItemNum,x,z);
                    CheckIfBlack(ItemNum,x,z);

                    ItemNum++;

                    if (z > 2)
                    {
                        CheckEmpty[ItemNum,0] = x;
                        CheckEmpty[ItemNum,1] = z-1;

                        //CheckIfBlack(ItemNum,x,z-1,new Vector3(-1, 0, 0));  // ←
                        //CheckIfBlack(ItemNum,x,z-1,new Vector3(1, 0, 0));   // →
                        //CheckIfBlack(ItemNum,x,z-1,new Vector3(0, 0, 1));   // ↓
                        //CheckIfBlack(ItemNum,x,z-1,new Vector3(-1, 0, 1));  // ↙
                        //CheckIfBlack(ItemNum,x,z-1,new Vector3(1, 0, 1));   // ↘
                        //CheckIfBlack(ItemNum,x,z-1,new Vector3(-1, 0, -1)); // ↖
                        //CheckIfBlack(ItemNum,x,z-1,new Vector3(1, 0, -1));  // ↗
                    
                        //CheckIfWhite(ItemNum,x,z-1,new Vector3(-1, 0, 0));  // ←
                        //CheckIfWhite(ItemNum,x,z-1,new Vector3(1, 0, 0));   // →
                        //CheckIfWhite(ItemNum,x,z-1,new Vector3(0, 0, 1));   // ↓
                        //CheckIfWhite(ItemNum,x,z-1,new Vector3(-1, 0, 1));  // ↙
                        //CheckIfWhite(ItemNum,x,z-1,new Vector3(1, 0, 1));   // ↘
                        //CheckIfWhite(ItemNum,x,z-1,new Vector3(-1, 0, -1)); // ↖
                        //CheckIfWhite(ItemNum,x,z-1,new Vector3(1, 0, -1));  // ↗

                        CheckIfWhite(ItemNum,x,z-1);
                        CheckIfBlack(ItemNum,x,z-1);
                    
                        ItemNum++;
                    }
                    break;
                }
            }
        }
    }

    public void ShowData()
    {
        string printData = "";
		for (int i = 0; i < 16; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				printData += CheckEmpty[i, j].ToString() + ",";
			}
			printData += "\n";
		}

		Debug.Log(printData);
    }

/*
    private void CheckIfBlack(int CheckingNum,int PosX,int PosZ,Vector3 dir)
    {
        // 調べる座標
        int checkPosX = PosX;
        int checkPosZ = PosZ;
        // 調べたい方向
        int dirX = (int)dir.x;
        int dirZ = (int)dir.z;
        
        int moveCount = 0;
        int enemyPieceCounter = 0;

        // dirの方向に「ひっくり返せるか」探索
        while(true)
        {
            // 調べたい方向に進んでいく
            checkPosX += dirX;
            checkPosZ += dirZ;
            string targetType = Map.Instance.map[checkPosZ, checkPosX];

            // 壁 or 空白なら終了
            if (targetType == wall || targetType == empty)
            {
                break;
            }
            else if (targetType == white)
            {
                enemyPieceCounter++;
            }
            else if (targetType == black || targetType == fixityBlack)
            {
                CheckEmpty[CheckingNum,3] = enemyPieceCounter;
                break;
            }
            moveCount++;
        }
    }

    private void CheckIfWhite(int CheckingNum,int PosX,int PosZ,Vector3 dir)
    {
        // 調べる座標
        int checkPosX = PosX;
        int checkPosZ = PosZ;
        // 調べたい方向
        int dirX = (int)dir.x;
        int dirZ = (int)dir.z;
        
        int moveCount = 0;
        int enemyPieceCounter = 0;

        // dirの方向に「ひっくり返せるか」探索
        while(true)
        {
            // 調べたい方向に進んでいく
            checkPosX += dirX;
            checkPosZ += dirZ;
            string targetType = Map.Instance.map[checkPosZ, checkPosX];

            // 壁 or 空白なら終了
            if (targetType == wall || targetType == empty)
            {
                enemyPieceCounter = 0;
                break;
            }
            else if (targetType == black)
            {
                enemyPieceCounter++;
            }
            else if (targetType == white || targetType == fixityWhite)
            {
                CheckEmpty[CheckingNum,2] = enemyPieceCounter;
                break;
            }
            moveCount++;
        }
    }
    */

    private void CheckIfBlack(int CheckingNum,int x,int y)
    {
        int scoreCounter = 0;

		for (int i = -1; i < 2; i++)
        {
			for (int j = -1; j < 2; j++)
            {
				//検索方向変数
				int Direction_X = x + j;
				int Direction_Y = y + i;

				//置かれた駒のマスは検索しない
				if (i == 0 && j == 0)
                {
					continue;
                }
				//検索方向に相手プレイヤーの駒が存在しない場合、その方向の検索を終了させる
				if (Map.Instance.map[Direction_Y,Direction_X] != white)
                {
					continue;
                }
				//検索の距離を足していく
				for (int s = 2; s < 9; s++)
				{
					//検索マス関数
					int Range_X = x + j * s;
					int Range_Y = y + i * s;

					if (Range_X >= 0 && Range_X < 10 && Range_Y >= 0 && Range_Y < 9)
					{
						//相手の駒を発見したあとに空きに当たった場合、その方向の検索を終了させる
						if (Map.Instance.map[Range_Y, Range_X] == empty || Map.Instance.map[Range_Y, Range_X] == wall)
						{
							break; 
						}
						//相手の駒を発見したあとに同じ色の駒に当たった場合、そのマスにいたるまでのマスの駒をひっくり返す
						if (Map.Instance.map[Range_Y, Range_X] == black || Map.Instance.map[Range_Y, Range_X] == fixityBlack)
						{
							for (int n = 1; n < s; n++)
                            {
								int Change_X = x + j * n;
								int Change_Y = y + i * n;
								scoreCounter++;
								Debug.Log("count = " + scoreCounter);
                                CheckEmpty[CheckingNum,3] = scoreCounter;
							}
							break;
						}
					}
				}
            }
        }
    }

    private void CheckIfWhite(int CheckingNum,int x,int y)
    {
        int scoreCounter = 0;

		for (int i = -1; i < 2; i++)
        {
			for (int j = -1; j < 2; j++)
            {
				//検索方向変数
				int Direction_X = x + j;
				int Direction_Y = y + i;

				//置かれた駒のマスは検索しない
				if (i == 0 && j == 0)
                {
					continue;
                }
				//検索方向に相手プレイヤーの駒が存在しない場合、その方向の検索を終了させる
				if (Map.Instance.map[Direction_Y,Direction_X] != black)
                {
					continue;
                }
				//検索の距離を足していく
				for (int s = 2; s < 9; s++)
				{
					//検索マス関数
					int Range_X = x + j * s;
					int Range_Y = y + i * s;

					if (Range_X >= 0 && Range_X < 10 && Range_Y >= 0 && Range_Y < 9)
					{
						//相手の駒を発見したあとに空きに当たった場合、その方向の検索を終了させる
						if (Map.Instance.map[Range_Y, Range_X] == empty || Map.Instance.map[Range_Y, Range_X] == wall)
						{
							break; 
						}
						//相手の駒を発見したあとに同じ色の駒に当たった場合、そのマスにいたるまでのマスの駒をひっくり返す
						if (Map.Instance.map[Range_Y, Range_X] == white || Map.Instance.map[Range_Y, Range_X] == fixityWhite)
						{
							for (int n = 1; n < s; n++)
                            {
								int Change_X = x + j * n;
								int Change_Y = y + i * n;
								scoreCounter++;
								//Debug.Log("count = " + scoreCounter);
                                CheckEmpty[CheckingNum,2] = scoreCounter;
							}
							break;
						}
					}
				}
            }
        }
    }
}
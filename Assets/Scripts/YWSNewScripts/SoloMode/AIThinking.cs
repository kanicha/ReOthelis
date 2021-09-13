using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIThinking : MonoBehaviour
{
    private const byte _WIDTH = 10;
    private const byte _HEIGHT = 11;
    public string[,] thinkingMap = new string[_HEIGHT, _WIDTH]
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
        { "■", "〇", "●", "〇", "●", "〇", "●", "〇", "●", "■" },
        { "■", "■", "■", "■", "■", "■", "■", "■", "■", "■" }
    };
    public readonly string wall = "■";
    public readonly string empty = "□";
    public readonly string black = "●";
    public readonly string white = "〇";
    public readonly string fixityBlack = "★";
    public readonly string fixityWhite = "☆";
    private int[,] CheckEmpty = new int [15,5];　//データ保存用の配列
    private int CheckPosX = 0;
    private int CheckPosZ = 0;
    bool IsAboveExist = false;

    //どの座標が一番駒をひっくり返せるかを調べるためのマップを準備
    public void MapPrepare()
    {
        //マップデータをメインゲームのマップからコピー
        for (int a = 0; a < _HEIGHT; a++)
        {
            for (int b = 0; b < _WIDTH; b++)
            {
                thinkingMap[a,b] = Map.Instance.map[a,b];
            }
        }

        string printMapData = "";
		for (int i = 0; i < _HEIGHT; i++)
		{
			for (int j = 0; j < _WIDTH; j++)
			{
				printMapData += thinkingMap[i, j].ToString() + ",";
			}
			printMapData += "\n";
		}
		Debug.Log("CPU用マップ" + printMapData);
    }

    public void CheckVertical()
    {
        //座標パターンの番号
        int ItemNum = 0;
        //全ての縦行を検索
        for (int x = 1; x < 10; x++)
        {
            //縦行の一番下から検索を行う
            for (int z = 9; z > 1; z--)
            {
                //一番下にある空いてる座標をデータ保存用の配列に入れる
                if (thinkingMap[z, x] == empty && thinkingMap[z+1,x] != empty)
                {
                    //データ保存配列の見方：
                    //0 x座標1
                    //1 z座標1
                    //2 x座標2
                    //3 z座標2
                    //4 ひっくり返せる駒の数の合計

                    //前の座標で上に空きが存在した場合、新しい座標を二個目の座標として取得
                    if (IsAboveExist == true)
                    {
                        CheckEmpty[ItemNum,2] = x;
                        CheckEmpty[ItemNum,3] = z;
                        ItemNum++;
                    }
                    //前の座標で上に空きがなかった場合、新しい座標を前の行に入力
                    //1列目は最初なのでこの処理は行わない
                    else if (IsAboveExist == false && x != 1)
                    {
                        CheckEmpty[ItemNum-1,2] = x;
                        CheckEmpty[ItemNum-1,3] = z;
                        ItemNum++;
                    }
                    CheckEmpty[ItemNum,0] = x;
                    CheckEmpty[ItemNum,1] = z;
                    IsAboveExist = false;
                    //縦パターン
                    //取得した座標の一個上が空いていれば、それを二個目の座標として取得
                    if (thinkingMap[z-1,x] == empty)
                    {
                        CheckEmpty[ItemNum,2] = x;
                        CheckEmpty[ItemNum,3] = z-1;
                        ItemNum++;
                        //縦パターンが存在するため、横パターンを想定して次の行に座標を入力
                        //8列目の右は壁なので横パターンは存在しない
                        if (x < 8)
                        {
                            CheckEmpty[ItemNum,0] = x;
                            CheckEmpty[ItemNum,1] = z;
                            IsAboveExist = true;
                        }
                    }
                    break;
                }
            }
        }
    }

    //マップ確認用
    public void ShowData()
    {
        string printData = "";
		for (int i = 0; i < 15; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				printData += CheckEmpty[i, j].ToString() + ",";
			}
			printData += "\n";
		}
		Debug.Log(printData);
    }

    

    private void CheckBlackBlack()
    {

    }

    private void CheckWhiteWhite()
    {

    }

    private void CheckBlackWhite()
    {
        
    }

    private void CheckBlack(int CheckingNum,Vector3 dir)
    {
        // 調べたい方向
        int dirX = (int)dir.x;
        int dirZ = (int)dir.z;
        
        int enemyPieceCounter = 0;

        // dirの方向に「ひっくり返せるか」探索
        while(true)
        {
            // 調べたい方向に進んでいく
            CheckPosX += dirX;
            CheckPosZ += dirZ;
            string targetType = thinkingMap[CheckPosZ, CheckPosX];

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
                CheckEmpty[CheckingNum,3] += enemyPieceCounter;
                break;
            }
        }
    }

    private void CheckWhite(int CheckingNum,Vector3 dir)
    {
        // 調べたい方向
        int dirX = (int)dir.x;
        int dirZ = (int)dir.z;
        
        int enemyPieceCounter = 0;

        // dirの方向に「ひっくり返せるか」探索
        while(true)
        {
            // 調べたい方向に進んでいく
            CheckPosX += dirX;
            CheckPosZ += dirZ;
            string targetType = thinkingMap[CheckPosZ, CheckPosX];

            // 壁 or 空白なら終了
            if (targetType == wall || targetType == empty)
            {
                break;
            }
            else if (targetType == black)
            {
                enemyPieceCounter++;
            }
            else if (targetType == white || targetType == fixityWhite)
            {
                CheckEmpty[CheckingNum,2] += enemyPieceCounter;
                break;
            }
        }
    }
}
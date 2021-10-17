using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIThinking : MonoBehaviour
{
    private const byte _WIDTH = 10;
    private const byte _HEIGHT = 11;
    public string[,] MapData = new string[_HEIGHT, _WIDTH]
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
    public static int[,] CheckEmpty = new int [15,5];　//データ保存用の配列
    bool IsAboveExist = false;
    private int _setPosX = 0;
    private int _setPosZ = 0;

    //どの座標が一番駒をひっくり返せるかを調べるためのマップを準備
    public void MapPrepare()
    {
        //マップデータをメインゲームのマップからコピー
        for (int a = 0; a < _HEIGHT; a++)
        {
            for (int b = 0; b < _WIDTH; b++)
            {
                MapData[a,b] = Map.Instance.map[a,b];
            }
        }
        //マップ確認用
        string printMapData = "";
		for (int i = 0; i < _HEIGHT; i++)
		{
			for (int j = 0; j < _WIDTH; j++)
			{
				printMapData += MapData[i, j].ToString() + ",";
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
                if (MapData[z, x] == empty && MapData[z+1,x] != empty)
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
                    if (MapData[z-1,x] == empty)
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

        //取得した座標データの検索を行う
        PatternClassification();
        
        //データ確認用
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

    private void PatternClassification()
    {
        for (int i = 0; i < 15; i++)
        {
            //座標1
            _setPosX = CheckEmpty[i,0];
            _setPosZ = CheckEmpty[i,1];

            //全方向探索

            //座標2

            //全方向探索
        }
    }

    private void OmnidirectionalSearch(Vector3 dir)
    {
        //
        int _checkPosX = _setPosX;
        int _checkPosZ = _setPosZ;

        //
        int _dirX = (int) dir.x;
        int _dirZ = (int) dir.z;

        //
        while (true)
        {
            //
            _checkPosX += _dirX;
            _checkPosZ += _dirZ;
            string _targetType = MapData[_checkPosZ, _checkPosX];

            //

        }
    }
}
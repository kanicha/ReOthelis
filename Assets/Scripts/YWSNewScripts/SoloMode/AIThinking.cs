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
    public static int[,] CheckEmpty = new int [15,6];　//データ保存用の配列
    bool IsAboveExist = false;
    private int _setPosX = 0;
    private int _setPosZ = 0;
    public static string _myColor = string.Empty;
    public static string _enemyColor = string.Empty;
    public static string _fixityMyColor = string.Empty;
    public static string _fixityEnemyColor = string.Empty;

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
        CheckMap();
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
                    //5 

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
        
        //データ確認用
        CheckData();
    }

    //取得した座標データの検索を行う
    public void PatternClassification()
    {
        for (int i = 0; i < 15; i++)
        {
            //座標1
            _setPosX = CheckEmpty[i,0];
            _setPosZ = CheckEmpty[i,1];
            if (PiecePatternGeneretor.type == 1)
            {
                _myColor = black;
                _fixityMyColor = fixityBlack;
                _enemyColor = white;
                _fixityEnemyColor = fixityWhite;

                //該当座標にコマを置く
                MapData[CheckEmpty[i,1],CheckEmpty[i,0]] = black;
            }
            else if (PiecePatternGeneretor.type == 2 || PiecePatternGeneretor.type == 3)
            {
                _myColor = white;
                _fixityMyColor = fixityWhite;
                _enemyColor = black;
                _fixityEnemyColor = fixityBlack;

                //該当座標にコマを置く
                MapData[CheckEmpty[i,1],CheckEmpty[i,0]] = white;
            }
            //全方向探索
            CheckInTheDirection(new Vector3(0, 0, -1),i); //↑
            CheckInTheDirection(new Vector3(-1, 0, 0),i); // ←
            CheckInTheDirection(new Vector3(1, 0, 0),i); // →
            CheckInTheDirection(new Vector3(0, 0, 1),i); // ↓
            CheckInTheDirection(new Vector3(-1, 0, 1),i); // ↙
            CheckInTheDirection(new Vector3(1, 0, 1),i); // ↘
            CheckInTheDirection(new Vector3(-1, 0, -1),i); // ↖
            CheckInTheDirection(new Vector3(1, 0, -1),i); // ↗

            //マップ確認用
            CheckMap();
            //データ確認用
            CheckData();

            //座標2
            _setPosX = CheckEmpty[i,2];
            _setPosZ = CheckEmpty[i,3];
            if (PiecePatternGeneretor.type == 3)
            {
                _myColor = black;
                _fixityMyColor = fixityBlack;
                _enemyColor = white;
                _fixityEnemyColor = fixityWhite;

                //該当座標にコマを置く
                MapData[CheckEmpty[i,3],CheckEmpty[i,2]] = black;
            }
            else
            {
                MapData[CheckEmpty[i,3],CheckEmpty[i,2]] = _myColor;
            }
            //全方向探索
            CheckInTheDirection(new Vector3(0, 0, -1),i); //↑
            CheckInTheDirection(new Vector3(-1, 0, 0),i); // ←
            CheckInTheDirection(new Vector3(1, 0, 0),i); // →
            CheckInTheDirection(new Vector3(0, 0, 1),i); // ↓
            CheckInTheDirection(new Vector3(-1, 0, 1),i); // ↙
            CheckInTheDirection(new Vector3(1, 0, 1),i); // ↘
            CheckInTheDirection(new Vector3(-1, 0, -1),i); // ↖
            CheckInTheDirection(new Vector3(1, 0, -1),i); // ↗

            //マップ確認用
            CheckMap();
            //データ確認用
            CheckData();

            //探索後にコマを消去する
            MapData[CheckEmpty[i,1],CheckEmpty[i,0]] = empty;
            MapData[CheckEmpty[i,3],CheckEmpty[i,2]] = empty;

            //コマパターンが二色の時、もう一度検索を行う
            if (PiecePatternGeneretor.type == 3)
            {
                _setPosX = CheckEmpty[i,0];
                _setPosZ = CheckEmpty[i,1];
                _myColor = black;
                _fixityMyColor = fixityBlack;
                _enemyColor = white;
                _fixityEnemyColor = fixityWhite;

                //該当座標にコマを置く
                MapData[CheckEmpty[i,1],CheckEmpty[i,0]] = black;

                //全方向探索
                CheckInTheDirection(new Vector3(0, 0, -1),i); //↑
                CheckInTheDirection(new Vector3(-1, 0, 0),i); // ←
                CheckInTheDirection(new Vector3(1, 0, 0),i); // →
                CheckInTheDirection(new Vector3(0, 0, 1),i); // ↓
                CheckInTheDirection(new Vector3(-1, 0, 1),i); // ↙
                CheckInTheDirection(new Vector3(1, 0, 1),i); // ↘
                CheckInTheDirection(new Vector3(-1, 0, -1),i); // ↖
                CheckInTheDirection(new Vector3(1, 0, -1),i); // ↗

                _setPosX = CheckEmpty[i,2];
                _setPosZ = CheckEmpty[i,3];
                _myColor = white;
                _fixityMyColor = fixityWhite;
                _enemyColor = black;
                _fixityEnemyColor = fixityBlack;

                //該当座標にコマを置く
                MapData[CheckEmpty[i,3],CheckEmpty[i,2]] = white;

                //全方向探索
                CheckInTheDirection(new Vector3(0, 0, -1),i); //↑
                CheckInTheDirection(new Vector3(-1, 0, 0),i); // ←
                CheckInTheDirection(new Vector3(1, 0, 0),i); // →
                CheckInTheDirection(new Vector3(0, 0, 1),i); // ↓
                CheckInTheDirection(new Vector3(-1, 0, 1),i); // ↙
                CheckInTheDirection(new Vector3(1, 0, 1),i); // ↘
                CheckInTheDirection(new Vector3(-1, 0, -1),i); // ↖
                CheckInTheDirection(new Vector3(1, 0, -1),i); // ↗
            }

            //マップ確認用
            CheckMap();
            //データ確認用
            CheckData();

            //探索後にコマを消去する
            MapData[CheckEmpty[i,1],CheckEmpty[i,0]] = empty;
            MapData[CheckEmpty[i,3],CheckEmpty[i,2]] = empty;

            //マップ確認用
            CheckMap();
        }
    }

    private void CheckInTheDirection(Vector3 dir, int ItemNum)
    {
        // 調べる座標
        int checkPosX = _setPosX;
        int checkPosZ = _setPosZ;
        // 調べたい方向
        int dirX = (int) dir.x;
        int dirZ = (int) dir.z;

        bool isReverse = false;
        int moveCount = 0;

        // dirの方向に「ひっくり返せるか」探索
        while (true)
        {
            // 調べたい方向に進んでいく
            checkPosX += dirX;
            checkPosZ += dirZ;
            string targetType = MapData[checkPosZ, checkPosX];

            // 壁 or 空白なら終了
            if (targetType == wall || targetType == empty)
            {
                break;
            }
            else if (targetType == _myColor || targetType == _fixityMyColor)
            {
                isReverse = true; // 自分の色で挟んだ扱い
                CheckEmpty[ItemNum,4] += moveCount;
                break;
            }

            moveCount++;
        }

        // 裏返しが発生するなら処理(厳密にはmoveCountが0でも処理)
        if (isReverse)
        {
            checkPosX = _setPosX;
            checkPosZ = _setPosZ;

            // リバースするコマをリストに追加
            for (int i = 0; i < moveCount; i++)
            {
                checkPosX += dirX;
                checkPosZ += dirZ;

                if (MapData[checkPosZ, checkPosX] != _fixityEnemyColor)
                {
                    MapData[checkPosZ, checkPosX] = _myColor;
                    CheckEmpty[ItemNum,4] += 1;
                }
            }
        }
    }

    public List<int> SameReverseNum = new List<int>();
    public void PatternChoice()
    {
        int MaxReverseNum = 0;

        //ひっくり返せるコマの最大数をデータ配列から探す
        for (int i = 0; i < 15; i++)
        {
            if (CheckEmpty[i,4] > MaxReverseNum)
            {
                MaxReverseNum = CheckEmpty[i,4];
            }
        }

        //同じ数のコマをひっくり返せるパターンがあるかをデータ配列から探す
        for (int i = 0; i < 15; i++)
        {
            if (CheckEmpty[i,4] == MaxReverseNum)
            {
                SameReverseNum.Add(i);
            }
        }

        //データ確認用
        Debug.Log("最大数=" + MaxReverseNum);
        for(int i = 0; i < SameReverseNum.Count; i++)
        {
            Debug.Log(SameReverseNum[i]);
        }
    }

    public void MovePiece()
    {
        int P1X = (int) GameDirector.Instance._activePieces[0].transform.position.x;
        int P1Z = (int) GameDirector.Instance._activePieces[0].transform.position.z;
        int P2X = (int) GameDirector.Instance._activePieces[1].transform.position.x;
        int P2Z = (int) GameDirector.Instance._activePieces[1].transform.position.z;
        Debug.Log("p1x=" + P1X + "\np1z=" + P1Z + "\np2x=" + P2X + "\np2z" + P2Z);

        int choiceNum = Random.Range(0,SameReverseNum.Count);
        int patternNum = SameReverseNum[choiceNum];
        Vector3 P1FinalPos = new Vector3 (AIThinking.CheckEmpty[patternNum,0], 0, AIThinking.CheckEmpty[patternNum,1]*-1);
        Vector3 P2FinalPos = new Vector3 (AIThinking.CheckEmpty[patternNum,2], 0, AIThinking.CheckEmpty[patternNum,3]*-1);
        Debug.Log("1x=" + AIThinking.CheckEmpty[patternNum,0] + "\n1z=" + AIThinking.CheckEmpty[patternNum,1] + "\n2x" + AIThinking.CheckEmpty[patternNum,2] + "\n2z" + AIThinking.CheckEmpty[patternNum,3]);

        GameDirector.Instance._activePieces[0].transform.position = P1FinalPos;
        GameDirector.Instance._activePieces[1].transform.position = P2FinalPos;
        int FinalP1X = (int) GameDirector.Instance._activePieces[0].transform.position.x;
        int FinalP1Z = (int) GameDirector.Instance._activePieces[0].transform.position.z;
        int FinalP2X = (int) GameDirector.Instance._activePieces[1].transform.position.x;
        int FinalP2Z = (int) GameDirector.Instance._activePieces[1].transform.position.z;
        Debug.Log("Finalp1x=" + FinalP1X + "\nFinalp1z=" + FinalP1Z + "\nFinalp2x=" + FinalP2X + "\nFinalp2z" + FinalP2Z);
    }

    private void CheckData()
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

    public void CheckMap()
    {
        string printMapData = "";
		for (int i = 0; i < _HEIGHT; i++)
		{
			for (int j = 0; j < _WIDTH; j++)
			{
				printMapData += MapData[i, j].ToString() + ",";
			}
			printMapData += "\n";
		}
		Debug.Log("CPU用マップ\n" + printMapData);
    }
}
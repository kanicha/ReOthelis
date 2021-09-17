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
    // ひっくり返す処理
    private List<GameObject> _reversePiece = new List<GameObject>();// ひっくり返すコマを格納
    public  GameObject[,] pieceMap = new GameObject[_HEIGHT, _WIDTH];
    private int _setPosX = 0;
    private int _setPosZ = 0;
    private string type = string.Empty;
    private string _myColor = string.Empty;
    private string _enemyColor = string.Empty;
    private string _fixityMyColor = string.Empty;
    private string _fixityEnemyColor = string.Empty;
    private bool _isChecking = false;
    private const string _REVERSED_TAG = "Reversed";
    private bool _isSecondCheck = false;
    public Piece.PieceType turnPlayerColor = Piece.PieceType.none;
    public bool isSkillActivate = false;
    public string ignoreFixityPiece = string.Empty;// 指定した色の固定効果を無視する(基本は空文字)
    private int checkingNum = 0;

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
        for (int num = 0; num < 15; num++)
        {
            //黒黒
            if (PiecePatternGeneretor.type == 1)
            {
                type = black;
                MapData[CheckEmpty[num,0],CheckEmpty[num,1]] = black;
                MapData[CheckEmpty[num,2],CheckEmpty[num,3]] = black;
                //座標1
                StartCoroutine(CheckReverse(Map.Instance.pieceMap[CheckEmpty[num,0],CheckEmpty[num,1]]));
                //座標2
                StartCoroutine(CheckReverse(Map.Instance.pieceMap[CheckEmpty[num,2],CheckEmpty[num,3]]));
            }
            //白白
            else if (PiecePatternGeneretor.type == 2)
            {
                type = white;
                MapData[CheckEmpty[num,0],CheckEmpty[num,1]] = white;
                MapData[CheckEmpty[num,2],CheckEmpty[num,3]] = white;
                //座標1
                StartCoroutine(CheckReverse(Map.Instance.pieceMap[CheckEmpty[num,0],CheckEmpty[num,1]]));
                //座標2
                StartCoroutine(CheckReverse(Map.Instance.pieceMap[CheckEmpty[num,2],CheckEmpty[num,3]]));
            }
            //黒白
            else if (PiecePatternGeneretor.type == 3)
            {
                
            }
        }
    }

    public IEnumerator CheckReverse(GameObject piece)
    {
        while (_isChecking)
            yield return null;// 2つのコルーチンは片方づつ処理する

        // このターンに置いたコマがリバースしている or このコマが盤面外に置かれているなら
        //if (piece.CompareTag(_REVERSED_TAG))
        //{
            //_isSecondCheck = false;
            //yield break;
        //}

        _isChecking = true;

        // 自分の色と相手の色を決定
        if (type == black)
        {
            _myColor = black;
            _fixityMyColor = fixityBlack;
            _enemyColor = white;
            _fixityEnemyColor = fixityWhite;
        }
        else
        {
            _myColor = white;
            _fixityMyColor = fixityWhite;
            _enemyColor = black;
            _fixityEnemyColor = fixityBlack;
        }

        // 置いたマスの座標を取得
        _setPosX = (int)piece.transform.position.x;
        _setPosZ = (int)piece.transform.position.z * -1;

        // 7方向にチェック(zは符号が逆転する)
        CheckInTheDirection(new Vector3(-1, 0, 0));  // ←
        CheckInTheDirection(new Vector3(1, 0, 0));   // →
        CheckInTheDirection(new Vector3(0, 0, 1));   // ↓
        //CheckInTheDirection(new Vector3(0, 0, -1));// ↑
        CheckInTheDirection(new Vector3(-1, 0, 1));  // ↙
        CheckInTheDirection(new Vector3(1, 0, 1));   // ↘
        CheckInTheDirection(new Vector3(-1, 0, -1)); // ↖
        CheckInTheDirection(new Vector3(1, 0, -1));  // ↗

        StartCoroutine(PieceReverse());
        ignoreFixityPiece = string.Empty;// スキル効果は1ターンで終了
    }

    private void CheckInTheDirection(Vector3 dir)
    {
        // 調べる座標
        int checkPosX = _setPosX;
        int checkPosZ = _setPosZ;
        // 調べたい方向
        int dirX = (int)dir.x;
        int dirZ = (int)dir.z;

        bool isReverse = false;
        int moveCount = 0;

        // dirの方向に「ひっくり返せるか」探索
        while(true)
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
                isReverse = true;// 自分の色で挟んだ扱い
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
            for(int i = 0; i < moveCount; i++)
            {
                checkPosX += dirX;
                checkPosZ += dirZ;

                // 相手の駒が固定コマなら
                if (MapData[checkPosZ, checkPosX] == _fixityEnemyColor)
                {
                    // 固定効果を無視するスキル効果
                    if (MapData[checkPosZ, checkPosX] == ignoreFixityPiece)
                    {
                        pieceMap[checkPosZ, checkPosX].GetComponent<Piece>().ChangeIsFixity();
                        MapData[checkPosZ, checkPosX] = _myColor;
                        _reversePiece.Add(pieceMap[checkPosZ, checkPosX]);
                        pieceMap[checkPosZ, checkPosX].tag = _REVERSED_TAG;
                    }
                    // スキルが発動していなければスルー
                }
                else// 相手色が確定しているので
                {
                    MapData[checkPosZ, checkPosX] = _myColor;// ←の都合で探索を分割しなければならない
                    _reversePiece.Add(pieceMap[checkPosZ, checkPosX]);
                    pieceMap[checkPosZ, checkPosX].tag = _REVERSED_TAG;
                }
            }
        }
    }

    private IEnumerator PieceReverse()
    {
        foreach (GameObject piece in _reversePiece)
        {
            CheckEmpty[checkingNum,4] += 1;
            piece.GetComponent<Piece>().Reverse();
            yield return new WaitForSeconds(0.3f);
        }
        checkingNum++;

        _reversePiece.Clear();

        // スキル効果なら準備時間に戻る
        if (isSkillActivate)
        {
            isSkillActivate = false;
            _isSecondCheck = false;
        }
        if(_isSecondCheck)// 2回目のチェックならステートを進める
        {
            _isSecondCheck = false;
        }
        else
            _isSecondCheck = true;// 2回目のチェックに入る

        _isChecking = false;
    }
}
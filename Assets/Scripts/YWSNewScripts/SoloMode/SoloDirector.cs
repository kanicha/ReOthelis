using UnityEngine;
using UnityEngine.UI;

public class SoloDirector : SingletonMonoBehaviour<GameDirector>
{
    [SerializeField, Header("基本スコア")]
    public int point = 0;
    [SerializeField, Header("接地中に配置を確定するまでの時間")]
    private float _marginTime = 0;
    [SerializeField, Header("事前に操作できる時間")]
    private float _preActiveTime = 0;
    [SerializeField, Header("ミノの初期位置")]
    public Vector3 _DEFAULT_POSITION = Vector3.zero;
    [SerializeField]
    PiecePatternGeneretor _generator = null;
    [SerializeField]
    private Player_1 _player1 = null;
    [SerializeField]
    private Player_2 _player2 = null;
    [SerializeField, Header("エフェクトコントローラー")] 
    private EffectController _effectController = null;
    [SerializeField]
    private AI_DataBase _ai = null;

    private int _turnCount = 0;
    private float _timeCount = 0;
    private bool _isDown = true;
    public GameObject[] _activePieces = new GameObject[2];
    public float intervalTime = 0;
    public bool _isLanding = false;
    public bool _isSkillBlack = false;
    public bool _isSkillWhite = false;
    public GameState gameState = GameState.none;
    public GameState nextStateCue = GameState.none;

    public enum GameState
    {
        none,
        preActive,
        active,
        confirmed,
        falled,
        interval,
        reversed,
        idle,
        end,
        ended,
    }

    void Start()
    {
        SoundManager.Instance.PlayBGM(0);

        _player1.isMyTurn = false;
        _player2.isMyTurn = false;

        // 最初は2セット生成
        PieceSet();
        ChangeTurn();
    }

    void Update()
    {
        Map.Instance.CheckMap();

        switch (gameState)
        {
            case GameState.preActive:
                _effectController.FallPieceHighLight(true, _activePieces[0]);

                _isLanding = false;
                _isDown = true;
                _timeCount += Time.deltaTime;

                // 待機時間を超えたらステートをすすめる(自動落下の処理はPieceMove()で管理)
                if (_timeCount > _preActiveTime)
                {
                    // 時間経過によりコマを一個下げる
                    _activePieces[0].transform.position += new Vector3(0, 0, -1);
                    _activePieces[1].transform.position += new Vector3(0, 0, -1);

                    // さげたら推移
                    intervalTime = 0;
                    nextStateCue = GameState.active;
                    gameState = GameState.interval;
                }
                break;

            case GameState.active:
                _effectController.FallPieceHighLight(true, _activePieces[0]);

                if (ModeSelect._selectCount == 0 && _player2.isMyTurn == true)
                {
                    _ai.MovePiece();
                }
                if (Map.Instance.CheckLanding(_activePieces[0].transform.position) || Map.Instance.CheckLanding(_activePieces[1].transform.position))
                {
                    // 接地時にカウント
                    _timeCount += Time.deltaTime;
                    if (_timeCount > _marginTime)
                    {
                        _timeCount = 0;
                        gameState = GameState.confirmed;
                    }
                }
                else
                    _timeCount = 0;
                break;

            case GameState.confirmed:
                _effectController.FallPieceHighLight(false, _activePieces[0]);
                if (_activePieces[0].transform.position.z > _activePieces[1].transform.position.z)
                {
                    // 下側のコマがインデックス0になるようソート
                    GameObject tempPiece = _activePieces[0];
                    _activePieces[0] = _activePieces[1];
                    _activePieces[1] = tempPiece;
                }

                Map.Instance.FallPiece(_activePieces[0]);
                Map.Instance.FallPiece(_activePieces[1]);
                _isLanding = true;
                gameState = GameState.falled;
                break;

            case GameState.falled:
                SoundManager.Instance.PlaySE(3);
                CheckPriority();
                Map.Instance.TagClear();

                gameState = GameState.idle;
                // リバース・アニメーション処理
                for (int i = 0; i < _activePieces.Length; i++)
                {
                    if (Map.Instance.CheckHeightOver(_activePieces[i], false))
                        StartCoroutine(Map.Instance.CheckReverse(_activePieces[i], false));
                }

                // スキルフラグ初期化
                _isSkillBlack = false;
                _isSkillWhite = false;
                break;

            case GameState.interval:// 強引スキル連打でバグが出るので時間を取る(応急処置)
                _timeCount += Time.deltaTime;
                if (_timeCount > intervalTime)
                {
                    gameState = nextStateCue;
                    _timeCount = 0;
                }
                break;

            case GameState.reversed:
                // ゲーム終了判定
                if (Map.Instance.CheckEnd())
                    gameState = GameState.end;
                else
                {
                    PieceSet();
                    ChangeTurn();
                }
                break;

            case GameState.end:
                if (_player1.reverseScore > _player2.reverseScore)
                    Debug.Log("<color=red>1Pの勝ち</color>");
                else if (_player1.reverseScore == _player2.reverseScore)
                    Debug.Log("<color=orange>引き分け</color>");
                else
                    Debug.Log("<color=blue>2Pの勝ち</color>");
                SoundManager.Instance.StopBGM();
                gameState = GameState.ended;
                break;

            default:
                break;
        }
    }

    private void CheckPriority()
    {
        // ターンプレイヤーの色を判定
        Piece.PieceType playersType;

        if (_player1.isMyTurn)
            playersType = Piece.PieceType.black;
        else
            playersType = Piece.PieceType.white;

        Map.Instance.turnPlayerColor = playersType;

        // どちらのコマからひっくり返すか判定
        GameObject tempPiece = _activePieces[0];
        Piece piece1 = _activePieces[0].GetComponent<Piece>();
        Piece piece2 = _activePieces[1].GetComponent<Piece>();

        // [色を比較]  どちらも自分の色 or どちらも相手の色ならポジションで判断する
        if ((piece1.pieceType == playersType && piece2.pieceType == playersType) || (piece1.pieceType != playersType && piece2.pieceType != playersType))
        {
            // [0]が上ならソート
            if ((int)_activePieces[0].transform.position.z > (int)_activePieces[1].transform.position.z)
            {
                _activePieces[0] = _activePieces[1];
                _activePieces[1] = tempPiece;
            }
            // [0]が右ならソート
            else if (_activePieces[0].transform.position.x > _activePieces[1].transform.position.x)
            {
                _activePieces[0] = _activePieces[1];
                _activePieces[1] = tempPiece;
            }
        }
        else if (piece2.pieceType == playersType)
        {
            _activePieces[0] = _activePieces[1];
            _activePieces[1] = tempPiece;
        }
    }

    private void ChangeTurn()
    {
        _turnCount++;

        _player1.isMyTurn = false;
        _player2.isMyTurn = false;

        // 黒ターン
        if (_turnCount % 2 == 1)
        {
            _player1.rotationNum = 0;
            _player1.controllPiece1 = _activePieces[0];
            _player1.controllPiece2 = _activePieces[1];
            _player1.isMyTurn = true;
        }
        else// 白ターン
        {
            _player2.rotationNum = 0;
            _player2.controllPiece1 = _activePieces[0];
            _player2.controllPiece2 = _activePieces[1];
            _player2.isMyTurn = true;
            _ai.MapPrepare();
            _ai.CheckVertical();
            _ai.PatternClassification();
            _ai.CheckMap();
            _ai.PatternChoice();
        }
        gameState = GameState.preActive;
    }

    private void PieceSet()
    {
        // 生成位置の1マス下が空いていれば生成
        Vector3 generatePos = _DEFAULT_POSITION + Vector3.back;
        int x = 0;
        while (true)
        {
            Vector3 checkPos = generatePos + new Vector3(x, 0);
            if (Map.Instance.CheckWall(checkPos))
            {
                _generator.Generate(checkPos + Vector3.forward);
                break;
            }
            else
            {
                checkPos = generatePos + new Vector3(x * -1, 0);
                if (Map.Instance.CheckWall(checkPos))
                {
                    _generator.Generate(checkPos + Vector3.forward);
                    break;
                }
            }
            x++;
            if (x > 4)
                Debug.LogError("生成できるマスがありません");
        }
    }

    public void AddScore(bool isBlack, int point)
    {
        if (isBlack)
        {
            _player1.reverseScore += point;
        }
        else
        {
            _player2.reverseScore += point;
        }
    }

    public void AddPreScore(bool isBlack, int point)
    {
        if (isBlack)
        {
            _player1.preScore += point;
        }
        else
        {
            _player2.preScore += point;
        }
    }

    public void AddReversedCount(bool isBlack)
    {
        if (isBlack)
            _player1.reversedCount++;
        else
            _player2.reversedCount++;
    }

    public void AddPieceCount(int blackCount, int whiteCount)
    {
        _player1.myPieceCount = blackCount;
        _player2.myPieceCount = whiteCount;
    }
}
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : SingletonMonoBehaviour<GameDirector>
{
    [SerializeField, Header("初期コマの配列")] private Piece[] _pieceArray = new Piece[8];
    [SerializeField, Header("基本スコア")] public int point = 0;

    [SerializeField, Header("接地中に配置を確定するまでの時間")]
    private float _marginTime = 0;

    [SerializeField, Header("事前に操作できる時間")] private float _preActiveTime = 0;
    [SerializeField, Header("ミノの初期位置")] public Vector3 _DEFAULT_POSITION = Vector3.zero;
    [SerializeField] PiecePatternGeneretor _generator = null;
    [SerializeField] private Player_1 _player1 = null;
    [SerializeField] private Player_2 _player2 = null;

    // プレイヤーのゲッタ
    public Player_1 player1 => _player1;
    public Player_2 player2 => _player2;

    // ピース用リストを宣言
    public List<Piece> pieces = new List<Piece>();

    [SerializeField, Header("エフェクトコントローラー")]
    private EffectController _effectController = null;

    private int _turnCount = 0;
    private float _timeCount = 0;
    private bool _canChangeTurn = false;
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

        if (ServerManager._isConnect)
        {
            ServerManager.Instance._onReceived.ObserveOnMainThread().Subscribe(onReceive).AddTo(this);
            // 自分のターンの時ゲームステートが変更加えられたときに相手に通知する、Interval, falledは無視
            this.ObserveEveryValueChanged(x => x.gameState).Where(_ => _player1.isMyTurn)
                .Where(state => state != GameState.interval).Where(state => state != GameState.falled).Subscribe(OnStateChangedInMyTurn).AddTo(this);
            this.ObserveEveryValueChanged(x => x.gameState).Subscribe(OnStateChanged).AddTo(this);

        }

        // ネットにつながっていない時 かつ つながっていても2Pのときはスルーを行う
        if (!(ServerManager._isConnect &&
              ServerManager.Instance.myPlayerNumber == ServerManager.playerNumber.twoPlayer))
        {
            if (ServerManager._isConnect)
            {
                ChangeTurn();
                PieceSet();
                _player1.controllPiece1 = _activePieces[0];
                _player1.controllPiece2 = _activePieces[1];
            }
            else
            {
                PieceSet();
                ChangeTurn();
            }

            gameState = GameState.preActive;
        }

        // ネットに繋がっている時 かつ 1Pの時
        if (ServerManager._isConnect &&
            ServerManager.Instance.myPlayerNumber == ServerManager.playerNumber.onePlayer)
        {
            // InitPieceArrayIdを_pieceArrayの最大値分初期化
            string[] initPieceArrayId = new string[_pieceArray.Length];

            for (var i = 0; i < _pieceArray.Length; i++)
            {
                initPieceArrayId[i] = _pieceArray[i]._pieceId;
                pieces.Add(_pieceArray[i]);
            }

            // Requestの生成
            InitPieceRequest initPieceRequest = new InitPieceRequest(initPieceArrayId);
            // 送信
            ServerManager.Instance.SendMessage(initPieceRequest);
        }
    }

    void Update()
    {
        Map.Instance.CheckMap();

        switch (gameState)
        {
            case GameState.preActive:
                _effectController.FallPieceHighLight(true, _activePieces[0]);

                PrePieceGauge.Instance.SetGaugeRatio(_timeCount, _preActiveTime);
                PrePieceGauge.Instance.SetPosition(_activePieces[0].transform.position, _activePieces[1].transform.position);

                // インターネットに接続されている かつ 自分のターンではない時
                if (ServerManager._isConnect && !_player1.isMyTurn)
                {
                    // 処理を行いたくないためbreak
                    break;
                }

                _isLanding = false;
                _isDown = true;
                _timeCount += Time.deltaTime;
                
                // 待機時間を超えたらステートをすすめる(自動落下の処理はPieceMove()で管理)
                if (_timeCount > _preActiveTime)
                {
                    // 時間経過によりコマを一個下げる
                    _activePieces[0].transform.position += new Vector3(0, 0, -1);
                    _activePieces[1].transform.position += new Vector3(0, 0, -1);

                    if (ServerManager._isConnect)
                    {
                        // Requestの作成
                        PieceMoveRequest pieceMoveRequest =
                            new PieceMoveRequest(_activePieces[0], _activePieces[1]);

                        ServerManager.Instance.SendMessage(pieceMoveRequest);
                    }

                    // さげたら推移
                    intervalTime = 0;
                    nextStateCue = GameState.active;
                    gameState = GameState.interval;
                    PrePieceGauge.Instance.Deactivate();
                }

                break;

            case GameState.active:
                _effectController.FallPieceHighLight(true, _activePieces[0]);

                // インターネットに接続されている かつ 自分のターンではない時
                if (ServerManager._isConnect && !_player1.isMyTurn)
                {
                    // 処理を行いたくないためbreak
                    break;
                }

                if (Map.Instance.CheckLanding(_activePieces[0].transform.position) ||
                    Map.Instance.CheckLanding(_activePieces[1].transform.position))
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

            case GameState.interval: // 強引スキル連打でバグが出るので時間を取る(応急処置)
                _timeCount += Time.deltaTime;
                if (_timeCount > intervalTime)
                {
                    gameState = nextStateCue;
                    _timeCount = 0;
                }

                break;

            case GameState.reversed:

                if (ServerManager._isConnect && !_canChangeTurn)
                {
                    break;
                }
                
                // ゲーム終了判定
                if (Map.Instance.CheckEnd())
                    gameState = GameState.end;
                else
                {
                    // インターネットに接続されてない時 or 接続されていてプレイヤー1じゃない時
                    /*
                    if (!ServerManager._isConnect || ServerManager._isConnect && !_player1.isMyTurn)
                        PieceSet();
                    ChangeTurn();
                    // サーバーに接続されている時
                    if (ServerManager._isConnect)
                    {
                        _player1.controllPiece1 = _activePieces[0];
                        _player1.controllPiece2 = _activePieces[1];
                    }
                    */
                    if (ServerManager._isConnect)
                    {
                        ChangeTurn();
                        if (_player1.isMyTurn)
                        {
                            PieceSet();
                        }
                       
                        _player1.controllPiece1 = _activePieces[0];
                        _player1.controllPiece2 = _activePieces[1];
                    }
                    else
                    {
                        PieceSet();
                        ChangeTurn();
                    }
                    gameState = GameState.preActive;
                    _canChangeTurn = false;
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
        Debug.LogWarning(_activePieces[0]);
        Piece piece1 = _activePieces[0].GetComponent<Piece>();
        Piece piece2 = _activePieces[1].GetComponent<Piece>();

        // [色を比較]  どちらも自分の色 or どちらも相手の色ならポジションで判断する
        if ((piece1.pieceType == playersType && piece2.pieceType == playersType) ||
            (piece1.pieceType != playersType && piece2.pieceType != playersType))
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
        else // 白ターン
        {
            _player2.rotationNum = 0;
            _player2.controllPiece1 = _activePieces[0];
            _player2.controllPiece2 = _activePieces[1];
            _player2.isMyTurn = true;
        }

        Debug.LogWarning("turnChange" + _turnCount);
    }

    /// <summary>
    /// プレイヤーが1pなのか2pなのか判別関数
    /// </summary>
    /// <param name="player">Playerの情報変数</param>
    /// <returns></returns>
    public bool PlayerJudge(PlayerBase player)
    {
        if (player == _player1)
            return true;
        else
            return false;
    }

    private void PieceSet()
    {
        // 生成位置の1マス下が空いていれば生成
        Vector3 generatePos = _DEFAULT_POSITION + Vector3.back;
        int x = 0;

        GameObject piece1 = null;
        GameObject piece2 = null;

        while (true)
        {
            Vector3 checkPos = generatePos + new Vector3(x, 0);
            if (Map.Instance.CheckWall(checkPos))
            {
                _generator.Generate(checkPos + Vector3.forward, out piece1, out piece2);
                break;
            }
            else
            {
                checkPos = generatePos + new Vector3(x * -1, 0);
                if (Map.Instance.CheckWall(checkPos))
                {
                    _generator.Generate(checkPos + Vector3.forward, out piece1, out piece2);
                    break;
                }
            }

            x++;
            if (x > 4)
                Debug.LogError("生成できるマスがありません");
        }

        // インターネットに接続されている時
        if (ServerManager._isConnect)
        {
            // インスタンス化と配列の作成
            Piece p = piece1.GetComponent<Piece>();
            Piece p2 = piece2.GetComponent<Piece>();
            PieceInfo[] pieceInfos = new PieceInfo[2]
            {
                new PieceInfo(p._myVector3, p.pieceType, p._pieceId),
                new PieceInfo(p2._myVector3, p2.pieceType, p2._pieceId)
            };

            // Requestの作成と送信
            PieceMoveRequest pieceMoveRequest = new PieceMoveRequest(pieceInfos);
            // 生成したら通知するように変数をtrueにする
            pieceMoveRequest.isCreated = true;
            ServerManager.Instance.SendMessage(pieceMoveRequest);
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

    /// <summary>
    /// 通信を受け取ったら処理関数
    /// </summary>
    /// <param name="req">通信内容</param>
    private void onReceive(object req)
    {
        RequestBase.PacketType packetType = ServerManager.Instance.ParsePacketType(req);

        switch (packetType)
        {
            case RequestBase.PacketType.PieceMoved:
                PieceMoveRequest pieceMoveRequest = (PieceMoveRequest)req;

                for (var i = 0; i < pieceMoveRequest.pieceObjArray.Length; i++)
                {
                    PieceInfo pieceInfo = pieceMoveRequest.pieceObjArray[i];
                    Piece savePiece = null;
                    // 移動したコマのidを探す
                    foreach (var piece in pieces)
                    {
                        // すでにあるコマのIDと移動したコマのIDを比較して代入
                        if (piece._pieceId.Equals(pieceInfo.pieceId))
                        {
                            savePiece = piece;

                            break;
                        }
                    }

                    Piece.PieceType pieceType = (Piece.PieceType)pieceInfo.pieceColor;
                    GameObject pieceObject = null;
                    
                    // 探したコマがなかった場合は生成を行う
                    if (savePiece == null)
                    {
                        pieceObject = _generator.Generate(pieceInfo.piecePos.ToVector3(), pieceType,
                            pieceInfo.pieceId);

                        savePiece = pieceObject.GetComponent<Piece>();
                        pieces.Add(savePiece);
                    }
                    else
                    {
                        // 実際にあった場合は代入をおこなう
                        pieceObject = savePiece.gameObject;
                        savePiece.transform.position = pieceInfo.piecePos.ToVector3();
                        savePiece.pieceType = pieceType;
                    }
                    
                    // コマの色の同期
                    savePiece.ApplyPieceType();
                    
                    // 相手のコマが生成されたら
                    if (pieceMoveRequest.isCreated)
                    {
                        // 同期を行う
                        _activePieces[i] = pieceObject;

                        Debug.LogWarning("indexNum:" + i);
                        Debug.LogWarning("Object:" + pieceObject);
                    }
                }

                break;

            case RequestBase.PacketType.InitPiece:
                InitPieceRequest initPieceRequest = (InitPieceRequest)req;

                for (var i = 0; i < initPieceRequest._initPieceIdArray.Length; i++)
                {
                    // idを代入
                    _pieceArray[i]._pieceId = initPieceRequest._initPieceIdArray[i];
                }

                break;

            case RequestBase.PacketType.StateChange:
                StateChangeRequest stateChangeRequest = (StateChangeRequest)req;
                gameState = (GameState)stateChangeRequest.gameState;

                // ゲームステートがfalledの時にMapに書き込みを行う
                /*if (gameState == GameState.falled)
                {
                    foreach (GameObject activePiece in _activePieces)
                    {
                        Piece p = activePiece.GetComponent<Piece>();

                        int x = (int)activePiece.transform.position.x;
                        int z = (int)activePiece.transform.position.z * -1; // zはマイナス方向に進むので符号を反転させる

                        // Mapの更新処理
                        Map.Instance.map[z, x] = Map.Instance._mapElement(p.pieceType);
                        Map.Instance.pieceMap[z, x] = activePiece;
                    }
                }*/

                break;
            
            case RequestBase.PacketType.TurnChangeable:
                _canChangeTurn = true;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 自分のターンにステート変更された時の処理関数
    /// </summary>
    /// <param name="gameState">ゲームステート</param>
    private void OnStateChangedInMyTurn(GameState gameState)
    {
        StateChangeRequest stateChangeRequest = new StateChangeRequest(gameState);
        ServerManager.Instance.SendMessage(stateChangeRequest);
        
        switch (gameState)
        {
            case GameState.active:
                PrePieceGauge.Instance.Deactivate();
                break;
        }
    }

    /// <summary>
    /// ステートの変更が行われた時の処理関数
    /// </summary>
    /// <param name="gameState">ゲームステート</param>
    private void OnStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.reversed:
                TurnChangeableRequest turnChangeableRequest = new TurnChangeableRequest();
                ServerManager.Instance.SendMessage(turnChangeableRequest);
                break;
        }
    }
}

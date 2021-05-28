using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OthelloSystem : MonoBehaviour
{
    const int Filed_Size_x = 8;
    const int Filed_Size_z = 8;

    // ブロック状態
    public enum CharacterState
    {
        None,
        Face,
        Back,

        Max
    }

    // ボードの実体
    private GameObject _boardObject = null;

    // ブロックの実体
    private GameObject[,] _filedCharacterObject = new GameObject[Filed_Size_z, Filed_Size_x];
    private PieceController[,] _fielfCharacters = new PieceController[Filed_Size_z, Filed_Size_x];

    // 最終的なブロックの状態
    private CharacterState[,] _fieldBlocksStateFinal = new CharacterState[Filed_Size_z, Filed_Size_x];

    // カーソルの実体
    private GameObject _cursorObject = null;

    [SerializeField] GameObject _characterPrefab = null;
    [SerializeField] GameObject _boardPrefab = null;
    [SerializeField] GameObject _cursorPrefab = null;

    // カーソル制御用
    private int _cursorX = 0;
    private int _cursorZ = 0;

    // ターン制御(黒からスタート)
    private CharacterState _turn = CharacterState.Face;

    // ひっくり返す対象
    class Position
    {
        public int _x;
        public int _z;
        public Position(int x, int z)
        {
            _x = x;
            _z = z;
        }
    }
    // ひっくり返し処理の方向
    int[] TURN_CHECK_X = new int[] { -1, -1, 0, 1, 1, 1, 0, -1 };
    int[] TURN_CHECK_Z = new int[] { 0, 1, 1, 1, 0, -1, -1, -1 };

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Filed_Size_z; i++)
        {
            for(int j = 0; j < Filed_Size_x; j++)
            {
                // ブロックの実体
                GameObject newObject = GameObject.Instantiate<GameObject>(_characterPrefab);
                PieceController newCharacter = newObject.GetComponent<PieceController>();
                newObject.transform.localPosition = new Vector3(-(Filed_Size_x - 1) * 0.5f + j, 0.5f, -(Filed_Size_z - 1) * 0.5f + i);
                _filedCharacterObject[i, j] = newObject;
                _fielfCharacters[i, j] = newCharacter;
                // ブロックの状態
                _fieldBlocksStateFinal[i, j] = CharacterState.None;
            }
            _fieldBlocksStateFinal[3, 3] = CharacterState.Face;
            _fieldBlocksStateFinal[4, 3] = CharacterState.Back;
            _fieldBlocksStateFinal[3, 4] = CharacterState.Back;
            _fieldBlocksStateFinal[4, 4] = CharacterState.Face;
        }
        // ボードの生成
        _boardObject = GameObject.Instantiate<GameObject>(_boardPrefab);

        // カーソルの生成
        _cursorObject = GameObject.Instantiate<GameObject>(_cursorPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        // カーソルの移動
        int deltaX = 0;
        int deltaZ = 0;
        if(GetKeyEx(KeyCode.W))
        {
            deltaZ += 1;
        }
        if (GetKeyEx(KeyCode.S))
        {
            deltaZ -= 1;
        }
        if (GetKeyEx(KeyCode.A))
        {
            deltaX -= 1;
        }
        if (GetKeyEx(KeyCode.D))
        {
            deltaX += 1;
        }
        _cursorX += deltaX;
        _cursorZ += deltaZ;
        _cursorObject.transform.localPosition =
            new Vector3(-(Filed_Size_x - 1) * 0.5f + _cursorX, 0.0f, -(Filed_Size_z - 1) * 0.5f + _cursorZ);

        // Enterキーを押すと駒が出てくる
        if (GetKeyEx(KeyCode.Return))
        {
            if(0 <= _cursorX && _cursorX < Filed_Size_x && 0 <= _cursorZ && _cursorZ < Filed_Size_z &&
                 _fieldBlocksStateFinal[_cursorZ, _cursorX] == CharacterState.None && Turn(false) > 0)
            {
                _fieldBlocksStateFinal[_cursorZ, _cursorX] = _turn;
                // 交互になるよう
                Turn(true);
                _turn = ((_turn == CharacterState.Face) ? CharacterState.Back : CharacterState.Face);
            }
        }

        // ブロックの状態を更新
        UpdateCharacterState();
    }

    // 駒をひっくり返す処理
    int Turn(bool isTurn)
    {
        // 相手の色
        CharacterState enemyColor = ((_turn == CharacterState.Face) ? CharacterState.Back : CharacterState.Face);

        // ひっくり返す数
        bool isValidTurn = false; // ひっくり返せるかどうか
        List<Position> positionList = new List<Position>();
        int count = 0;

        // 左
        int deltaX = 0, deltaZ = 0;
        for(int i = 0; i < TURN_CHECK_X.Length; i++)
        {
            int x = _cursorX;
            int z = _cursorZ;
            deltaX = TURN_CHECK_X[i];
            deltaZ = TURN_CHECK_Z[i];
            isValidTurn = false;
            positionList.Clear();
            while (true)
            {
                x += deltaX;
                z += deltaZ;
                if (!(0 <= x && x < Filed_Size_x && 0 <= z && z < Filed_Size_z))
                {
                    // 範囲外
                    break;
                }
                if (_fieldBlocksStateFinal[z, x] == enemyColor)
                {
                    // ひっくり返す対象
                    positionList.Add(new Position(x, z));
                }
                else if (_fieldBlocksStateFinal[z, x] == _turn)
                {
                    // ひっくり返す
                    isValidTurn = true;
                    break;
                }
                else
                {
                    // 何もなし
                    break;
                }
            }
            // 実際のひっくり返し処理
            if (isValidTurn)
            {
                count += positionList.Count;
                if(isTurn)
                {
                    for (int j = 0; j < positionList.Count; j++)
                    {
                        Position pos = positionList[j];
                        _fieldBlocksStateFinal[pos._z, pos._x] = _turn;
                    }
                }
            }
        }

        return count;
    }

    void UpdateCharacterState()
    {
        // ブロックの状態反映 (フィールド上)
        for (int i = 0; i < Filed_Size_z; i++)
        {
            for (int j = 0; j < Filed_Size_x; j++)
            {
                // ブロックの状態
                _fielfCharacters[i, j].SetState(_fieldBlocksStateFinal[i, j]);
            }
        }
    }

    // キー入力
    private Dictionary<KeyCode, int> _keyImputTimer = new Dictionary<KeyCode, int>();
    private bool GetKeyEx(KeyCode keycode)
    { 
        if(!_keyImputTimer.ContainsKey(keycode))
        {
            _keyImputTimer.Add(keycode, -1);
        }
        if(Input.GetKey(keycode))
        {
            _keyImputTimer[keycode]++;
        }
        else
        {
            _keyImputTimer[keycode] = -1;
        }
        return (_keyImputTimer[keycode] == 0 || _keyImputTimer[keycode] >= 10);
    }
}

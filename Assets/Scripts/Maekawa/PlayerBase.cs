using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBase : MonoBehaviour
{
    // �L�[�l�[��
    protected string DS4_circle_name = "";
    protected string DS4_cross_name = "";
    protected string DS4_square_name = "";
    protected string DS4_triangle_name = "";
    protected string DS4_L1_name = "";
    protected string DS4_R1_name = "";
    protected string DS4_option_name = "";
    protected string DS4_horizontal_name = "";
    protected string DS4_vertical_name = "";
    protected string DS4_Lstick_horizontal_name = "";
    protected string DS4_Lstick_vertical_name = "";
    protected string DS4_Rstick_horizontal_name = "";
    protected string DS4_Rstick_vertical_name = "";
    // �L�[�{�[�h����p�L�[�l�[��
    protected string key_board_horizontal_name = "";
    protected string key_board_vertical_name = "";
    // �L�[�o�����[
    protected bool _DS4_circle_value = false;
    protected bool _DS4_cross_value = false;
    protected bool _DS4_square_value = false;
    protected bool _DS4_triangle_value = false;
    protected bool _DS4_L1_value = false;
    protected bool _DS4_R1_value = false;
    protected bool _DS4_option_value = false;
    protected float _DS4_horizontal_value = 0.0f;
    protected float _DS4_vertical_value = 0.0f;
    protected float _DS4_Lstick_horizontal_value = 0.0f;
    protected float _DS4_Lstick_vertical_value = 0.0f;
    protected float _DS4_Rstick_horizontal_value = 0.0f;
    protected float _DS4_Rstick_vertical_value = 0.0f;
    // �O�t���[���̃L�[�o�����[
    protected float last_horizontal_value = 0.0f;
    protected float last_vertical_value = 0.0f;
    protected float lastLstick_horizontal_value = 0.0f;
    protected float last_Lstick_vertical_value = 0.0f;
    protected float last_Rstick_horizontal_value = 0.0f;
    protected float last_Rstick_vertical_value = 0.0f;
    // �L�[�{�[�h�p
    //private float _keyBoardHorizontal = 0.0f;
    //private float _keyBoardVertical = 0.0f;
    private bool _keyBoardLeft = false;
    private bool _keyBoardRight = false;

    //
    [SerializeField, Header("1�}�X�������鎞��")]
    private float _fallTime = 0.0f;
    [SerializeField]
    protected Text scoreText = null;
    [SerializeField]
    protected Text reversedCountText = null;
    [SerializeField]
    protected Text myPieceCountText = null;
    [SerializeField]
    protected Image charactorImage = null;
    private float _timeCount = 0.0f;
    public bool isMyTurn = false;
    public bool isPreurn = false;
    public int score = 0;
    public int reversedCount = 0;
    protected const int MAX_REVERSE_COUNT = 20;
    public int myPieceCount = 0;
    public GameObject controllPiece1 = null;
    public GameObject controllPiece2 = null;
    public int rotationNum = 0;
    //
    private const int _SKILL_1_COST = 3;
    private const int _SKILL_2_COST = 5;
    private const int _SKILL_3_COST = 10;
    protected Piece.PieceType playerType = Piece.PieceType.none;
    protected string myColor = "";
    protected string enemyColor = "";

    protected delegate void Skill_1(int cost);
    protected delegate void Skill_2(int cost);
    protected delegate void Skill_3(int cost);
    protected Skill_1 skill_1;
    protected Skill_2 skill_2;
    protected Skill_3 skill_3;

    protected readonly Vector3[] rotationPos = new Vector3[]
    {
        new Vector3(0,  0, 1),
        new Vector3(-1, 0, 0),
        new Vector3(0,  0, -1),
        new Vector3(1,  0, 0)
    };

    protected void KeyInput()
    {
        _DS4_circle_value = Input.GetButtonDown(DS4_circle_name);
        _DS4_cross_value = Input.GetButtonDown(DS4_cross_name);
        _DS4_square_value = Input.GetButtonDown(DS4_square_name);
        _DS4_triangle_value = Input.GetButtonDown(DS4_triangle_name);
        _DS4_L1_value = Input.GetButtonDown(DS4_L1_name);
        _DS4_R1_value = Input.GetButtonDown(DS4_R1_name);
        _DS4_option_value = Input.GetButtonDown(DS4_option_name);
        _DS4_horizontal_value = Input.GetAxis(DS4_horizontal_name);
        _DS4_vertical_value = Input.GetAxis(DS4_vertical_name);
        _DS4_Lstick_horizontal_value = Input.GetAxis(DS4_Lstick_horizontal_name);
        _DS4_Lstick_vertical_value = Input.GetAxis(DS4_Lstick_vertical_name);
        _DS4_Rstick_horizontal_value = Input.GetAxis(DS4_Rstick_horizontal_name);
        _DS4_Rstick_vertical_value = Input.GetAxis(DS4_Rstick_vertical_name);

        if (0 != Input.GetAxis(key_board_horizontal_name))
            _DS4_horizontal_value = Input.GetAxis(key_board_horizontal_name);
        if (0 != Input.GetAxis(key_board_vertical_name))
            _DS4_vertical_value = Input.GetAxis(key_board_vertical_name);
        _keyBoardLeft = Input.GetKeyDown(KeyCode.Q);
        _keyBoardRight = Input.GetKeyDown(KeyCode.E);
    }

    /// <summary>
    /// �O�t���[���̓��͂�ۑ�
    /// </summary>
    protected void SaveKeyValue()
    {
        last_horizontal_value = _DS4_horizontal_value;
        last_vertical_value = _DS4_vertical_value;
        lastLstick_horizontal_value = _DS4_Lstick_horizontal_value;
        last_Lstick_vertical_value = _DS4_Lstick_vertical_value;
        last_Rstick_horizontal_value = _DS4_Rstick_horizontal_value;
        last_Rstick_vertical_value = _DS4_Rstick_vertical_value;
    }

    protected void InputSkill()
    {
        // �X�L��1...�~ �X�L��2...�� �X�L��3...��
        if(Input.GetKeyDown(KeyCode.Z) || _DS4_cross_value)
        {
            skill_1(_SKILL_1_COST);
        }
        else if(Input.GetKeyDown(KeyCode.X) || _DS4_triangle_value)
        {
            skill_2(_SKILL_2_COST);
        }
        //else if(Input.GetKeyDown(KeyCode.C) || _DS4_square_value)
        //{
        //    skill_3(_SKILL_3_COST);
        //}
    }

    protected void PieceMove()
    {
        Vector3 move = Vector3.zero;
        bool isDown = false;

        _timeCount += Time.deltaTime;

        // �ړ�
        if ((_DS4_horizontal_value < 0 && last_horizontal_value == 0) || (_DS4_Lstick_horizontal_value < 0 && lastLstick_horizontal_value == 0))
            move.x = -1;
        else if ((_DS4_horizontal_value > 0 && last_horizontal_value == 0) || (_DS4_Lstick_horizontal_value > 0 && lastLstick_horizontal_value == 0))
            move.x = 1;
        else if ((_DS4_vertical_value < 0 && last_vertical_value == 0) || (_DS4_Lstick_vertical_value < 0 && last_Lstick_vertical_value == 0))
        {
            isDown = true;
            move.z = -1;
        }
        else if (_timeCount >= _fallTime)// ���ԗ���
        {
            _timeCount = 0;
            move.z = -1;
        }

        // �ړ���̍��W���v�Z
        Vector3 movedPos = controllPiece1.transform.position + move;
        Vector3 rotMovedPos = movedPos + rotationPos[rotationNum];

        // �ړ���̍��W�ɏ�Q�����Ȃ����
        if (Map.Instance.CheckWall(movedPos) && Map.Instance.CheckWall(rotMovedPos))
        {
            controllPiece1.transform.position = movedPos;
            controllPiece2.transform.position = rotMovedPos;
        }
        else if (isDown)
            GameDirector.Instance.gameState = GameDirector.GameState.confirmed;// �����͂����A��Q��������Ȃ�m��
    }

    protected void PieceRotate()
    {
        int lastNum = rotationNum;

        if (_DS4_L1_value || _keyBoardLeft)
            rotationNum++;// ����]
        else if (_DS4_R1_value || _keyBoardRight)
            rotationNum += 3;// �E��](=����3��])

        rotationNum %= 4;
        Vector3 rotatedPos = controllPiece1.transform.position + rotationPos[rotationNum];
        Vector3 rotatedUnderPos = rotatedPos + Vector3.back;


        if (Map.Instance.CheckWall(rotatedPos))
        {
            if ((int)rotatedPos.z == -1 && !Map.Instance.CheckWall(rotatedUnderPos))
                rotationNum = lastNum;  
            else
            {
                if (rotationNum != lastNum)
                    SoundManager.Instance.PlaySE(2);
                controllPiece2.transform.position = rotatedPos;
            }
        }
        else
            rotationNum = lastNum;
    }

    protected void PrePieceMove()
    {
        Vector3 move = Vector3.zero;

        // ���E�ړ�
        if ((_DS4_horizontal_value < 0 && last_horizontal_value == 0) || (_DS4_Lstick_horizontal_value < 0 && lastLstick_horizontal_value == 0))
            move.x = -1;
        else if ((_DS4_horizontal_value > 0 && last_horizontal_value == 0) || (_DS4_Lstick_horizontal_value > 0 && lastLstick_horizontal_value == 0))
            move.x = 1;

        // ���E�ɓ��͂����Ȃ�ړ�
        if (move != Vector3.zero)
        {
            Vector3 movedPos = controllPiece1.transform.position;
            while (true)
            {
                movedPos += move;
                Vector3 movedUnderPos = movedPos + Vector3.back;
                Vector3 rotMovedPos = movedUnderPos + rotationPos[rotationNum];

                // �ǂ܂ōs������X���[
                if ((int)movedPos.x < 1 || (int)movedPos.x > 8)
                    break;

                // �ړ���̍��W��1���ɏ�Q�����Ȃ����
                if (Map.Instance.CheckWall(movedUnderPos) && Map.Instance.CheckWall(rotMovedPos))
                {
                    controllPiece1.transform.position = movedPos;
                    controllPiece2.transform.position = movedPos + rotationPos[rotationNum];
                    break;
                }
            }
        }

        // �����͂�����{����J�n
        if ((_DS4_vertical_value < 0 && last_vertical_value == 0) || (_DS4_Lstick_vertical_value < 0 && last_Lstick_vertical_value == 0))
        {
            GameDirector.Instance.intervalTime = 0;
            GameDirector.Instance.nextStateCue = GameDirector.GameState.active;
            GameDirector.Instance.gameState = GameDirector.GameState.interval;
            controllPiece1.transform.position += Vector3.back;
            controllPiece2.transform.position += Vector3.back;
        }
    }


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void SetSkills(int charaType)
    {
        // �����Ӗ���enum��1P��2P��2����̂�enum��int��enum�ɃL���X�g 
        CharaImageMoved.CharaType1P type = (CharaImageMoved.CharaType1P)charaType;

        switch (type)
        {
            case CharaImageMoved.CharaType1P.Cow:
                skill_1 = Cancellation;
                skill_2 = MyPieceLock;
                break;
            case CharaImageMoved.CharaType1P.Mouse:
                skill_1 = RandomLock;
                skill_2 = Cancellation;
                break;
            case CharaImageMoved.CharaType1P.Rabbit:
                skill_1 = TakeAway;
                skill_2 = RandomLock;
                break;
            case CharaImageMoved.CharaType1P.Tiger:
                skill_1 = MyPieceLock;
                skill_2 = TakeAway;
                break;
            default:
                break;
        }
    }

    private bool CheckColor(string type)
    { 
        bool isThere = false;
        // ����s���������R�}���u�����\���̂���}�X��T��
        for (int i = 2; i < 9; i++)
            for (int j = 1; j < 9; j++)
            {
                if (Map.Instance.map[i, j] == type)
                    isThere = true;
            }

        return isThere;
    }

    private bool ActivateCheck(GameDirector.GameState targetState, int cost)
    {
        if (GameDirector.Instance.gameState == targetState && reversedCount >= cost)
            return true;
        else
            return false;
    }

    //  ����
    public void TakeAway(int cost)
    {
        if (!ActivateCheck(GameDirector.GameState.preActive, cost))
            return;

        // �ŉ��i�������}�b�v�ɑ���̐F������Ȃ�(�Œ�R�}�͑ΏۊO)
        if (CheckColor(enemyColor))
        {
            Debug.Log("����");
            reversedCount -= cost;
            while (true)
            {
                // �K���Ƀ����_���ȍ��W���Ƃ�A���ꂪ�����̐F�Ȃ�ϊ�
                int z = Random.Range(2, 9);
                int x = Random.Range(1, 9);
                if (Map.Instance.map[z, x] == enemyColor)
                {
                    Map.Instance.map[z, x] = myColor;
                    Map.Instance.pieceMap[z, x].GetComponent<Piece>().SkillReverse();
                    // �����A���o�[�X�������s��
                    Map.Instance.TagClear();
                    Map.Instance.isSkillActivate = true;
                    GameDirector.Instance.gameState = GameDirector.GameState.idle;
                    StartCoroutine(Map.Instance.CheckReverse(Map.Instance.pieceMap[z, x]));
                    break;
                }
            }
        }
        else
            Debug.Log("����̐F�̃R�}������܂���");
    }

    // �Œ�
    public void RandomLock(int cost)
    {
        if (!ActivateCheck(GameDirector.GameState.preActive, cost) && !ActivateCheck(GameDirector.GameState.active, cost))
            return;

        // �ŉ��i�������}�b�v�Ɏ����̐F������Ȃ�(�Œ�R�}�͑ΏۊO)
        if (CheckColor(myColor))
        {
            Debug.Log("�Œ�");
            reversedCount -= cost;

            string type;
            if (myColor == Map.Instance.black)
                type = Map.Instance.fixityBlack;
            else
                type = Map.Instance.fixityWhite;

            while (true)
            {
                // �K���Ƀ����_���ȍ��W���Ƃ�A���ꂪ�����̐F�Ȃ�Œ�R�}�ɕϊ�
                int z = Random.Range(2, 9);
                int x = Random.Range(1, 9);
                if (Map.Instance.map[z, x] == myColor)
                {
                    Map.Instance.map[z, x] = type;
                    Map.Instance.pieceMap[z, x].GetComponent<Piece>().ChangeIsFixity();
                    break;
                }
            }
        }
        else
            Debug.Log("�����̐F�̃R�}������܂���");
    }

    // �c�e
    public void MyPieceLock(int cost)
    {
        if (!ActivateCheck(GameDirector.GameState.preActive, cost) && !ActivateCheck(GameDirector.GameState.active, cost))
            return;

        Piece piece1 = controllPiece1.GetComponent<Piece>();
        Piece piece2 = controllPiece2.GetComponent<Piece>();

        // �����̐F������Ώ���
        if (piece1.pieceType == playerType || piece2.pieceType == playerType)
        {
            Debug.Log("�c�e");
            reversedCount -= cost;
            // �����̐F���Œ艻(2�Ƃ������̐F�Ȃ痼��)
            if (piece1.pieceType == playerType)
                piece1.ChangeIsFixity();
            if (piece2.pieceType == playerType)
                piece2.ChangeIsFixity();
        }
        else
            Debug.Log("�����̐F�̃R�}�𑀍삵�Ă��܂���");
    }

    // �ŏ���
    public void Cancellation(int cost)
    {
        // �����̐F�̃R�}�𑀍삵�Ă��Ȃ��Ă������ł���(�Ӗ��͂Ȃ�)�̂ŗv���k

        if (!ActivateCheck(GameDirector.GameState.preActive, cost))
            return;

        // ����F�̌Œ�R�}��T��
        string targetColor;
        if (myColor == Map.Instance.black)
            targetColor = Map.Instance.fixityWhite;
        else
            targetColor = Map.Instance.fixityBlack;

        if (CheckColor(targetColor))
        {
            Debug.Log("�ŏ���");
            reversedCount -= cost;
            Map.Instance.ignoreFixityPiece = targetColor;// ����̌Œ�R�}���Ђ�����Ԃ���悤�ɂȂ�
        }
        else
            Debug.Log("����F�̌Œ�R�}������܂���");
    }
}
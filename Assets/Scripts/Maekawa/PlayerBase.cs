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
    protected string myColor = "";

    protected delegate void Skill_1();
    protected delegate void Skill_2();
    protected delegate void Skill_3();
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

    protected void SkillActivate()
    {
        // �X�L��1...�~ �X�L��2...�� �X�L��3...��
        if(Input.GetKeyDown(KeyCode.Z) || _DS4_cross_value)
        {
            Debug.Log("skill_1");
            skill_1();
        }
        else if(Input.GetKeyDown(KeyCode.X) || _DS4_triangle_value)
        {
            Debug.Log("skill_2");
            skill_2();
        }
        else if(Input.GetKeyDown(KeyCode.C) || _DS4_square_value)
        {
            Debug.Log("skill_3");
            skill_3();
        }
    }

    protected void PieceMove()
    {
        Vector3 move = Vector3.zero;
        bool isDown = false;

        _timeCount += Time.deltaTime;

        // ���E�ړ�
        if ((_DS4_horizontal_value < 0 && last_horizontal_value == 0) || (_DS4_Lstick_horizontal_value < 0 && lastLstick_horizontal_value == 0))
            move.x = -1;
        else if ((_DS4_horizontal_value > 0 && last_horizontal_value == 0) || (_DS4_Lstick_horizontal_value > 0 && lastLstick_horizontal_value == 0))
            move.x = 1;

        // ���ړ�
        if ((_DS4_vertical_value < 0 && last_vertical_value == 0) || (_DS4_Lstick_vertical_value < 0 && last_Lstick_vertical_value == 0))
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
        // ����]
        if (_DS4_L1_value || _keyBoardLeft)
        {
            rotationNum++;
            SoundManager.Instance.PlaySE(2);
        }
        // �E��](=����3��])
        else if (_DS4_R1_value || _keyBoardRight)
        {
            rotationNum += 3;
            SoundManager.Instance.PlaySE(2);
        }

        // �^����](�ړ�����₱�����Ȃ�̂�Rotation�͂�����Ȃ�)
        rotationNum %= 4;
        Vector3 rotatedPos = controllPiece1.transform.position + rotationPos[rotationNum];

        if (Map.Instance.CheckWall(rotatedPos))
            controllPiece2.transform.position = rotatedPos;
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
            GameDirector.Instance.gameState = GameDirector.GameState.active;
    }


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private bool CheckColor(bool isMycolor)
    {
        string targetColor;
        
        // �����̐F
        if (isMycolor)
            targetColor = myColor;
        else// ����̐F
        {
            if (myColor == Map.Instance.black)
                targetColor = Map.Instance.white;
            else
                targetColor = Map.Instance.black;
        }

        bool isThere = false;
        // ����s���������R�}���u�����\���̂���}�X��T��
        for (int i = 2; i < 9; i++)
            for (int j = 1; j < 9; j++)
            {
                if (Map.Instance.map[i, j] == targetColor)
                    isThere = true;
            }

        return isThere;
    }

    //  ����
    public void Forcibly()
    {
        // �ŉ��i�������}�b�v�ɑ���̐F���Ȃ���΃��^�[��(�Œ�R�}�͑ΏۊO)
        if (!CheckColor(false))
        {
            Debug.Log("����̐F������܂���");
            return;
        }

        if (GameDirector.Instance.gameState == GameDirector.GameState.preActive)
            GameDirector.Instance.gameState = GameDirector.GameState.idle;
        else
            return;

        // ����̐F������
        string enemyColor;
        if (myColor == Map.Instance.black)
            enemyColor = Map.Instance.white;
        else
            enemyColor = Map.Instance.black;

        while (true)
        {
            // �K���Ƀ����_���ȍ��W���Ƃ�A���ꂪ�����̐F�Ȃ�ϊ�
            int z = Random.Range(2, 9);
            int x = Random.Range(1, 9);
            if (Map.Instance.map[z, x] == enemyColor)
            {
                Map.Instance.map[z, x] = myColor;
                Map.Instance.pieceMap[z, x].GetComponent<Piece>().SkillReverse();
                Map.Instance.isSkillActivate = true;
                // �����A���o�[�X�������s��
                Map.Instance.TagClear();
                StartCoroutine(Map.Instance.CheckReverse(Map.Instance.pieceMap[z, x]));
                break;
            }
        }
    }

    // �Œ�
    public void Lock()
    {
        // �ŉ��i�������}�b�v�Ɏ����̐F���Ȃ���΃��^�[��(�Œ�R�}�͑ΏۊO)
        if (!CheckColor(true))
            return;

        GameDirector.Instance.gameState = GameDirector.GameState.idle;
        while (true)
        {
            // �K���Ƀ����_���ȍ��W���Ƃ�A���ꂪ�����̐F�Ȃ�ϊ�
            int z = Random.Range(2, 9);
            int x = Random.Range(1, 9);
            if (Map.Instance.map[z, x] == myColor)
            {
                Map.Instance.pieceMap[z, x].GetComponent<Piece>().pieceType = Piece.PieceType.fixity;
                if (myColor == Map.Instance.black)
                    Map.Instance.map[z, x] = Map.Instance.fixed_black;
                else
                    Map.Instance.map[z, x] = Map.Instance.fixed_black;
                break;
            }
        }

        GameDirector.Instance.gameState = GameDirector.GameState.active;
    }

    // �c�e
    public void BeLeftShadow()
    {
        Piece.PieceType playerType;
        if (myColor == Map.Instance.black)
            playerType = Piece.PieceType.black;
        else
            playerType = Piece.PieceType.white;

        // �����̐F������ΌŒ�R�}��
        if (controllPiece1.GetComponent<Piece>().pieceType == playerType)
            controllPiece1.GetComponent<Piece>().pieceType = Piece.PieceType.fixity;
        if (controllPiece2.GetComponent<Piece>().pieceType == playerType)
            controllPiece2.GetComponent<Piece>().pieceType = Piece.PieceType.fixity;
    }
}
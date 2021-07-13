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
    public Image charactorImage = null;
    private float _timeCount = 0.0f;
    public bool isMyTurn = false;
    public bool isPreurn = false;
    public int score = 0;
    public int reversedCount = 0;
    public int myPieceCount = 0;
    public GameObject controllPiece1 = null;
    public GameObject controllPiece2 = null;
    public int rotationNum = 0;

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public static bool isMyTurn = false;
    public float _horizontal = 0.0f;
    public float _vertical = 0.0f;
    public float _stickHorizontal = 0.0f;
    public float _stickVertical = 0.0f;
    public bool _ds4L1;
    public bool _ds4R1;
    public bool _ds4circle;
    public bool _ds4cross;
    
    public float _keyBoardHorizontal = 0.0f;
    public float _keyBoardVertical = 0.0f;
    public bool _keyBoardLeft;
    public bool _keyBoardRight;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // ボタン処理変数
        _horizontal = Input.GetAxis(DS4ControllerP1.DS4_HORIZONTAL);
        _vertical = Input.GetAxis(DS4ControllerP1.DS4_VERTICAL);
        _stickHorizontal = Input.GetAxis(DS4ControllerP1.DS4L_STICK_HORIZONTAL);
        _stickVertical = Input.GetAxis(DS4ControllerP1.DS4L_STICK_VERTICAL);
        _ds4L1 = Input.GetButtonDown(DS4ControllerP1.DS4_L1);
        _ds4R1 = Input.GetButtonDown(DS4ControllerP1.DS4_R1);
        _ds4circle = Input.GetButtonDown(DS4ControllerP1.DS4_O);
        _ds4cross = Input.GetButtonDown(DS4ControllerP1.DS4_X);
        
        // キーボード処理変数
        _keyBoardHorizontal = Input.GetAxis("Horizontal");
        _keyBoardVertical = Input.GetAxis("Vertical");
        _keyBoardLeft = Input.GetKeyDown(KeyCode.Q);
        _keyBoardRight = Input.GetKeyDown(KeyCode.E);
    }
}
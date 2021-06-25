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

    // Start is called before the first frame update
    void Start()
    {
        isMyTurn = false;
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxis(DS4ControllerP1.DS4_HORIZONTAL);
        _vertical = Input.GetAxis(DS4ControllerP1.DS4_VERTICAL);
        _stickHorizontal = Input.GetAxis(DS4ControllerP1.DS4L_STICK_HORIZONTAL);
        _stickVertical = Input.GetAxis(DS4ControllerP1.DS4L_STICK_VERTICAL);
        _ds4L1 = Input.GetButtonDown(DS4ControllerP1.DS4_L1);
        _ds4R1 = Input.GetButtonDown(DS4ControllerP1.DS4_R1);
    }
}
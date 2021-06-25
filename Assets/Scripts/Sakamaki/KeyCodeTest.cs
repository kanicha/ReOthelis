using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCodeTest : MonoBehaviour
{
    float _horizontal = 0;
    float _vertical = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxisRaw(DS4ControllerP1.DS4_HORIZONTAL);
        _vertical = Input.GetAxisRaw(DS4ControllerP1.DS4_VERTICAL);

        if (_horizontal < 0)
            Debug.Log("Left");
        else if (_horizontal > 0)
            Debug.Log("Right");
        else if (_vertical < 0)
            Debug.Log("back");
    }
}

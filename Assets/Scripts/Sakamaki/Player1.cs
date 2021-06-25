using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(DS4ControllerP1.DS4_O))
        {
            Debug.Log("P1Pushed !");
        }
    }
}
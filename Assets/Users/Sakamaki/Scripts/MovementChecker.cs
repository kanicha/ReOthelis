using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementChecker : MonoBehaviour
{
    private GridManager hoge;

    private void Start()
    {
        hoge = GetComponent<GridManager>();
    }
}

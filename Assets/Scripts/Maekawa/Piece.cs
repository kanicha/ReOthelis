using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public enum PieceType
    {
        none,
        black,
        white,
        joker
    }

    public PieceType pieceType = PieceType.none;
    public int rotationNum = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

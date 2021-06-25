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

    public void Reverse()
    {
        if(pieceType == PieceType.black)
        {
            pieceType = PieceType.white;
            this.name = "wihte";
            this.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            pieceType = PieceType.black;
            this.name = "black";
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceDisplay : MonoBehaviour
{
    public Text P1PieceText = null;
    public Text P2PieceText = null;

    // Start is called before the first frame update
    void Start()
    {
        P1PieceText.text = Player_1.displayPieceAmount.ToString();
        P2PieceText.text = Player_2.displayPieceAmount.ToString();
    }
}

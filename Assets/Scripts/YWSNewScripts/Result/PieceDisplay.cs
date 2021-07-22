using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceDisplay : MonoBehaviour
{
    //private Player_1 _player1;
    //private Player_2 _player2;
    public Text P1PieceText = null;
    public Text P2PieceText = null;

    // Start is called before the first frame update
    void Start()
    {
        //_player1 = FindObjectOfType<Player_1>();
        P1PieceText.text = Player_1.displayPieceAmount.ToString();
        //_player2 = FindObjectOfType<Player_2>();
        P2PieceText.text = Player_2.displayPieceAmount.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // マッチングのインスタンス化
        MatchingRequest matchingRequest = new MatchingRequest();
        
        // マッチング開始をサーバーに通知
        ServerManager.Instance.SendMessage(matchingRequest);
    }
}

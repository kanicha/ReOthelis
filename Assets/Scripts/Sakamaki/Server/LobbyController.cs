using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
    private GameSceneManager _gameSceneManager;
    
    // Start is called before the first frame update
    void Start()
    {
        ServerManager.Instance.InitServer();
        _gameSceneManager = FindObjectOfType<GameSceneManager>();
        // マッチングのインスタンス化
        MatchingRequest matchingRequest = new MatchingRequest();
        
        // マッチング開始をサーバーに通知
        ServerManager.Instance.SendMessage(matchingRequest);
        ServerManager.Instance._onReceived.ObserveOnMainThread().Subscribe(OnReceived).AddTo(this);
    }

    /// <summary>
    /// 受信したときに行う処理関数
    /// </summary>
    private void OnReceived(object req)
    {
        RequestBase.PacketType packetType = ServerManager.Instance.ParsePacketType(req);

        switch (packetType)
        {
            case RequestBase.PacketType.Matching:
                // requestBase -> MatchingRequestにキャスト
                MatchingRequest matchingRequest = (MatchingRequest)req;

                if (matchingRequest.isFull)
                {
                    // 満員時の処理
                    Debug.LogWarning("接続先のサーバーが満員でした");
                }
                else if (matchingRequest.isJoined)
                {
                    // 参加したときの処理
                    Debug.LogWarning("接続しました。");
                }
                else if (matchingRequest.isCompleteMatching)
                {
                    // 2人ともマッチが完了したときの処理
                    Debug.LogWarning("マッチが完了しました");
                    
                    // キャラ選択画面のシーンに遷移
                    SceneChange(_gameSceneManager);
                }
                break;
            default:
                break;
        }
    }

    private void SceneChange(GameSceneManager gameSceneManager)
    {
        gameSceneManager.SceneNextCall("CharacterSelect");
    }
}

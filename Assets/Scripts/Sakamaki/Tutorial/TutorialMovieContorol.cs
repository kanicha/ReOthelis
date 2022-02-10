using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TutorialMovieContorol : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private VideoClip[] _controllerMovies = new VideoClip[4];

    // キー入力のenum
    public enum movies
    {
        none,
        ArrowInput,
        RotateInput,
        SpInput
    }
    
    public void videoPlayerContol(movies movies)
    {
        // 値に応じて代入
        switch (movies)
        {
            case movies.none:
                _videoPlayer.clip = _controllerMovies[0];
                break;
            case movies.ArrowInput:
                _videoPlayer.clip = _controllerMovies[1];
                break;
            case movies.RotateInput:
                _videoPlayer.clip = _controllerMovies[2];
                break;
            case movies.SpInput:
                _videoPlayer.clip = _controllerMovies[3];
                break;
            default:
                _videoPlayer.clip = null;
                break;
        }
    }
}

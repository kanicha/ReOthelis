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
        CircleInput,
        LeftArrowInput,
        RightArrowInput,
        LInput,
        RInput,
        OptionInput,
        SquareInput
    }
    
    public void videoPlayerContol(movies movies)
    {
        // 値に応じて代入
        switch (movies)
        {
            case movies.none:
                _videoPlayer.Stop();
                _videoPlayer.clip = null;
                break;
            case movies.CircleInput:
                _videoPlayer.clip = _controllerMovies[1];
                break;
            case movies.LeftArrowInput:
                _videoPlayer.clip = _controllerMovies[2];
                break;
            case movies.RightArrowInput:
                _videoPlayer.clip = _controllerMovies[3];
                break;
            case movies.LInput:
                _videoPlayer.clip = _controllerMovies[4];
                break;
            case movies.RInput:
                _videoPlayer.clip = _controllerMovies[5];
                break;
            case movies.OptionInput:
                _videoPlayer.clip = _controllerMovies[6];
                break;
            case movies.SquareInput:
                _videoPlayer.clip = _controllerMovies[7];
                break;
            default:
                break;
        }
    }
}

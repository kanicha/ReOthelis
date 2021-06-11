using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipManager : MonoBehaviour
{
    public enum ChipFrontBack
    {
        Front = 0,
        Back = 1
    }

    public enum ChipDirection
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    private Dictionary<ChipDirection, Vector3> _addVector3s = new Dictionary<ChipDirection, Vector3>()
                                                              {
                                                                  {ChipDirection.Up, new Vector3(0,0.5f,0)},
                                                                  {ChipDirection.Right, new Vector3(0.5f,0,0)},
                                                                  {ChipDirection.Down, new Vector3(0,-0.5f,0)},
                                                                  {ChipDirection.Left, new Vector3(-0.5f,0,0)}
                                                              };

    private ChipFrontBack _chipFrontBack = ChipFrontBack.Front;
    public  ChipFrontBack ChipFB { get { return _chipFrontBack; } }
    private ChipDirection _chipDirection = ChipDirection.Up;
    public  ChipDirection ChipDir { get { return _chipDirection; } }

    public void Init(ChipFrontBack chipFrontBack, ChipDirection dir)
    {
        _chipFrontBack = chipFrontBack;
        _chipDirection = dir;
        float addAngle = (ChipFrontBack.Front == _chipFrontBack) ? -90f : 90f; 
        transform.localRotation = Quaternion.Euler(addAngle, 0, 0);
    }
    
    public void TurnChip(Animator animator)
    {
        animator.SetBool("Turn", ChipFrontBack.Back == _chipFrontBack);
        if (ChipFrontBack.Back == _chipFrontBack)
            _chipFrontBack = ChipFrontBack.Front;
        else
            _chipFrontBack = ChipFrontBack.Back;
    }

    public void SetPosition(Vector3 pos)
    {
        transform.localPosition = pos + _addVector3s[_chipDirection];
    }
}

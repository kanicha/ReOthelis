using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipView : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    public Animator Animator { get { return _animator; } }
}

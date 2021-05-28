using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipPresenter : MonoBehaviour
{
    private ChipManager _chipManager;
    private ChipView    _chipView;

    private void Awake()
    {
        _chipManager = GetComponent<ChipManager>();
        _chipView    = GetComponent<ChipView>();
    }

    public void Init(ChipManager.ChipFrontBack chipFrontBack, ChipManager.ChipDirection dir)
    {
        _chipManager.Init(chipFrontBack, dir);
    }

    public void Turn()
    {
        _chipManager.TurnChip(_chipView.Animator);
    }

    public void SetPosition(Vector3 pos)
    {
        _chipManager.SetPosition(pos);
    }
}

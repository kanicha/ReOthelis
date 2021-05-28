using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleGamePresenter : MonoBehaviour
{
    private SampleSceneManager _sampleSceneManager;

    private SampleGameView _sampleGameView;

    private void Awake()
    {
        _sampleSceneManager = GetComponent<SampleSceneManager>();
        _sampleGameView     = GetComponent<SampleGameView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _sampleSceneManager.SetNewPanelChip(_sampleGameView.PanelFiledPresenter);
        }

        if (Input.GetMouseButtonDown(1))
        {
            _sampleSceneManager.TurnChip(_sampleGameView.PanelFiledPresenter);
        }
    }
}

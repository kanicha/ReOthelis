using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleGameView : MonoBehaviour
{
    [SerializeField]
    private PanelFiledPresenter _panelFiledPresenter;
    public PanelFiledPresenter PanelFiledPresenter { get { return _panelFiledPresenter; } }
}

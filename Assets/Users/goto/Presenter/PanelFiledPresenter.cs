using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelFiledPresenter : MonoBehaviour
{
    private PanelFiledManager _panelFiledManager;

    private PanelFieldView _panelFieldView;
    
    // Start is called before the first frame update
    void Start()
    {
        _panelFiledManager = GetComponent<PanelFiledManager>();
        _panelFieldView    = GetComponent<PanelFieldView>();
    }

    public void SetNewPanelChip()
    {
        _panelFiledManager.SetPanelChip();
    }

    public void TurnChip()
    {
        _panelFiledManager.TurnChip();
    }
}

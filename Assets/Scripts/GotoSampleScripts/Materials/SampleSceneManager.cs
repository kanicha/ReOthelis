using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleSceneManager : MonoBehaviour
{
    public void SetNewPanelChip(PanelFiledPresenter panelFiledPresenter)
    {
        panelFiledPresenter.SetNewPanelChip();
    }

    public void TurnChip(PanelFiledPresenter panelFiledPresenter)
    {
        panelFiledPresenter.TurnChip();
    }
}

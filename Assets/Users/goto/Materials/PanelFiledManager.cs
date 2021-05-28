using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelFiledManager : MonoBehaviour
{
    enum PareDirection
    {
        Horizontal = 0,
        Vertical   = 1
    }

    private class PanelPare
    {
        //  パネルペア
        public List<GameObject> PanelPares = new List<GameObject>();
        public PareDirection    Direction = PareDirection.Horizontal;
    }

    [SerializeField]
    private GameObject _panelChipPrefab;

    //  生成したチップリスト
    private List<GameObject> _panelChipList = new List<GameObject>();
    private PanelPare  _panelPare;

    private Dictionary<PareDirection, List<Vector3>> _posTabl = new Dictionary<PareDirection, List<Vector3>>()
    {
        { PareDirection.Horizontal, new List<Vector3>(){new Vector3(0.5f, 0, 0),new Vector3(-0.5f, 0, 0)}},    
        { PareDirection.Vertical, new List<Vector3>(){new Vector3(0, 0.5f, 0),new Vector3(0, -0.5f, 0)}},
    };

    //  新規にパネルを生成
    public void SetPanelChip()
    {
        _panelPare = new PanelPare();
        _panelPare.Direction = (PanelFiledManager.PareDirection) Random.Range(0, 2);
        var gobj1 = Instantiate(_panelChipPrefab, transform);
        gobj1.GetComponent<ChipPresenter>()
             .Init((PareDirection.Horizontal ==_panelPare.Direction)? ChipManager.ChipFrontBack.Front : ChipManager.ChipFrontBack.Back,
                   (PareDirection.Horizontal ==_panelPare.Direction)? ChipManager.ChipDirection.Right : ChipManager.ChipDirection.Up);
        gobj1.GetComponent<ChipPresenter>().SetPosition(new Vector3(0,0,0));
        _panelPare.PanelPares.Add(gobj1);
        
        var   gobj2    = Instantiate(_panelChipPrefab, transform);
        gobj2.GetComponent<ChipPresenter>()
             .Init((PareDirection.Horizontal ==_panelPare.Direction)? ChipManager.ChipFrontBack.Front : ChipManager.ChipFrontBack.Back,
                   (PareDirection.Horizontal ==_panelPare.Direction)? ChipManager.ChipDirection.Left : ChipManager.ChipDirection.Down);
        gobj2.GetComponent<ChipPresenter>().SetPosition(new Vector3(0, 0, 0));
        _panelPare.PanelPares.Add(gobj2);
    }

    public void TurnChip()
    {
        _panelPare.PanelPares[0].GetComponent<ChipPresenter>().Turn();
    }
}
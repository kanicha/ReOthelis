using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrePieceGauge : SingletonMonoBehaviour<PrePieceGauge>
{
    [SerializeField]
    private Image _frame1 = null;
    [SerializeField]
    private Image _frame2 = null;
    [SerializeField]
    private Image _gauge = null;

    public void SetPosition(Vector3 pos1, Vector3 pos2)
    {
        _frame1.rectTransform.anchoredPosition = new Vector2(Map._BOARD_WIDTH / (Map._HORIZON_SIZE - 2) * (pos1.x - 1), Map._BOARD_HEIGHT / (Map._VERTICAL_SIZE - 1) * pos1.z);
        _frame2.rectTransform.anchoredPosition = new Vector2(Map._BOARD_WIDTH / (Map._HORIZON_SIZE - 2) * (pos2.x - 1), Map._BOARD_HEIGHT / (Map._VERTICAL_SIZE - 1) * pos2.z);

        //
        Vector3 setPos = Vector3.zero;
        Vector2 pivot = Vector2.zero;
        Vector3 rotation = Vector3.zero;
        // horizontal
        if ((int)pos1.x == (int)pos2.x)
        {
            if (pos1.z < pos2.z)
                setPos = pos1;
            else
                setPos = pos2;

            rotation = new Vector3(0, 0, 90);
            pivot = new Vector2(0.5f, 1);
        }
        // vertical
        else
        {
            if (pos1.x < pos2.x)
                setPos = pos1;
            else
                setPos = pos2;

            rotation = new Vector3(0, 0, 0);
            pivot = new Vector2(0, 1);
        }

        _gauge.rectTransform.pivot = pivot;
        _gauge.rectTransform.anchoredPosition = new Vector2(Map._BOARD_WIDTH / (Map._HORIZON_SIZE - 2) * (setPos.x - 1), Map._BOARD_HEIGHT / (Map._VERTICAL_SIZE - 1) * setPos.z);
        _gauge.rectTransform.rotation = Quaternion.Euler(rotation);
    }

    public void SetGaugeRatio(float passedTime, float maxTime)
    {
        _gauge.fillAmount = 1 - passedTime / maxTime;
    }

    public void Deactivate()
    {
        _frame1.rectTransform.anchoredPosition = new Vector2(9999, 9999);
        _frame2.rectTransform.anchoredPosition = new Vector2(9999, 9999);
        _gauge.rectTransform.anchoredPosition= new Vector2(9999, 9999);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneView : MonoBehaviour
{
    [SerializeField]
    private ScenarioPresenter _scePresenter;
    public ScenarioPresenter ScePresenter { get { return _scePresenter; } }
}

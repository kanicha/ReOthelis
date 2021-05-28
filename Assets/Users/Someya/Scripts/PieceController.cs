using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    [SerializeField] Material _material = null;

    Material _myMaterialW = null;
    Material _myMaterialB = null;

    [SerializeField] MeshRenderer _cylinderW = null;
    [SerializeField] MeshRenderer _cylinderB = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColor(bool isFace)
    {
        if(_myMaterialW == null)
        {
            _myMaterialW = GameObject.Instantiate<Material>(_material);
            _myMaterialB = GameObject.Instantiate<Material>(_material);
            _cylinderW.material = _myMaterialW;
            _cylinderB.material = _myMaterialB;
        }
        _myMaterialW.color = isFace ? Color.white : Color.black;
        _myMaterialB.color = isFace ? Color.black : Color.white;
    }

    public void SetState(OthelloSystem.CharacterState state)
    {
        bool isActive = (state != OthelloSystem.CharacterState.None);
        {
            _cylinderW.gameObject.SetActive(isActive);
            _cylinderB.gameObject.SetActive(isActive);
        }
        SetColor(state == OthelloSystem.CharacterState.Face);
    }
}

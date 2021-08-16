using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioView : MonoBehaviour
{
    [SerializeField]
    private Text _messageText;
    public Text MessageText { get { return _messageText; } }

    [SerializeField]
    private List<Button> _selectButtons = new List<Button>();
    public List<Button> SelectButtons { get { return _selectButtons; } }

    [SerializeField]
    private List<Text> _selectTexts = new List<Text>();
    public List<Text> SelectTexts { get { return _selectTexts; } }
}
